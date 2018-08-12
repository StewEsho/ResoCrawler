using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JellyHeadcrab : MonoBehaviour
{
	private HealthManagement player;
	private bool isLunging;
	private bool isMoving = true;
	private Rigidbody2D rb;

	private Vector2 displacement;
	private float angle;
	private float angleDiff = 0;
	
	// Use this for initialization
	void Start ()
	{
		angle = Random.Range(0, 360);
//		displacement = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
		transform.eulerAngles = new Vector3(0, 0, angle);
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManagement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isMoving)
		{
			rb.AddForce(185 * transform.right, ForceMode2D.Force);
			angleDiff += Random.value - (0.5f + Mathf.Sign(angleDiff)/20f);
			transform.Rotate(new Vector3(0, 0, angleDiff));
		}
		if (!isLunging && Vector2.Distance(transform.position, player.transform.position) < 7)
		{
			isLunging = true;
			isMoving = false;
			StartCoroutine(Lunge());
		}
	}

	IEnumerator RotateToPlayer(Vector3 target)
	{
		Debug.Log("BEGUN ROTATING.");
		Vector2 dir = target - transform.position;
		Vector2 perpindicularDir = new Vector2(-dir.y, dir.x);
		Quaternion desiredRotation = Quaternion.LookRotation(transform.forward, perpindicularDir);
		do
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.33f);
			yield return null;
		} while (Quaternion.Angle(transform.rotation, desiredRotation) > 1f);
		Debug.Log("DONE ROTATING.");
		yield return new WaitForSeconds(0.5f);
	}

	IEnumerator Lunge()
	{
		Vector3 target = player.transform.position;
		yield return StartCoroutine(RotateToPlayer(target));
		float forceStrength = 300f * (Vector2.Distance(transform.position, target) / 7);
		rb.AddForce(forceStrength * transform.right, ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.25f);
		isMoving = true;
		yield return new WaitForSeconds(2);
		isLunging = false;
		Debug.Log("Done!");
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("LOL");
		if (other.transform.CompareTag("Player"))
		{
			//assuming there is only one player:
			player.AdjustHealth(-3);
		}
	}
}
