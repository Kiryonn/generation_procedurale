using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tampon: Draggable, IEndDragHandler
{
	[SerializeField] private Image top;
	[SerializeField] private Image front;
	[SerializeField] private RectTransform stampArea;

	public override void OnBeginDrag(PointerEventData eventData) {
		base.OnBeginDrag(eventData);
		top.gameObject.SetActive(true);
		front.gameObject.SetActive(false);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
			top.rectTransform.parent as RectTransform,
			eventData.position,
			null,
			out Vector2 localMousePosition);

        // center the rubber stamp on the mouse
        top.rectTransform.localPosition = localMousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
        top.gameObject.SetActive(false);
		front.gameObject.SetActive(true);

		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			stampArea,
			eventData.position,
			eventData.pressEventCamera,
			out Vector2 localPoint);

		if (stampArea.rect.Contains(localPoint))
			GameManager.Instance.AnswerCheck(true);

        rectTransform.anchoredPosition = originalPosition;
    }

}