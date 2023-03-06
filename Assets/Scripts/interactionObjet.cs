using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class interactionObjet : MonoBehaviour
{

    public GameObject trace_Verte;
    public GameObject trace_Rouge;
    [SerializeField]
    private Canvas canvas;
    Vector2 positionBase;

    private void Start()
    {
        
        positionBase=transform.position;
    }

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        (RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);
        
        transform.position = canvas.transform.TransformPoint(position);
        
    }

    public void DropHandler(BaseEventData data)
    {


        transform.position = positionBase;

    }


}
