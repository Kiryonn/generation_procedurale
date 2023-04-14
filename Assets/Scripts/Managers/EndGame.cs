// Namespace imports
using Data;
using UI;

// Unity imports
using TMPro;
using UnityEngine.UI;
using UnityEngine;

// Other imports
using System.Collections.Generic;


public class EndGame: MonoBehaviour {
	[SerializeField] private EmailUI email;
	[SerializeField] private TMP_Text nextDayTextButton;
	[SerializeField] private TMP_Text mailInfoLabel;

	[SerializeField] private Button mainMenuButton;
	[SerializeField] private Button nextLevelButton;
	[SerializeField] private Button nextButton;
	[SerializeField] private Button previousButton;
	private int _mailIndex = 0;
	private List<Email> _emails;

	public void ListEmail(List<Email> list) {
		_emails = list;
		_mailIndex = 0;
	}

	public void Defeat() {
		_mailIndex = 0;
		nextDayTextButton.text = "Recomencer le niveau";
		UpdateMailReviewLabel();
	}

	public void Victory() {
		_mailIndex = 0;
		nextDayTextButton.text = "Niveau suivant";
		UpdateMailReviewLabel();
	}

	public void OnNextButtonPressed() {
		_mailIndex++;
		UpdateMailReviewLabel();
		nextButton.interactable = _mailIndex != _emails.Count - 1;
		previousButton.interactable = true;
	}

	public void OnPreviousButtonPressed() {
		_mailIndex--;
		UpdateMailReviewLabel();
		previousButton.interactable = _mailIndex != 0;
		nextButton.interactable = true;
	}

	private void UpdateMailReviewLabel() {
		mailInfoLabel.text = $"Revue de mail {_mailIndex + 1} / {_emails.Count}";
		email.UpdateMailInfos(_emails[_mailIndex]);
	}
}
