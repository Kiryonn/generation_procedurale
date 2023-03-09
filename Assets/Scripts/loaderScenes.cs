using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loaderScenes : MonoBehaviour
{
    

        public void LoadGame()
    {
        SceneManager.LoadScene("game");
    }
     
        public void LoadMenuPrincipale()
    {
        SceneManager.LoadScene("menuPrincipale");
    }
}
