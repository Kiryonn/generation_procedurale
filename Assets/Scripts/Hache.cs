using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hache : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Canvas canvas;
    public Image devant;
    private Vector2 basePosition;
    public RectTransform rectTransformToCheck;


    private void Start()
    {
        basePosition = transform.position;

    }

    public void DragHandler(BaseEventData data)
    {
        devant.gameObject.SetActive(true);

        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        (RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void DropHandler(BaseEventData data)
    {
        devant.gameObject.SetActive(false);

        PointerEventData eventData = (PointerEventData)data;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransformToCheck,
            eventData.position, 
            eventData.pressEventCamera, 
            out Vector2 localPoint);

        if (rectTransformToCheck.rect.Contains(localPoint))
        {
            //Debug.Log("mail valid�e");
        }



        transform.position = basePosition;
    }
}