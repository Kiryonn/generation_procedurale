using System.Linq;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
	public class EmailUI: Draggable, IEndDragHandler
	{
		[Header("Prefab Stuff")] [SerializeField]
		private Scrollbar scrollbar;

		[SerializeField] private VerticalLayoutGroup textArea;
		[SerializeField] private TMP_Text addressTMPText;
		[SerializeField] private TMP_Text headerTMPText;
		[SerializeField] private TMP_Text bodyTMPText;
		[SerializeField] private TMP_Text footerTMPText;

		[Space] [Header("Drop stuff")]
		[SerializeField] private RectTransform[] droppableAreas = new RectTransform[2];
		private RectTransform _currentPlaceHolder;
		private bool _isMailReadable = false;
		private RectTransform _letter;
		private RectTransform _warp;

		private void Start()
		{
			_letter = transform.Find("Open").GetComponent<RectTransform>();
			_warp = transform.Find("Close").GetComponent<RectTransform>();
			_currentPlaceHolder = droppableAreas[0];
		}

		/// <summary>
		/// updates the content and display of the e-mail
		/// </summary>
		/// <param name="email">the e-mail to display</param>
		public void UpdateMailInfos(Email email)
		{
			// update e-mail content
			addressTMPText.text = email.Address;
			headerTMPText.text = email.Header;
			bodyTMPText.text = email.Body;
			footerTMPText.text = email.Footer;

			// force the update of the textArea
			// EXTREMELY IMPORTANT DO NOT TOUCH
			Canvas.ForceUpdateCanvases();
			textArea.enabled = false;
			textArea.enabled = true;

			// scroll to the top of the letter
			scrollbar.value = 1;
		}

		public void Open()
		{
			_isMailReadable = true;
			_letter.gameObject.SetActive(true);
			_warp.gameObject.SetActive(false);
		}

		public void Close()
		{
			_isMailReadable = false;
			_letter.gameObject.SetActive(false);
			_warp.gameObject.SetActive(true);
		}

		public void Toggle()
		{
			_isMailReadable ^= true;
			if (_isMailReadable) Open();
			else Close();
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			// check if there is a restriction
			if (droppableAreas.Length == 0) return;

			// Check if the e-mail is dropped above one of the droppableAreas
			foreach (RectTransform droppableArea in droppableAreas)
			{
				if (!RectTransformUtility.RectangleContainsScreenPoint(droppableArea,eventData.position, canvas.worldCamera)) continue;
				if (_currentPlaceHolder == droppableArea) return;
				_currentPlaceHolder = droppableArea;
				Toggle();
				return;
			}

			// return the e-mail to its original position
			rectTransform.anchoredPosition = originalPosition;
		}
	}
}