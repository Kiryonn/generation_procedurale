using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int currentMail = 0;

    private int jour;
    public Reputation reputation;
    public float perteReputation;



    void Start()
    {
        
    }

    /// <summary>
    /// l'utilisateur a dit que le mail est bon
    /// </summary>
    public void valideMail() 
    {
        checkResult(true);
    }

    /// <summary>
    /// l'utilisateur a dit que le mail n'est pas bon
    /// </summary>
    public void refuseMail() 
    {
        checkResult(false);
    }

    /// <summary>
    /// verifie si la reponse du joeur est bonne 
    /// </summary>
    /// <param name="playerResponse"> reponse du joeur </param>
    void checkResult(bool playerResponse)
    {
        if (mailGood(currentMail)!= playerResponse) 
        {
            reputation.addReputation(perteReputation);
        }
        else 
        {
            //todo
        }
       
    }

    /// <summary>
    /// on veut savoir si notre mail est bon ou pas
    /// </summary>
    /// <param name="n">le numero du mail actuel</param>
    /// <returns></returns>
    bool mailGood(int n)
    { 
    //todo

    return true;
    }

}
