using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tampon : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Canvas canvas;
    public Image top;
    public Image front;
    private Vector2 basePosition;
    public RectTransform rectTransformToCheck;


    private void Start()
    {
        basePosition = transform.position;

    }

    public void DragHandler(BaseEventData data)
    {
        top.gameObject.SetActive(true);
        front.gameObject.SetActive(false);

        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        (RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void DropHandler(BaseEventData data)
    {
        top.gameObject.SetActive(false);
        front.gameObject.SetActive(true);
         

        /*

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        (RectTransform)canvas.transform,
        ((PointerEventData)data).position,
        canvas.worldCamera,
        out Vector2 position);

        Debug.Log("position: " + position.x);
        var isEmailVisible = rightArea.rect.Contains(position);*/

        PointerEventData eventData = (PointerEventData)data;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransformToCheck,
            eventData.position, 
            eventData.pressEventCamera, 
            out Vector2 localPoint);

        if (rectTransformToCheck.rect.Contains(localPoint))
        {
            //Debug.Log("mail validée");
        }



        transform.position = basePosition;
    }
}
