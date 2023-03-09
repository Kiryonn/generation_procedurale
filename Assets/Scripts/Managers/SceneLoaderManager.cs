using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
	public class SceneLoaderManager : MonoBehaviour
	{
		public void LoadGame(int session)
		{
			PlayerPrefs.SetInt("Session", session);
			SceneManager.LoadScene("Game");
		}

		public void LoadMainMenu()
		{
			SceneManager.LoadScene("MainMenu");
		}

		public void LoadMenuSession()
		{
			SceneManager.LoadScene("MenuSession");
		}
	}
}
