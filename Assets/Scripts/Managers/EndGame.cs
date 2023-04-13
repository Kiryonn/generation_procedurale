using System;
using Data;
using System.Collections.Generic;
using Managers;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {
    public EmailUI email;
    public GameManager gameManager;
    public TextMeshProUGUI nextDayTextButon;
    public TMP_Text mailInfoLabel;

    public Button mainMenu;
    public Button next;
    public Button R;
    public Button L;
    private int _mailIndex = 0;
    List<Email> Email;

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void listEmail(List<Email> list) 
    {
        Email = list;
        _mailIndex = 0;
        //_mailIndex = Email.Count;
    }

    public void ActiveButton() {
        mainMenu.interactable = true;
        
    }
    public void DisabledButton() {
        mainMenu.interactable = true;
        next.interactable = false;
        L.interactable = false;
        R.interactable = true;
    }
    public void Defeat() {
        _mailIndex=0;
        nextDayTextButon.text = "Recomencer le niveau";
        UpdateMailReviewLabel();
        DisabledButton();
    }

    public void Victory() {
        _mailIndex = 0;
        nextDayTextButon.text = "niveau suivant";
        UpdateMailReviewLabel();
        DisabledButton();
    }

    public void buttonNext()
    {
        _mailIndex++;
        UpdateMailReviewLabel();
        //ActiveButton();
        if(_mailIndex== Email.Count-1)
        {
            next.interactable = true;
            L.interactable = true;
            R.interactable = false;
        }
        else
        {
            L.interactable = true;
        }
    }

    public void buttonBack()
    {
        _mailIndex--;
        UpdateMailReviewLabel();
        if (_mailIndex == 0)
        {
            L.interactable = false;
            R.interactable = true;
        }
        else
        {
            R.interactable = true;
        }
    }

    private void UpdateMailReviewLabel()
    {
        mailInfoLabel.text = "Revue de mail "+_mailIndex+"/"+Email.Count;
        email.UpdateMailInfos(Email[_mailIndex]);
    }
}
