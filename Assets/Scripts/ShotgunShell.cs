using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShell : MonoBehaviour
{
	private Rigidbody2D rb;

	void Start ()
	{
		Destroy(gameObject, 2);
		transform.Rotate(Vector3.forward * Random.Range(0, 90));
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(Quaternion.AngleAxis(Random.Range(-15, 15), Vector3.forward) * Vector2.up * 4f, ForceMode2D.Impulse);
	}
	
	void FixedUpdate () {
		rb.AddForce(12f * Vector3.down, ForceMode2D.Force);
	}
}
