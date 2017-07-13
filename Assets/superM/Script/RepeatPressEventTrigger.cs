using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class RepeatPressEventTrigger:MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

	void Start ()
	{
	}

	void Update ()
	{
		
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		Main.Instance.setLog ("OnPointerDown:" + name);

		if (name.Equals ("Up")) {
			Main.Instance.MoveTop ();
		} else if (name.Equals ("Down")) {
			Main.Instance.MoveDown ();
		} else if (name.Equals ("Left")) {
			Main.Instance.MoveLeft ();
		} else if (name.Equals ("Right")) {
			Main.Instance.MoveRight ();
		}
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		Main.Instance.setLog ("OnPointerUp:" + name);
	}
}