using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

/// <summary>
/// afiche la documentation ou la chache quand on clique sur l'objet
/// </summary>
public class DocumentToggle : MonoBehaviour
{

    public GameObject toHide;
    private bool _isActive;

    private void Start()
    {
        _isActive = false;
    }

    public void Click()
    {
        _isActive = !_isActive;
        toHide.SetActive(_isActive);
    }

}
