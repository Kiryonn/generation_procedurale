// Namespace imports
using Managers;

// Unity imports
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Introduction: MonoBehaviour {
	private float _counter;
	[SerializeField] private TextMeshProUGUI _rulesText;
	[SerializeField] private TextMeshProUGUI Titre;
	[SerializeField] private float time;

	private void Update() {
		_counter += Time.deltaTime;
		if (_counter > time)
			end();
	}

	private void end() {
		gameObject.SetActive(false);
	}

	public void Restart(Rules rules, int curentDay) {
		_counter = 0;
		Titre.text = "Jour      :  " + curentDay;

		// Construire le texte à afficher en fonction des r�gles actives
		string rulesActiveText = "Règles actives : \n";
		if (rules.HasFlag(Rules.InvalidAddress))
			rulesActiveText += "Il y a de Mauvaise adresse email \n";
		if (rules.HasFlag(Rules.IncorrectSpelling))
			rulesActiveText += "Il y a des faute d'orthographe \n"; //todo
		if (rules.HasFlag(Rules.PersonalData))
			rulesActiveText += "Il y a des demandes de donn�es personnelles \n"; //todo
		if (rules.HasFlag(Rules.FishyLink))
			rulesActiveText += "Il y a des lien piègé \n";
		if (rules.HasFlag(Rules.ExaggeratedMail))
			rulesActiveText += "Il y a des mails faisant appel aux sentiments\n";
		if (rules.HasFlag(Rules.WeirdHeader))
			rulesActiveText += "Il y a des mails avec titre de mail douteux\n";
		if (rules.HasFlag(Rules.Threat))
			rulesActiveText += "Il y a des Mail contenant des menace\n";
		// Afficher le texte des règles actives
		if (rulesActiveText.Length < 30)
			rulesActiveText += "pas de regles actives";

		_rulesText.text = rulesActiveText;
	}
}
