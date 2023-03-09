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
		public GameObject victoryScreen;
		public GameObject defeatScreen;
		[Header("E-mail")]
		public EmailUI email;
		private Email[] _sessionEmails;

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		private void Start()
		{
			var daysPath = Application.dataPath + "/Data/difficulties.json";
			var days = JsonUtility.FromJson<int[]>(File.ReadAllText(daysPath));
			var currentDay = PlayerPrefs.GetInt("Session");
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
			Instantiate(victoryScreen); 
		}
	
		public void Defeat()
		{
			Instantiate(defeatScreen);
		}

		public void GoToMainMenu()
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
