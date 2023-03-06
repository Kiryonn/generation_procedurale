using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// classe qui g�re la r�putation 
/// </summary>
public class Reputation : MonoBehaviour
{
    public int reputation=100;
    public int reputationMax=100;
    public ReputationBar reputationBar;
    void Start()
    {
        UpdateBar();
    }

    /*private void Update()
    {
        addReputation(-1);
    }*/


    /// <summary>
    /// ajoute une valeur a la reputation 
    /// </summary>
    /// <param name="n">valeur a ajout�e a la reputation actuel</param>
    void addReputation(int n) 
    {
        reputation += n;

        if (reputation > reputationMax) { reputation=reputationMax; }
        if (reputation <=0) { GameOver(); reputation = 0; }
        UpdateBar();
        

    }

    /// <summary>
    /// afiche la repution actuel sur la barre 
    /// </summary>
    void UpdateBar()
    {
        reputationBar.SetRepution((0f+reputation)/reputationMax);
    }

    void GameOver()
    {
        //To do
    }

}
