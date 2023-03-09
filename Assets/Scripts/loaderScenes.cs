using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loaderScenes : MonoBehaviour
{
    

    public void LoadGame(int n)
    {
        PlayerPrefs.SetInt("Session", n);
        SceneManager.LoadScene("game");
        //Debug.Log(PlayerPrefs.GetInt("Session"));
    }
    

        public void LoadGame()
    {
        PlayerPrefs.SetInt("Session", 1);
        SceneManager.LoadScene("game");
    }

        public void LoadMenuPrincipale()
    {
        SceneManager.LoadScene("menuPrincipale");
    }

        public void LoadMenuSession()
    {
        SceneManager.LoadScene("MenuSession");
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

}
