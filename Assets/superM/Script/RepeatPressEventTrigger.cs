using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class RepeatPressEventTrigger:MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

	private bool isPointerDown = false;

	void Start ()
	{
	}

	void Update ()
	{
		if (isPointerDown) {
			if (name.Equals ("Up")) {
				Main.Instance.MoveUp ();
			} else if (name.Equals ("Down")) {
				Main.Instance.MoveDown ();
			} else if (name.Equals ("Left")) {
				Main.Instance.MoveLeft ();
			} else if (name.Equals ("Right")) {
				Main.Instance.MoveRight ();
			}
		}
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		Main.Instance.setLog ("OnPointerDown:" + name);
		isPointerDown = true;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		Main.Instance.setLog ("OnPointerUp:" + name);
		isPointerDown = false;
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		Main.Instance.setLog ("OnPointerExit:" + name);
		isPointerDown = false;
	}

}