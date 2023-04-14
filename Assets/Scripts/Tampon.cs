using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tampon: Draggable, IEndDragHandler
{
	[SerializeField] private Image top;
	[SerializeField] private Image front;
	public RectTransform rectTransformToCheck;

	public override void OnBeginDrag(PointerEventData eventData) {
		base.OnBeginDrag(eventData);
		top.gameObject.SetActive(true);
		front.gameObject.SetActive(false);



        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(top.rectTransform.parent as RectTransform, eventData.position, null, out localMousePosition);

        // Centrer l'objet "top" sur la position de la souris
        top.rectTransform.localPosition = localMousePosition;

    }

	public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("x : " + eventData.position.x + " y : " + eventData.position.y);
        top.gameObject.SetActive(false);
		front.gameObject.SetActive(true);
		
			
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			rectTransformToCheck,
			eventData.position,
			eventData.pressEventCamera,
			out Vector2 localPoint);

		if (rectTransformToCheck.rect.Contains(localPoint))
			GameManager.Instance.mailValider(true);

        rectTransform.anchoredPosition = originalPosition;
    }

}