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
		GameM.Instance.setLog ("OnPointerEnter:" + name);

		if (name.Equals ("Up")) {
			GameM.Instance.MoveTop ();
		} else if (name.Equals ("Down")) {
			GameM.Instance.MoveDown ();
		} else if (name.Equals ("Left")) {
			GameM.Instance.MoveLeft ();
		} else if (name.Equals ("Right")) {
			GameM.Instance.MoveRight ();
		}
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		GameM.Instance.setLog ("OnPointerExit:" + name);
	}
}