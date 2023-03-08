using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// inverse l'etat du game objet quand on click 
/// </summary>
public class interactionDocument : MonoBehaviour
{

    //[SerializeField]
    //private Canvas canvas;
    //Vector2 positionBase;

    public GameObject autreEtat;
    bool isActif;

    private void Start()
    {
        isActif = false;

        //positionBase=transform.position;
    }

    public void Click(BaseEventData data)
    {
        isActif = !isActif;
        autreEtat.SetActive(isActif);
        


    }



}
