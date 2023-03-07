using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Canvas canvas;

    public GameObject CanvasEnd;
    public void defeat()
    { canvas.transform.gameObject.SetActive(false);
        GameObject t = Instantiate(CanvasEnd);
    }
    
    public void victory() 
    { }

}
