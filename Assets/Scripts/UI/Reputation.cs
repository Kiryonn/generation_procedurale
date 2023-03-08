using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// classe qui g�re la r�putation 
/// </summary>
public class Reputation : MonoBehaviour
{
    public float reputation=100f;
    //public int reputationMax;
    public Slider reputationSlider;


    /// <summary>
    /// a faire
    /// </summary>
    void Start()
    {
        //reputation = reputation*1f;
        reputationSlider.maxValue = reputation;
    }

    /// <summary>
    /// ajoute une valeur a la reputation 
    /// </summary>
    /// <param name="n">valeur a ajout�e a la reputation actuel</param>
    void addReputation(int n) 
    {
        reputation += n;

        
        if (reputation <=0) {
            reputation = 0;
            GameOver();
        }
        else { UpdateBar(); }

        
        

    }

    /// <summary>
    /// actualise le slider
    /// </summary>
    void UpdateBar()
    {
        reputationSlider.value = reputation;
    }

    /// <summary>
    /// a faire
    /// </summary>
    void GameOver()
    {
        //todo
    }

}
