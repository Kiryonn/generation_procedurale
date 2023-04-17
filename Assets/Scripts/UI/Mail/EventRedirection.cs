using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Mail {
	public class EventRedirection: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
		[SerializeField] private EmailUI mail;

		public void OnBeginDrag(PointerEventData eventData) { mail.OnBeginDrag(eventData); }
		public void OnDrag(PointerEventData eventData) { mail.OnDrag(eventData); }
		public void OnEndDrag(PointerEventData eventData) { mail.OnEndDrag(eventData); }
	}
}