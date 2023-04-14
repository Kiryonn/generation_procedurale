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
	}

	public void OnEndDrag(PointerEventData eventData) {
		top.gameObject.SetActive(false);
		front.gameObject.SetActive(true);

		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			rectTransformToCheck,
			eventData.position,
			eventData.pressEventCamera,
			out Vector2 localPoint);

		if (rectTransformToCheck.rect.Contains(localPoint))
			GameManager.Instance.mailValider(true);

		transform.position = originalPosition;
	}
}