using System;
using Data;
using UI;
using UnityEngine;

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
		public Canvas gameScreen;
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
			var daysPath = Application.dataPath;
			Debug.Log(daysPath);
			// daysPath += "Data/difficulties.json";
			var currentDay = PlayerPrefs.GetInt("Session");
			// days = JsonUtility.FromJson<int[]>(File.ReadAllText(daysPath));
		}

		/// <summary>
		/// Check if the player is right or wrong and update the reputation accordingly.
		/// </summary>
		/// <param name="playerAnswer">Did the player listed the e-mail as phishing or not.</param>
		private void CheckResult(bool playerAnswer)
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
			gameScreen.gameObject.SetActive(false);
			Instantiate(defeatScreen);
		}
	}
}
