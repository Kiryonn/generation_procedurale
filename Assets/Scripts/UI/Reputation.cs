using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// classe qui gère la réputation 
/// </summary>
public class Reputation : MonoBehaviour
{
    public float reputation = 100f;
    //public int reputationMax;
    public Slider reputationSlider;
    public GameOver gameOver;


    /// <summary>
    /// a faire
    /// </summary>
    private void Start()
    {
        //reputation = reputation*1f;
        reputationSlider.maxValue = reputation;
        reputationSlider.value = reputation;
    }

    /// <summary>
    /// ajoute une valeur a la reputation 
    /// </summary>
    /// <param name="n">valeur a ajoutée a la reputation actuel</param>
    public void addReputation(float n)
    {
        reputation += n;


        if (reputation <= 0)
        {
            reputation = 0;
            UpdateBar();
            GameOver();
        }
        else { UpdateBar(); }




    }

    /// <summary>
    /// actualise le slider
    /// </summary>
    private void UpdateBar()
    {
        reputationSlider.value = reputation;
    }

    /// <summary>
    /// apel la fin de partie defeat
    /// </summary>
    private void GameOver()
    {
        gameOver.defeat();

    }

    public void win()
    {
        gameOver.victory(reputation);
    }
}
