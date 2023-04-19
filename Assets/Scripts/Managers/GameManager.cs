using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
	public class GameManager : MonoBehaviour {
		public static GameManager Instance;
		[Header("Reputation")]
		public Reputation reputation;
		public float reputationLoss;
		public float reputationGain;
		[Header("Screens")]
		public EndGame endGame;
		[SerializeField] private GameObject game;
        [SerializeField] private Introduction intro;
        private int currentDay;
		private int currentMail;
		[Header("E-mail")]
		public EmailUI email;
		private Vector2 _emailOriginalPosition;
		private List<Email> _sessionEmails;
		[Header("Nombre de mail")]
		public int nbMailDay;

		private bool _isGameOver;
		private int _rules;
		int[] days;


        private void Awake() {
			if (Instance == null)
				Instance = this;
		}

		private void Start() {
			_emailOriginalPosition = email.transform.position;
			var daysPath = $"{Application.streamingAssetsPath}/difficulties.json";
			var json = File.ReadAllText(daysPath);
			days = json.Substring(1, json.Length - 2).Split(",").Select(int.Parse).ToArray();
			currentDay = PlayerPrefs.GetInt("Session");
			_rules = days[currentDay];
			_sessionEmails = new List<Email>();
            intro.gameObject.SetActive(true);
            intro.Restart((Rules)_rules, currentDay);
		}

		public void CheckResult(bool playerAnswer) {
			var isPlayerCorrect = playerAnswer == _sessionEmails[currentMail].IsPhishing;
			reputation.AddReputation(isPlayerCorrect ? reputationLoss : reputationGain);
		}

		public void Victory() {
            //Instantiate(victoryScreen);
            endGame.gameObject.SetActive(true);
            game.SetActive(false);
            endGame.ListEmail(_sessionEmails);
            endGame.Victory();
            _isGameOver = true;
		}

		public void Defeat() {
            currentDay -= 1;
            endGame.gameObject.SetActive(true);
            game.SetActive(false);
            endGame.ListEmail(_sessionEmails);
            endGame.Defeat();
            
            _isGameOver = true;
        }

		public void MailValider(bool playerAnswer) {
			CheckResult(playerAnswer);
			if (_isGameOver) { return; }
            if (_sessionEmails.Count == nbMailDay) { Victory(); }
			else { CreateNewEmail(); }
        }

		public void GoToMainMenu() {
			SceneManager.LoadScene("MainMenu");
        }

		public void NextDay() {
            _isGameOver = false;
			_sessionEmails = new List<Email>();
            endGame.gameObject.SetActive(false);

            currentDay += 1;
            _rules = days[currentDay];

			intro.gameObject.SetActive(true);
			intro.Restart((Rules)_rules,currentDay);

            //game.SetActive(true);
            
        }

        public void CreateNewEmail(){
            currentMail++;
            _sessionEmails.Add(EmailManager.Instance.CreateEMail(_rules));
			email.transform.position = _emailOriginalPosition;
			email.Close();
			email.UpdateMailInfos(_sessionEmails[currentMail]);
		}

		public void Go() 
		{
			game.SetActive(true);

            currentMail = -1;
            CreateNewEmail();
            //plus reste de la page

        }
    }
}

[Flags]
public enum Rules {
	// pour connaitre les regles active:
	// (_rules & (int) Rules.InvalidAddress) == 0 signifie que la regle des addresses est inactive
	// (_rules & (int) Rules.InvalidAddress) == 1 signifie qu'il faut faire attention a l'adresse
	Random = -1,
	InvalidAddress = 1,
	IncorrectSpelling = 2,
	PersonalData = 4,
	FishyLink = 8,
    ExaggeratedMail = 16,
    WeirdHeader = 32,
    Threat = 64
}
