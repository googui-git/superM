using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class play : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerEnter(PointerEventData eventData)
    {
		Music.Instance.play(transform.name);
		GetComponent<Image> ().color = Color.red;
    }

	public void OnPointerExit(PointerEventData eventData)
	{
		//Music.Instance.play(transform.name);
		GetComponent<Image> ().color = Color.white;
	}
}
