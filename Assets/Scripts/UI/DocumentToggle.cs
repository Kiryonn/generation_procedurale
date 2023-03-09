using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// afiche la documentation ou la chache quand on clique sur l'objet
/// </summary>
public class DocumentToggle : MonoBehaviour
{

    private GameObject _toHide;
    private bool _isActive;

    private void Start()
    {
        _isActive = false;
    }

    public void Click()
    {
        _isActive = !_isActive;
        _toHide.SetActive(_isActive);
    }

}
