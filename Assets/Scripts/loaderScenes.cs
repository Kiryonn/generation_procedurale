using UnityEngine;
using UnityEngine.SceneManagement;

public class loaderScenes : MonoBehaviour
{

	/// <summary>
	/// charge la scène de jeu a la session niveau n
	/// </summary>
	/// <param name="n">niveau de la session</param>
	public void LoadGame(int n)
	{
		PlayerPrefs.SetInt("Session", n);
		SceneManager.LoadScene("game");
	}

	/// <summary>
	/// charge la scène de jeu a la session niveau 1
	/// </summary>
	public void LoadGame()
	{
		PlayerPrefs.SetInt("Session", 1);//si on rajoute un tuto le metre sur 0
		SceneManager.LoadScene("game");
	}

	/// <summary>
	/// charge la scène du menu pricipale
	/// </summary>
	public void LoadMenuPrincipale()
	{
		SceneManager.LoadScene("menuPrincipale");
	}

	public void LoadMenuSession()
	{
		SceneManager.LoadScene("MenuSession");
	}

	/// <summary>
	/// ferme l'application
	/// </summary>
	public void ApplicationQuit()
	{
		Application.Quit();
	}

}
