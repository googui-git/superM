using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CollisionDetector : MonoBehaviour
{
	private float speed = 0.5f;
	private Vector2 startPoint1;
	private Vector2 startPoint2;

	private RaycastHit2D hit;
	private float radius;
	private Vector2 a = new Vector2 (-1f, 1f);

	private bool isMove = false;

	void Start ()
	{
		
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		radius = renderer.bounds.size.x / 2;
	}

	void Update ()
	{

		startPoint1 = new Vector2 (transform.position.x, transform.position.y + radius * a.y);
		startPoint2 = new Vector2 (transform.position.x + radius * a.x, transform.position.y);

		Debug.DrawRay (startPoint1, a * 10f, Color.red);
		Debug.DrawRay (startPoint2, a * 10f, Color.red);

		Move ();

		//if (!IsCollidingVertically ()) {
		//transform.Translate (a * MovingForce * Time.deltaTime);
		//}
	}

	void Move ()
	{
		if (isMove) {
			return;
		}
		isMove = true;

		float distance = 0;
		Vector2 to = Vector2.zero;
		Vector2 temp = Vector2.zero;

		hit = Physics2D.Raycast (startPoint1, a, 100f);
		if (hit != null && hit.collider != null) {
			distance = Vector2.Distance (startPoint1, hit.point);
			temp = new Vector2 (a.x, -a.y);
			to = hit.point + temp * radius;
		}

		hit = Physics2D.Raycast (startPoint2, a, 100f);
		if (hit != null && hit.collider != null) {
			if (Vector2.Distance (startPoint2, hit.point) < distance) {
				temp = new Vector2 (-a.x, a.y);
				to = hit.point + temp * radius;
			}
		}

		a = temp;

		Debug.Log (transform.position + "|" + to);

		transform.DOMove (to, speed).SetEase (Ease.Linear).OnComplete (new TweenCallback (delegate() {
			isMove = false;
		}));
	}
}
