// Namespace imports
using Data;
using Managers;

// Unity imports
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
	public class EmailUI: Draggable, IEndDragHandler {
		[Header("Prefab References")]
		[SerializeField] private Scrollbar scrollbar;
		[SerializeField] private RectTransform openedLetter;
		[SerializeField] private RectTransform closedLetter;
		[SerializeField] private ScrollRect scrollRect;

		[SerializeField] private TMP_Text addressTMPText;
		[SerializeField] private TMP_Text headerTMPText;
		[SerializeField] private TMP_Text bodyTMPText;
		[SerializeField] private TMP_Text footerTMPText;

		[Space] [Header("Drop stuff")]
		[SerializeField] private RectTransform openArea;
		[SerializeField] private RectTransform closeArea;
		[SerializeField] private RectTransform trashCan;
		private bool _isMailReadable;

		[Header("Animation")]
		[SerializeField] private GameObject animationImage;

		private void Start() {
			Invoke(nameof(AnimationStart), 10);
		}
		
		private void AnimationStart() {
			animationImage.SetActive(true);
		}

		private void AnimationStop() {
			CancelInvoke();
			animationImage.SetActive(false);
		}
		

		public void UpdateMailInfos(Email email) {
			// update e-mail content
			addressTMPText.text = email.Address;
			headerTMPText.text = email.Header;
			bodyTMPText.text = email.Body;
			footerTMPText.text = email.Footer;

			// force scrollbar update
			scrollbar.size /= 2;
			scrollbar.size *= 2;
			
			// scroll to the top of the letter
			scrollRect.normalizedPosition = new Vector2(0, 1);
		}

		public void Open() {
			_isMailReadable = true;
			openedLetter.gameObject.SetActive(true);
			closedLetter.gameObject.SetActive(false);
		}

		public void Close() {
			_isMailReadable = false;
			openedLetter.gameObject.SetActive(false);
			closedLetter.gameObject.SetActive(true);
		}

		public void Toggle() {
			if (_isMailReadable) Open();
			else Close();
		}

		public override void OnBeginDrag(PointerEventData eventData) {
			base.OnBeginDrag(eventData);
			AnimationStop();
		}

		public void OnEndDrag(PointerEventData eventData) {
			if (Overlaps(trashCan, eventData)) {
				GameManager.Instance.AnswerCheck(false);
			} else if (Overlaps(closeArea, eventData)) {
				if (!_isMailReadable) return;  // mail already closed
				Close();
				closedLetter.position = closeArea.position;
				return;
			} else if (Overlaps(openArea, eventData)) {
				if (_isMailReadable) return;  // mail already opened
				Open();
				return;
			}
			// if the dropped position is not valid
			rectTransform.anchoredPosition = originalPosition;
		}

		private bool Overlaps(RectTransform area, PointerEventData eventData) {
			return RectTransformUtility.RectangleContainsScreenPoint(area, eventData.position, canvas.worldCamera);
		}
	}
}