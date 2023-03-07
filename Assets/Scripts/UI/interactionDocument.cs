using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class interactionDocument : MonoBehaviour
{

    //[SerializeField]
    //private Canvas canvas;
    //Vector2 positionBase;

    public interactionDocument autreEtat;

    private void Start()
    {
        
        //positionBase=transform.position;
    }

    public void Click(BaseEventData data)
    {

        autreEtat.transform.gameObject.SetActive(true);
        gameObject.SetActive(false);


    }



}
