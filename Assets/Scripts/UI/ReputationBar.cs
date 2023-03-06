using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class qui gère la Bar de Repution
/// </summary>
public class ReputationBar : MonoBehaviour
{
    public float reputation = 0.3f;
    public Image reputationFill;

    /// <summary>
    /// change la valeur de la barre de reputaion pour une valeur x 
    /// </summary>
    /// <param name="x">taille de la barre entre 0 et 1 en float </param>
    public void SetRepution(float x)
    {
        reputation = x;
        if (reputation > 1) { reputation = 1; }
        if (reputation < 0) { reputation = 0; }
        reputationFill.transform.localScale = new Vector3( reputation,1,1);
        
    }
}
