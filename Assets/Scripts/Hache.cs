using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hache : Draggable, IEndDragHandler {
	[SerializeField] private RectTransform[] dropableAreas;

	public void OnEndDrag(PointerEventData eventData) {
		if (dropableAreas.Any(area => RectTransformUtility.RectangleContainsScreenPoint(area, eventData.position, canvas.worldCamera)))
			return;
		rectTransform.anchoredPosition = originalPosition;
	}
}