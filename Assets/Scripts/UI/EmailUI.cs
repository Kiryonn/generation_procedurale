using Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
	public class EmailUI: MonoBehaviour
	{
		[SerializeField] private VerticalLayoutGroup textArea;
		[SerializeField] private TMP_Text addressTMPText;
		[SerializeField] private TMP_Text headerTMPText;
		[SerializeField] private TMP_Text bodyTMPText;
		[SerializeField] private TMP_Text footerTMPText;
		private void Start()
		{
			Email email = new Email(
				"miaou1234@gmail.com",
				"Bonjour,",
				"Ceci est un texte qui n'a aucun sens. Ne lisez pas. Non vraiment y a rien a lire. Pourquoi tu lis ? T'es con ou quoi ? egaegaehae heearhea ej aeoijg oaej jeao gjaeoj aeoj oae e gjoaej goae aegjoa",
				"Miaou");
			UpdateMailInfos(email);
		}

		/// <summary>
		/// change l'apparence de l'e-mail
		/// </summary>
		/// <param name="email">l'e-mail à afficher</param>
		public void UpdateMailInfos(Email email)
		{
			// update content
			addressTMPText.text = email.address;
			headerTMPText.text = email.header;
			bodyTMPText.text = email.body;
			footerTMPText.text = email.footer;
			
			// force the update of the textArea
			Canvas.ForceUpdateCanvases();
			textArea.enabled = false;
			textArea.enabled = true;
		}
	}
}