using UnityEngine;
using UnityEngine.EventSystems;

public class MailInteraction : MonoBehaviour
{

    [SerializeField]
    private Canvas canvas;
    private Vector2 basePosition;
    private bool EmailDisplay;
    public GameObject eMail;

    private void Start()
    {
        basePosition = transform.position;
        EmailDisplay = false;
    }

    /// <summary>
    /// appelée pendant le drag
    /// </summary>
    /// <param name="data"></param>
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        (RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    /// <summary>
    /// appelée apres le drop
    /// </summary>
    /// <param name="data"></param>
    public void DropHandler(BaseEventData data)
    {
        if (EmailDisplay)
        {
            eMail.SetActive(true);
            transform.position = basePosition;
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = basePosition;
        }
    }

    /// <summary>
    /// le pointer est entrée dans la zone de droite
    /// </summary>
    /// <param name="data"></param>
    public void enterPointerRight(BaseEventData data)
    {
        EmailDisplay = true;
        Debug.Log("true");
    }

    /// <summary>
    /// le pointer est entrée dans la zone de gauche
    /// </summary>
    /// <param name="data"></param>
    public void enterPointerLeft(BaseEventData data)
    {
        EmailDisplay = false;
        Debug.Log("false");
    }


}
