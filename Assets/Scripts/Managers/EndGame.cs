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
    private int _mailIndex = 1;
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
        _mailIndex = Email.Count;
    }

    public void ActiveButton() {
        mainMenu.interactable = true;
        next.interactable = true;
    }
    public void DisabledButton() {
        mainMenu.interactable = false;
        next.interactable = false;
    }
    public void Defeat() {
        nextDayTextButon.text = "Recomencer le niveau";
        UpdateMailReviewLabel();
        DisabledButton();
    }

    public void Victory() {
        nextDayTextButon.text = "niveau suivant";
        UpdateMailReviewLabel();
        DisabledButton();
    }

    public void buttonNext()
    {
        _mailIndex++;
        UpdateMailReviewLabel();
        ActiveButton();
    }

    public void buttonBack()
    {
        _mailIndex--;
        UpdateMailReviewLabel();
    }

    private void UpdateMailReviewLabel()
    {
        //mailInfoLabel.text = $"Revue de mail ({_mailIndex}/4)";
    }
}
