using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;
		[Header("Reputation")]
		public Reputation reputation;
		public float reputationLoss;
		public float reputationGain;
		[Header("Screens")]
		[SerializeField]
        private EndGame endGame;
        [SerializeField]
        private GameObject game;
        [Header("E-mail")]
		public EmailUI email;
		private Vector2 _emailOriginalPosition;
		private Email[] _sessionEmails;
		private int currentDay;

		private int _rules;

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		private void Start()
		{
			_emailOriginalPosition = email.transform.position;
			var daysPath = Application.dataPath + "/Data/difficulties.json";
			var json = File.ReadAllText(daysPath);
			var days = json.Substring(1, json.Length - 2).Split(",").Select(int.Parse).ToArray();
			var currentDay = PlayerPrefs.GetInt("Session");
			_rules = days[currentDay];
			CreateNewEmail();
		}
		

		/// <summary>
		/// Check if the player is right or wrong and update the reputation accordingly.
		/// </summary>
		/// <param name="playerAnswer">Did the player listed the e-mail as phishing or not.</param>
		public void CheckResult(bool playerAnswer) {
			Email currentMail = _sessionEmails[^1];
			var isPlayerCorrect = playerAnswer == currentMail.IsPhishing;
			reputation.AddReputation(isPlayerCorrect ? reputationLoss : reputationGain);
		}

		public void Victory() {
			Instantiate(victoryScreen); 
		}

		public void Defeat() {
            endGame.gameObject.SetActive(true);
            game.SetActive(false);
            currentDay -= 1;
            endGame.Defeat();
        }

		public void GoToMainMenu() {
			SceneManager.LoadScene("MainMenu");
			
        }

		public void nextDay() {
			currentDay += 1;
            endGame.gameObject.SetActive(false);
            game.SetActive(true);
			//plus reste de la page
        }

		public void CreateNewEmail()
		{
			email.gameObject.SetActive(false);
			email.transform.position = _emailOriginalPosition;
			email.Close();
			email.UpdateMailInfos(EmailManager.Instance.CreateEMail(_rules));
			email.gameObject.SetActive(true);
		}
	}
}
