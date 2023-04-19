// Namespace imports
using Data;

// System imports
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Unity imports
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers {
	public class EmailManager: MonoBehaviour {
		private void Start() {
			// get the list of possible context
			var emailDirPath = $"{Application.streamingAssetsPath}/Emails";
			DirectoryInfo emailDir = new DirectoryInfo(emailDirPath);
			var contexts = emailDir.GetDirectories().Select(dir => dir.Name).ToArray();

			GameManager gameManager = GameManager.Instance;

			// choose contexts
			var usedContexts = new string[gameManager.nbMailDay];
			for (var i = 0; i < usedContexts.Length; i++)
				usedContexts[i] = contexts[Random.Range(0, usedContexts.Length)];

			var mailsCache = new Dictionary<string, Dictionary<Rules, EmailData>>();
			Rules activeRules = gameManager.activeRules;

			// retrieve and cache mails parts
			foreach (var uniqContext in usedContexts.Distinct()) {
				mailsCache[uniqContext] = new();

				// search data within context
				var contextPath = $"{emailDirPath}/{uniqContext}";
				var files = new DirectoryInfo(contextPath).GetFiles("*.json");

				foreach (FileInfo file in files) {
					// retrieve rule
					Rules rule = FileNameToRules(file);

					// check rule compatibility
					if (rule != Rules.None && (activeRules & rule) == 0) continue;

					// read file and extract data
					var json = File.ReadAllText($"{contextPath}/{file.Name}");
					EmailData emailData = JsonUtility.FromJson<EmailData>(json);
					mailsCache[uniqContext].Add(rule, emailData);
				}
			}
			
			var phishingChance = gameManager.phishingChange;
			var mails = new Email[usedContexts.Length];
			for (var i = 0; i < usedContexts.Length; i++) {
				Rules errors = Enum
					.GetValues(typeof(Rules)).Cast<Rules>()
					.Where(flag => activeRules.HasFlag(flag) && Random.Range(0, 1f) < phishingChance)
					.Aggregate(Rules.None, (currentErrors, newError) => currentErrors | newError);
				// todo check cache compat with error before sending to CreateMail and remove the rubish happening in it
				mails[i] = CreateMail(errors, mailsCache[usedContexts[i]]);
			}

			gameManager.sessionEmails = mails;
			gameManager.LoadNextMail();
			Destroy(this);
		}

		private Email CreateMail(Rules errors, IReadOnlyDictionary<Rules, EmailData> pool) {
			var addressFlags = new[] { Rules.InvalidAddress };
			var headerFlags = new[] { Rules.WeirdHeader, Rules.IncorrectSpelling };
			var bodyFlags = new[] { Rules.ExaggeratedMail, Rules.FishyLink, Rules.IncorrectSpelling, Rules.PersonalData, Rules.Threat };
			var footerFlags = new Rules[] { };

			// load incorect possibilities
			string[] addresses;
			string[] headers;
			string[] bodies;
			string[] footers;

			// search if error available in pool else find the closest rule to the error
			if (pool.ContainsKey(errors)) {
				addresses = pool[errors].addresses;
				headers = pool[errors].headers;
				bodies = pool[errors].bodies;
				footers = pool[errors].footers; }
			else {
				Rules addressRules = addressFlags.Where(flag => errors.HasFlag(flag)).Aggregate(Rules.None, (current, flag) => current | flag);
				Rules headerRules = headerFlags.Where(flag => errors.HasFlag(flag)).Aggregate(Rules.None, (current, flag) => current | flag);
				Rules bodyRules = bodyFlags.Where(flag => errors.HasFlag(flag)).Aggregate(Rules.None, (current, flag) => current | flag);
				Rules footerRules = footerFlags.Where(flag => errors.HasFlag(flag)).Aggregate(Rules.None, (current, flag) => current | flag);

				Rules closestAddressRules = Combinations(addressRules).FirstOrDefault(rules => pool.ContainsKey(rules));
				Rules closestHeaderRules = Combinations(headerRules).FirstOrDefault(rules => pool.ContainsKey(rules));
				Rules closestBodyRules = Combinations(bodyRules).FirstOrDefault(rules => pool.ContainsKey(rules));
				Rules closestFooterRules = Combinations(footerRules).FirstOrDefault(rules => pool.ContainsKey(rules));

				addresses = pool[closestAddressRules].addresses;
				headers = pool[closestHeaderRules].headers;
				bodies = pool[closestBodyRules].bodies;
				footers = pool[closestFooterRules].footers;

				// update errors to the closest rules found
				errors = closestAddressRules | closestHeaderRules | closestBodyRules | closestFooterRules; }

			// load correct answers and change `errors` if empty
			// maybe overkill and unnecessary, idk i don't want to think anymore
			if (addresses.Length == 0) {
				foreach (Rules flag in addressFlags) if (errors.HasFlag(flag)) errors ^= flag;
				addresses = pool[Rules.None].addresses; }
			if (headers.Length == 0) {
				foreach (Rules flag in headerFlags) if (errors.HasFlag(flag)) errors ^= flag;
				headers = pool[Rules.None].headers; }
			if (bodies.Length == 0) {
				foreach (Rules flag in bodyFlags) if (errors.HasFlag(flag)) errors ^= flag;
				bodies = pool[Rules.None].bodies; }
			if (footers.Length == 0) {
				foreach (Rules flag in footerFlags) if (errors.HasFlag(flag)) errors ^= flag;
				footers = pool[Rules.None].footers; }

			// create the mail data
			var address = addresses[Random.Range(0, addresses.Length)];
			var header = headers[Random.Range(0, headers.Length)];
			var body = bodies[Random.Range(0, bodies.Length)];
			var footer = footers[Random.Range(0, footers.Length)];

			// create and return the mail
			return new Email(address, header, body, footer, errors);
		}

		public Rules[] Combinations(Rules rule) {
			return Enumerable.Range(0, (int)Math.Pow(2, Enum.GetValues(typeof(Rules)).Length))
				.Select(x => (Rules)x)
				.Where(x => (x & rule) != 0)
				.OrderByDescending(x => x)
				.ToArray();
		}

		private Rules FileNameToRules(FileSystemInfo file) {
			var highest = ((int)Enum.GetValues(typeof(Rules)).Cast<Rules>().Max() << 1) - 1;
			var rules = int.Parse(file.Name[..^file.Extension.Length]);

			if (rules > highest)
				throw new InvalidDataException($"There is no rules combination that would stack up higher than {highest}. Given : {file.Name}");

			return (Rules) rules;
		}
	}
}
