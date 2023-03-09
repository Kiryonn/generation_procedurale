using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionManger : MonoBehaviour
{
    int session;
    public Button play;
    public loaderScenes loadScene;

    public void SelectionSession(int n)
    {
        session = n;
        play.interactable = true;
    }

    public void LoadSession()
    {
        loadScene.LoadGame(session);
    }

}
