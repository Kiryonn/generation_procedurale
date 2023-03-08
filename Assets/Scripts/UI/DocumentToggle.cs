using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// afiche la documentation ou la chache quand on clique sur l'objet
/// </summary>
public class DocumentToggle : MonoBehaviour
{

    private GameObject hide;
    private bool isActif;

    private void Start()
    {
        isActif = false;
    }

    public void Click(BaseEventData data)
    {
        isActif = !isActif;
        hide.SetActive(isActif);
    }

}
