using UnityEngine;
using UnityEngine.UI;

public class SessionManger : MonoBehaviour
{
	private int session;
	public Button play;
	public loaderScenes loadScene;

	/// <summary>
	/// selection une session
	/// </summary>
	/// <param name="n"></param>
	public void SelectionSession(int n)
	{
		session = n;
		play.interactable = true;
	}

	/// <summary>
	/// lance le jeu a la sesion demand�e 
	/// </summary>
	public void LoadSession()
	{
		loadScene.LoadGame(session);
	}

}
