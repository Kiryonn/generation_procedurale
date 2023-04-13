using UnityEngine;
using UnityEngine.UI;

public class Stamp : MonoBehaviour {
	public Image image;
	public Sprite stampOn;

	public void OnStamp() {
		image.sprite = stampOn;
		Destroy(this);
	}
}
