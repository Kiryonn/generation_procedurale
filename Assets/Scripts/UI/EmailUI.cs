// Namespace imports
using Data;

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
		[SerializeField] private VerticalLayoutGroup textArea;
		
		[SerializeField] private TMP_Text addressTMPText;
		[SerializeField] private TMP_Text headerTMPText;
		[SerializeField] private TMP_Text bodyTMPText;
		[SerializeField] private TMP_Text footerTMPText;
		

		[Space] [Header("Drop stuff")]
		[SerializeField] private RectTransform[] openAreas;
		[SerializeField] private RectTransform[] closeAreas;
		private bool _isMailReadable;
		
		[Header("Hints for players")]
		[SerializeField] private float hintDelay;
        [SerializeField] private float Duree;
        private bool _hasInteracted;
		private float _counter;

		[Header("animation")]
		public Image imageDrag;

        private void Update() {
			_counter += Time.deltaTime;

			if (_counter > hintDelay) {
				imageDrag.gameObject.SetActive(true);
                imageDrag.rectTransform.position += new Vector3( 1, 0, 0);

                if (_counter-hintDelay > Duree) 
				{
					_counter = 0;
					imageDrag.rectTransform.position = closedLetter.position;
                    imageDrag.gameObject.SetActive(false);
                }
            }
		}

		public void UpdateMailInfos(Email email) {
			// update e-mail content
			addressTMPText.text = email.Address;
			headerTMPText.text = email.Header;
			bodyTMPText.text = email.Body;
			footerTMPText.text = email.Footer;

			scrollbar.size += 0.1f;
			scrollbar.size -= 0.1f;
			
			Invoke(nameof(RefreshContent), 0.1f);
			
			// scroll to the top of the letter
			scrollbar.value = 0;
			
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

		public void OnEndDrag(PointerEventData eventData) {
			// check if the drop position is within a valid area
			foreach (RectTransform droppableArea in closeAreas) {
				if (!Overlaps(droppableArea, eventData)) continue;
				if (!_isMailReadable) return;  // mail already closed
				Close();
				closedLetter.position = droppableArea.position;
				return;
			}
			foreach (RectTransform droppableArea in openAreas) {
				if (!Overlaps(droppableArea, eventData)) continue;
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

		private void RefreshContent() {
			ContentSizeFitter contentSizeFitter = textArea.GetComponent<ContentSizeFitter>();
			RectTransform textAreaRectTransform = textArea.GetComponent<RectTransform>();

			Canvas.ForceUpdateCanvases();
			LayoutRebuilder.ForceRebuildLayoutImmediate(textAreaRectTransform);
			
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
			Canvas.ForceUpdateCanvases();
			LayoutRebuilder.ForceRebuildLayoutImmediate(textAreaRectTransform);
			
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			Canvas.ForceUpdateCanvases();
			LayoutRebuilder.ForceRebuildLayoutImmediate(textAreaRectTransform);
			
			textArea.enabled = false;
			Canvas.ForceUpdateCanvases();
			LayoutRebuilder.ForceRebuildLayoutImmediate(textAreaRectTransform);
			
			textArea.enabled = true;
		}
	}
}