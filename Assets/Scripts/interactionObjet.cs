using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionObjet : MonoBehaviour
{

    public GameObject trace_Verte;
    public GameObject trace_Rouge;


    bool active = false;


    void Start()
    {

    }


    void OnMouseUp()
    {
        active = false;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Instantiate(trace_Verte, mousePosition+new Vector3(0,0,10),Quaternion.identity);
    }

    void OnMouseDown()
    {
        active = true;
    }    

    private void Update()
    {
        if(active)
        { 
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        transform.position = new Vector3(mousePosition.x, mousePosition.y, -1);
        
        }
    }


}
