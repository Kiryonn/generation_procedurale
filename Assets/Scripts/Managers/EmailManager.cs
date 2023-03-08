using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers {
	class EmailManager: MonoBehaviour {
		public static EmailManager Instance;
		public float phishingChance;
		private string[] _possibleContext;

		private void Awake() {
			// get the list of possible context
			DirectoryInfo emailDir = new DirectoryInfo(Application.dataPath + "/Data/Emails/");
			DirectoryInfo[] contexts = emailDir.GetDirectories();
			_possibleContext = new string[contexts.Length];

			for (int i = 0; i < contexts.Length; i++)
				_possibleContext[i] = contexts[i].Name;

			// singleton
			if (Instance == null)
				Instance = this;
		}

		/// <summary>
		/// Generate a brand new e-mail (data)
		/// </summary>
		/// <param name="rules">the active rules of the level</param>
		/// <returns>The generated e-mail</returns>
		public Email CreateEMail(int rules) {
			// choose a context
			var context = _possibleContext[Random.Range(0, _possibleContext.Length)];

			// declare possibilities
			var possibleMails = new Dictionary<int, EmailBlock>();

			// search data
			string contextPath = Application.dataPath + "/Data/Emails/" + context + "/";
			DirectoryInfo contextDir = new DirectoryInfo(contextPath);
			FileInfo[] emailRules = contextDir.GetFiles();

			foreach (FileInfo emailRule in emailRules) {
				var extension = emailRule.Extension;
				var filename = emailRule.Name;
				// ignore unsupported files
				if (extension != ".json") continue;

				// check rules compatibility
				var fileRules = int.Parse(filename.Substring(0, filename.Length - extension.Length));
				var areRulesValid = rules >= fileRules;
				var rulesCopy = rules;
				var fileRulesCopy = fileRules;

				while (areRulesValid || rulesCopy == fileRulesCopy) {
					if (rulesCopy == 0 || fileRulesCopy == 0) { areRulesValid = rulesCopy >= fileRulesCopy; break; }
					if ((rulesCopy & 1) < (fileRulesCopy & 1)) { areRulesValid = false; continue; }
					rulesCopy >>= 1;
					fileRulesCopy >>= 1;
				}

				// ignore incompatible rules
				if (!areRulesValid) continue;

				// read file
				var json = File.ReadAllText(contextPath + filename);
				EmailBlock emailBlock = JsonUtility.FromJson<EmailBlock>(json);

				// add data to possibilities
				possibleMails.Add(fileRules, emailBlock);
			}

			// choose e-mail data
			var difficulties = possibleMails.Keys.ToArray();
			var difficulty = difficulties[Random.Range(0, difficulties.Length)];

			var isAddressWrong = Random.Range(0f, 1f) <= phishingChance;
			var isHeaderWrong = Random.Range(0f, 1f) <= phishingChance;
			var isBodyWrong = Random.Range(0f, 1f) <= phishingChance;
			var isFooterWrong = Random.Range(0f, 1f) <= phishingChance;

			EmailBlock pool = possibleMails[difficulty];

			var address = isAddressWrong
				? pool.addresses.invalid[Random.Range(0, pool.addresses.invalid.Count)]
				: pool.addresses.valid[Random.Range(0, pool.addresses.valid.Count)];
			var header = isHeaderWrong
				? pool.headers.invalid[Random.Range(0, pool.headers.invalid.Count)]
				: pool.headers.valid[Random.Range(0, pool.headers.valid.Count)];
			var body = isBodyWrong
				? pool.bodies.invalid[Random.Range(0, pool.bodies.invalid.Count)]
				: pool.bodies.valid[Random.Range(0, pool.bodies.valid.Count)];
			var footer = isFooterWrong
				? pool.footers.invalid[Random.Range(0, pool.footers.invalid.Count)]
				: pool.footers.valid[Random.Range(0, pool.footers.valid.Count)];
			var isEmailWrong = isAddressWrong || isHeaderWrong || isBodyWrong || isFooterWrong;

			// return the newly created e-mail
			return new Email(address, header, body, footer, isEmailWrong);
		}
	}
}