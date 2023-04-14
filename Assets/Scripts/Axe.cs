using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Axe: Draggable, IEndDragHandler {
	[SerializeField] private RectTransform[] droppableAreas;

	public void OnEndDrag(PointerEventData eventData) {
		if (droppableAreas.Any(area => RectTransformUtility.RectangleContainsScreenPoint(area, eventData.position, canvas.worldCamera)))
			return;
		rectTransform.anchoredPosition = originalPosition;
	}
}