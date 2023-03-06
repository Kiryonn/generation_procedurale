using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// classe qui gère la réputation 
/// </summary>
public class Reputation : MonoBehaviour
{
    public int reputation=100;
    public int reputationMax=100;
    ReputationBar reputationBar;
    void Start()
    {
        UpdateBar();
    }

    /// <summary>
    /// ajoute une valeur a la reputation 
    /// </summary>
    /// <param name="n">valeur a ajoutée a la reputation actuel</param>
    void addReputation(int n) 
    {
        reputation += n;
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
