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

        // Construire le texte à afficher en fonction des règles actives
        string rulesActiveText = "Règles actives : \n";
        if (rules.HasFlag(Rules.InvalidAddress))
            rulesActiveText += "Mauvaise adresse \n";
        if (rules.HasFlag(Rules.IncorrectSpelling))
            rulesActiveText += "IncorrectSpelling\n";//todo
        if (rules.HasFlag(Rules.PersonalData))
            rulesActiveText += "PersonalData\n";//todo
        if (rules.HasFlag(Rules.FishyLink))
            rulesActiveText += "lien piégé \n";
        if (rules.HasFlag(Rules.ExageratedMail))
            rulesActiveText += "Mail joant sur les sentiment\n";

        // Afficher le texte des règles actives
        _rulesText.text = rulesActiveText;


    }
}
