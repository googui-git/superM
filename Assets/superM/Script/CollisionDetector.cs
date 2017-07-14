using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CollisionDetector : MonoBehaviour
{
	private float speed = 5f;
	private Vector2 startPointX;
	private Vector2 startPointY;

	private float radius;
	private Vector2 direction = new Vector2 (-1f, 1f);

	private bool isMove = false;

	void Start ()
	{
		
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		radius = renderer.bounds.size.x / 2;
	}

	void Update ()
	{

		startPointX = new Vector2 (transform.position.x, transform.position.y + radius * direction.y);
		startPointY = new Vector2 (transform.position.x + radius * direction.x, transform.position.y);

		Debug.DrawRay (startPointX, direction * 10f, Color.red);
		Debug.DrawRay (startPointY, direction * 10f, Color.red);

		Move ();
	}

	void Move ()
	{
		if (isMove) {
			return;
		}
		isMove = true;

		float distanceX = 0;
		float distanceY = 0;
		Vector2 to = Vector2.zero;
		float duration = speed;

		RaycastHit2D hitX = Physics2D.Raycast (startPointX, direction, 100f);
		if (hitX != null && hitX.collider != null) {
			distanceX = Vector2.Distance (startPointX, hitX.point);
		}

		RaycastHit2D hitY = Physics2D.Raycast (startPointY, direction, 100f);
		if (hitY != null && hitY.collider != null) {
			distanceY = Vector2.Distance (startPointY, hitY.point);
		}

		if (distanceY < distanceX) {
			direction.Set (-direction.x, direction.y);
			to = hitY.point + direction * radius;
			duration = distanceY / speed;
		} else {
			direction.Set (direction.x, -direction.y);
			to = hitX.point + direction * radius;
			duration = distanceX / speed;
		}

		transform.DOMove (to, duration).SetEase (Ease.Linear).OnComplete (new TweenCallback (delegate() {
			isMove = false;
		}));
	}
}
