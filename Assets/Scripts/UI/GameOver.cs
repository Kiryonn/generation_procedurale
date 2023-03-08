using UnityEngine;

/// <summary>
/// afiche l'écran de fin de partie
/// </summary>
public class GameOver : MonoBehaviour
{
    public Canvas canvas;

    public GameObject CanvasDefeat;
    public GameObject CanvasVictory;
    public void defeat()
    {
        canvas.transform.gameObject.SetActive(false);
        GameObject t = Instantiate(CanvasDefeat);
    }
    public void victory(float n)
    {
        GameObject t = Instantiate(CanvasVictory); 
    }

}
