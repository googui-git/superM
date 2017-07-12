using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class RepeatPressEventTrigger:MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

	void Start ()
	{
	}

	void Update ()
	{
		
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Main.Instance.setLog ("OnPointerEnter:" + name);

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

	public void OnPointerExit (PointerEventData eventData)
	{
		Main.Instance.setLog ("OnPointerExit:" + name);
	}
}