using System.IO;
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
		private Email[] _sessionEmails;
		private int currentDay;


        private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		private void Start()
		{
			var daysPath = Application.dataPath + "/Data/difficulties.json";
			var days = JsonUtility.FromJson<int[]>(File.ReadAllText(daysPath));
			currentDay = PlayerPrefs.GetInt("Session");
		}

		/// <summary>
		/// Check if the player is right or wrong and update the reputation accordingly.
		/// </summary>
		/// <param name="playerAnswer">Did the player listed the e-mail as phishing or not.</param>
		public void CheckResult(bool playerAnswer)
		{
			Email currentMail = _sessionEmails[^1];
			var isPlayerCorrect = playerAnswer == currentMail.IsPhishing;
			reputation.AddReputation(isPlayerCorrect ? reputationLoss : reputationGain);
		}
	
		public void Victory()
		{
			endGame.gameObject.SetActive(true);
			game.SetActive(false);// normalent pas besoin mais evite davoir trop d'objet actif en meme temps inutilment  

        }
	
		public void Defeat()
		{
            endGame.gameObject.SetActive(true);
            game.SetActive(false);
            currentDay -= 1;
            endGame.Defeat();
        }

		public void GoToMainMenu()
		{
			SceneManager.LoadScene("MainMenu");
			
        }

		public void nextDay() 
		{
			currentDay += 1;
            endGame.gameObject.SetActive(false);
            game.SetActive(true);
			//plus reste de la page
        }


    }
}
