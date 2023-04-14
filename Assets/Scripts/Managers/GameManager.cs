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
		public EndGame endGame;
		[SerializeField] private GameObject game;
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

		private void Awake() {
			if (Instance == null)
				Instance = this;
		}

		private void Start() {
			_emailOriginalPosition = email.transform.position;
			var daysPath = Application.dataPath + "/Data/difficulties.json";
			var json = File.ReadAllText(daysPath);
			var days = json.Substring(1, json.Length - 2).Split(",").Select(int.Parse).ToArray();
			currentDay = PlayerPrefs.GetInt("Session");
			_rules = days[currentDay];
			_sessionEmails = new List<Email>();
            currentMail = -1;
            CreateNewEmail();
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
            currentDay += 1;
            endGame.gameObject.SetActive(false);
            game.SetActive(true);
            currentMail = -1;
            CreateNewEmail();
			//plus reste de la page
        }

        public void CreateNewEmail(){
            currentMail++;
            _sessionEmails.Add(EmailManager.Instance.CreateEMail(_rules));

            email.gameObject.SetActive(false);
			email.transform.position = _emailOriginalPosition;
			email.Close();
			email.UpdateMailInfos(_sessionEmails[currentMail]);
			email.gameObject.SetActive(true);
		}
	}
}

public enum Rules {
	InvalidAddress = 1,
	IncorrectSpelling = 2,
	PersonalData = 4,
	FishyLink = 8
}
