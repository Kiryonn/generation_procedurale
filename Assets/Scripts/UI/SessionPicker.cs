using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class SessionPicker : MonoBehaviour
	{
		[SerializeField]
		private Button _play;
		private void Start()
		{
			//_play = GetComponent<Button>();
			_play.interactable = false;
		}

		public void SelectionSession(int n)
		{
			_play.interactable = true;
			PlayerPrefs.SetInt("Session", n);
		}

		public void OnButtonPlayPressed()
		{
			SceneManager.LoadScene("Game");
		}
	}
}
