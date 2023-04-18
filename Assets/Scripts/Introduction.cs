using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    private float _counter;

    [SerializeField] private TextMeshProUGUI _rulesText;
    [SerializeField] private TextMeshProUGUI Titre;

    [SerializeField] private float time;

    void Update()
    {
        _counter += Time.deltaTime;
        if (_counter > time)
        {
            end();
        }
    }

    private void end()
    {
        gameObject.SetActive(false);
        GameManager.Instance.Go();
    }

    public void Restart(Rules rules, int curentDay)
    {
        _counter = 0;
        Titre.text = "Jour      :  " + curentDay;

        // Construire le texte � afficher en fonction des r�gles actives
        string rulesActiveText = "R�gles actives : \n";
        if (rules.HasFlag(Rules.InvalidAddress))
            rulesActiveText += "Il y a de Mauvaise adresse email \n";
        if (rules.HasFlag(Rules.IncorrectSpelling))
            rulesActiveText += "Il y a des faute d'orthographe \n";//todo
        if (rules.HasFlag(Rules.PersonalData))
            rulesActiveText += "Il y a des demandes de donn�es personnelles \n";//todo
        if (rules.HasFlag(Rules.FishyLink))
            rulesActiveText += "Il y a des lien pi�g� \n";
        if (rules.HasFlag(Rules.ExageratedMail))
            rulesActiveText += "Il y a des mails faisant appel aux sentiments\n";

        // Afficher le texte des r�gles actives
        if (rulesActiveText.Length < 30) { rulesActiveText += "pas de regles actives"; }
        _rulesText.text = rulesActiveText;


    }
}
