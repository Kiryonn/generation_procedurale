using Data;
using TMPro;

using UnityEngine;

using UnityEngine.UI;

namespace UI {
	public class EmailUI: MonoBehaviour {
		[SerializeField] private Scrollbar scrollbar;
		[SerializeField] private VerticalLayoutGroup textArea;
		[SerializeField] private TMP_Text addressTMPText;
		[SerializeField] private TMP_Text headerTMPText;
		[SerializeField] private TMP_Text bodyTMPText;
		[SerializeField] private TMP_Text footerTMPText;


		/// <summary>
		/// updates the content and display of the e-mail
		/// </summary>
		/// <param name="email">the e-mail to display</param>
		public void UpdateMailInfos(Email email) {
			// update e-mail content
			addressTMPText.text = email.Address;
			headerTMPText.text = email.Header;
			bodyTMPText.text = email.Body;
			footerTMPText.text = email.Footer;
			
			// force the update of the textArea
			// EXTREMELY IMPORTANT DO NOT TOUCH
			Canvas.ForceUpdateCanvases();
			textArea.enabled = false;
			textArea.enabled = true;
			
			// scroll to the top of the letter
			scrollbar.value = 1;
		}
	}
}