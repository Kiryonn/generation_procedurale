using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EmailUI: MonoBehaviour
    {
        [Header("Body Padding")]
        [SerializeField] private int bodyPaddingTop;
        [SerializeField] private int bodyPaddingLeft;
        [Header("Footer Padding")]
        [SerializeField] private int footerPaddingTop;
        [SerializeField] private int footerPaddingLeft;

        private Transform _textArea;
        private TMP_Text _addressPlaceholder;
        private TMP_Text _headerPlaceholder;
        private TMP_Text _bodyPlaceholder;
        private TMP_Text _footerPlaceholder;
        private void Start()
        {
            _textArea = transform.Find("ScrollableArea").Find("TextArea");
            _addressPlaceholder = _textArea.Find("Address").GetComponent<TMP_Text>();
            _headerPlaceholder = _textArea.Find("Header").GetComponent<TMP_Text>();
            _bodyPlaceholder = _textArea.Find("Body").GetComponent<TMP_Text>();
            _footerPlaceholder = _textArea.Find("Footer").GetComponent<TMP_Text>();
            GameObject truc = new GameObject();
            truc.transform.parent = transform;
            truc.name = "Miaou";
        }

        public void UpdateMailInfos(Data.Email email)
        {
            _addressPlaceholder.text = email.address;
            _headerPlaceholder.text = email.header;
            _bodyPlaceholder.text = email.body;
            _footerPlaceholder.text = email.footer;
            
            Vector2 bodyPlaceholderPosition = _bodyPlaceholder.transform.position;
            bodyPlaceholderPosition.y = _headerPlaceholder.preferredHeight + bodyPaddingTop;
            _bodyPlaceholder.transform.position = bodyPlaceholderPosition;
            
            Vector2 footerPlaceholderPosition = _footerPlaceholder.transform.position;
            bodyPlaceholderPosition.y = _bodyPlaceholder.preferredHeight + footerPaddingTop;
            _footerPlaceholder.transform.position = footerPlaceholderPosition;
        }
    }
}