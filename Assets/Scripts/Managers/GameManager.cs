// Namespace imports
using Data;
using UI;

// System imports
using System;
using System.IO;
using System.Linq;

// Unity imports
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
	public class GameManager: MonoBehaviour {
		public static GameManager Instance;
		[Header("Reputation")] public Reputation reputation;
		public float reputationLoss;
		public float reputationGain;
		[Header("Screens")] public EndGame endGame;
		[SerializeField] private GameObject game;
		private int _currentDay;
		private int _currentMail;
		[Header("E-mail")] public EmailUI email;
		public int nbMailDay;
		public float phishingChange;
		[NonSerialized] public Email[] sessionEmails;
		[NonSerialized] public Rules activeRules;
		private Vector2 _emailOriginalPosition;

		private bool _isGameOver;

		private void Awake() {
			if (Instance == null) Instance = this;

			sessionEmails = new Email[nbMailDay];
			var daysPath = $"{Application.streamingAssetsPath}/days.json";
			var json = File.ReadAllText(daysPath);

			var days = json[1..^1].Split(",").Select(int.Parse).ToArray();
			_currentDay = PlayerPrefs.GetInt("Session");
			activeRules = (Rules)days[_currentDay];
			_currentMail = -1;
		}

		private void Start() {
			_emailOriginalPosition = email.transform.position;
		}

		public void CheckResult(bool playerAnswer) {
			var isPlayerCorrect = playerAnswer && sessionEmails[_currentMail].Errors == Rules.None ||
								!playerAnswer && sessionEmails[_currentMail].Errors != Rules.None;
			reputation.AddReputation(isPlayerCorrect ? reputationLoss : reputationGain);
		}

		private void Victory() {
			//Instantiate(victoryScreen);
			endGame.gameObject.SetActive(true);
			game.SetActive(false);
			endGame.ListEmail(sessionEmails);
			endGame.Victory();
			_isGameOver = true;
		}

		public void Defeat() {
			_currentDay -= 1;
			endGame.gameObject.SetActive(true);
			game.SetActive(false);
			endGame.ListEmail(sessionEmails);
			endGame.Defeat();

			_isGameOver = true;
		}

		public void AnswerCheck(bool playerAnswer) {
			CheckResult(playerAnswer);
			if (_isGameOver) return;
			if (_currentMail == nbMailDay) Victory();
			else LoadNextMail();
		}

		public void GoToMainMenu() {
			SceneManager.LoadScene("MainMenu");
		}

		public void NextDay() {
			PlayerPrefs.SetInt("Session", _currentDay + 1);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void LoadNextMail() {
			_currentMail++;
			email.Close();
			email.UpdateMailInfos(sessionEmails[_currentMail]);
			email.transform.position = _emailOriginalPosition;
		}
	}
}

[Flags]
public enum Rules {
	// pour connaitre les regles active:
	// (_rules & (int) Rules.InvalidAddress) == 0 signifie que la regle des addresses est inactive
	// (_rules & (int) Rules.InvalidAddress) == 1 signifie qu'il faut faire attention a l'adresse
	None = 0,
	InvalidAddress = 1,
	IncorrectSpelling = 1 << 1,
	PersonalData = 1 << 2,
	FishyLink = 1 << 3,
	ExaggeratedMail = 1 << 4,
	WeirdHeader = 1 << 5,
	Threat = 1 << 6
}
