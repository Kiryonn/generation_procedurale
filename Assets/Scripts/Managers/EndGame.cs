using Data;
using Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public EmailUI email;
    public GameManager gameManager;
    public TextMeshProUGUI nextDayTextButon;
    public TextMeshProUGUI nombreErreu;
    public TextMeshProUGUI currentErreu;

    public Button mainMenu;
    public Button suivant;

    public void activeButon() 
    {
        mainMenu.interactable = true;
        suivant.interactable = true;
    }
    public void disabledButon() 
    {
        mainMenu.interactable = false;
        suivant.interactable = false;
    }
    public void Defeat()
    {
        nextDayTextButon.text = "recomencée le niveau";
        nombreErreu.text = "" + 4;
        disabledButon();
    }

    public void Victory()
    {
        nextDayTextButon.text = "niveau suivant";
        nombreErreu.text = "" + 4;
        disabledButon();
    }

        public void buttonNext() 
    {
        //chargée mail suivant
        currentErreu.text = "" + 2;
        activeButon();
    }

        public void buttonBack()
    {
        //chargée mail precendent 
        currentErreu.text = "" + 1;
    }


}
