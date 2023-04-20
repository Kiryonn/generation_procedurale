using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler {
	[SerializeField] protected Canvas canvas;
	protected RectTransform rectTransform;
	protected Vector2 originalPosition;

	private void Awake() { rectTransform = GetComponent<RectTransform>(); }

	public virtual void OnBeginDrag(PointerEventData eventData) { originalPosition = rectTransform.anchoredPosition; }

	public void OnDrag(PointerEventData eventData) { rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; }
}