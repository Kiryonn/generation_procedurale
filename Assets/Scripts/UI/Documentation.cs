using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Documentation : MonoBehaviour
{
    public TextMeshProUGUI titre;
    public TextMeshProUGUI text;
    public DocumentStrutuDonne ficheTec;
    public Button Left;
    public Button Right;
    int ficherActuel;
    private void Start()
    {
        aficher(ficherActuel);
    }
    public void right() 
    {
        aficher(ficherActuel + 1);
    }
    public void left() 
    {
        aficher(ficherActuel - 1);
    }

    void aficher(int n)
    {
        ficherActuel = n;
        titre.text=ficheTec.typeDeTechnique[ficherActuel].Titre;
        text.text = ficheTec.typeDeTechnique[ficherActuel].text;
        afficheButon();
    }

    void afficheButon() 
    {
        if(ficherActuel== ficheTec.typeDeTechnique.Count-1) { Right.interactable = false; }
        else { Right.interactable = true; }

        if (ficherActuel == 0) { Left.interactable = false; }
        else { Left.interactable = true; }
    }
}
