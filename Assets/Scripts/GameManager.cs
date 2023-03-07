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

    public void valideMail() 
    {
        checkResult(true);
    }

    public void refuseMail() 
    {
        checkResult(false);
    }

    void checkResult(bool playerResponse)
    {
        if (mailGood(currentMail)!= playerResponse) 
        {
            reputation.addReputation(perteReputation);
        }
        else 
        {
            
        }
       
    }
    bool mailGood(int n)
    { 
    //todo

    return true;
    }

}
