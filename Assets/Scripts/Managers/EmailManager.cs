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
		// todo create all mails from the start to lower latency
		// todo fix email display on end screen

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
			// todo create mails and push them into the GameManager
			var mails = new Email[usedContexts.Length];
			for (var i = 0; i < usedContexts.Length; i++) {
				Rules errors = Enum
					.GetValues(typeof(Rules)).Cast<Rules>()
					.Where(flag => activeRules.HasFlag(flag) && Random.Range(0, 1f) < phishingChance)
					.Aggregate(Rules.None, (currentErrors, newError) => currentErrors | newError);
				mails[i] = CreateMail(errors, mailsCache[usedContexts[i]]);
			}

			gameManager.sessionEmails = mails;
			gameManager.LoadNextMail();
			Destroy(this);
		}

		private Email CreateMail(Rules errors, IReadOnlyDictionary<Rules, EmailData> pool) {
			var addressFlags = new[] { Rules.InvalidAddress };
			var headerFlags = new[] { Rules.WeirdHeader };
			var bodyFlags = new[] { Rules.ExaggeratedMail, Rules.FishyLink, Rules.IncorrectSpelling, Rules.PersonalData, Rules.Threat };
			var footerFlag = new Rules[] { };
			
			// load incorect possibilities
			// todo update lists
			var addresses = Array.Empty<string>();
			var headers = Array.Empty<string>();
			var bodies = Array.Empty<string>();
			var footers = Array.Empty<string>();

			// load correct values
			if (addresses.Length == 0) addresses = pool[Rules.None].addresses;
			if (headers.Length == 0) headers = pool[Rules.None].headers;
			if (bodies.Length == 0) bodies = pool[Rules.None].bodies;
			if (footers.Length == 0) footers = pool[Rules.None].footers;

			// create the mail data
			var address = addresses[Random.Range(0, addresses.Length)];
			var header = headers[Random.Range(0, headers.Length)];
			var body = bodies[Random.Range(0, bodies.Length)];
			var footer = footers[Random.Range(0, footers.Length)];

			// create and return the mail
			return new Email(address, header, body, footer, errors);
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