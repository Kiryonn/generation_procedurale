using UnityEngine;
using UnityEngine.UI;

public class AnimationMail : MonoBehaviour
{
    [SerializeField] private RectTransform closedLetter;
    [Header("Hint animation for players")]
    [SerializeField] private float hintDelay;
    [SerializeField] private float hintDuration;
    [SerializeField] private Image imageDrag;
    private float _counter;
    public bool actif;
    void Update()
    {
        if(actif)
        { 
        _counter += Time.deltaTime;

        if (!(_counter > hintDelay)) return;
        imageDrag.gameObject.SetActive(true);
        imageDrag.rectTransform.position += new Vector3(1, 0, 0);

        if (!(_counter - hintDelay > hintDuration)) return;
        _counter = 0;
        imageDrag.rectTransform.position = closedLetter.position;
        imageDrag.gameObject.SetActive(false);
        }
        else{
            _counter = 0;
            imageDrag.rectTransform.position = closedLetter.position;
            imageDrag.gameObject.SetActive(false);
        }
    }
}
