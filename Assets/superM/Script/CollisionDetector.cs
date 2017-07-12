using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour
{
	public float MovingForce;
	Vector2 StartPoint1;
	Vector2 StartPoint2;

	RaycastHit2D HitInfo;
	float radius;
	Vector2 a = new Vector2 (-1f, 1f);

	void Start ()
	{
		
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		radius = renderer.bounds.size.x / 2;
	}

	void Update ()
	{

		StartPoint1 = new Vector2 (transform.position.x, transform.position.y + radius * a.y);
		StartPoint2 = new Vector2 (transform.position.x + radius * a.x, transform.position.y);

		Debug.DrawRay (StartPoint1, a, Color.red);
		Debug.DrawRay (StartPoint2, a, Color.red);

		if (!IsCollidingVertically ()) {
			transform.Translate (a * MovingForce * Time.deltaTime);
		}
	}

	bool IsCollidingVertically ()
	{
		HitInfo = Physics2D.Raycast (StartPoint1, a, 0.0000000000001f);
		if (HitInfo != null && HitInfo.collider != null) {
			a.Set(a.x, -a.y);
			return true;
		}

		HitInfo = Physics2D.Raycast (StartPoint2, a, 0.0000000000001f);
		if (HitInfo != null && HitInfo.collider != null) {
			a.Set(-a.x, a.y);
			return true;
		}
		return false;
	}
}
