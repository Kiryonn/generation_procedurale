// Namespace imports
using Data;
using UI;

// Unity imports
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class EndGame: MonoBehaviour {
	[SerializeField] private EmailUI email;
	[SerializeField] private TMP_Text nextDayTextButton;
	[SerializeField] private TMP_Text mailInfoLabel;

	[SerializeField] private Button mainMenuButton;
	[SerializeField] private Button nextLevelButton;
	[SerializeField] private Button nextButton;
	[SerializeField] private Button previousButton;
	private int _mailIndex;
	private Email[] _emails;

	public void ListEmail(Email[] list) {
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
		previousButton.interactable = true;
		nextButton.interactable = _mailIndex != _emails.Length - 1;
	}

	public void OnPreviousButtonPressed() {
		_mailIndex--;
		UpdateMailReviewLabel();
		previousButton.interactable = _mailIndex != 0;
		nextButton.interactable = true;
	}

	private void UpdateMailReviewLabel() {
		mailInfoLabel.text = $"Revue de mail {_mailIndex + 1} / {_emails.Length}";
		email.UpdateMailInfos(_emails[_mailIndex]);
	}
}
