using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	[NonSerialized] public RectTransform rectTransform;
	[NonSerialized] public Canvas canvas;
	[NonSerialized] public Vector2 originalPosition;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		originalPosition = rectTransform.anchoredPosition;
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}
}