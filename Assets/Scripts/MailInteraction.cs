using UnityEngine;
using UnityEngine.EventSystems;

public class MailInteraction : MonoBehaviour
{

	[SerializeField]
	private Canvas canvas;
	private Vector2 _basePosition;
	public GameObject eMail;
	public RectTransform rightArea;

	private void Start()
	{
		_basePosition = transform.position;
	}

	/// <summary>
	/// appel�e pendant le drag
	/// </summary>
	/// <param name="data"></param>
	public void DragHandler(BaseEventData data)
	{
		PointerEventData pointerData = (PointerEventData) data;

		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			(RectTransform) canvas.transform,
			pointerData.position,
			canvas.worldCamera,
			out Vector2 position);

		transform.position = canvas.transform.TransformPoint(position);
	}


	public void DropHandler(BaseEventData data)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			(RectTransform) canvas.transform,
			((PointerEventData) data).position,
			canvas.worldCamera,
			out Vector2 position);

		var isEmailVisible = rightArea.rect.xMin <= position.x;
		eMail.SetActive(isEmailVisible);
		transform.position = _basePosition;
		gameObject.SetActive(!isEmailVisible);
	}


}