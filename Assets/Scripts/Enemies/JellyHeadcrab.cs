using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JellyHeadcrab : MonoBehaviour
{
	private HealthManagement player;
	private bool isLunging = false;
	private Rigidbody2D rb;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManagement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isLunging && Vector2.Distance(transform.position, player.transform.position) < 7)
		{
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
		yield return new WaitForSeconds(0.25f);
	}

	IEnumerator Lunge()
	{
		isLunging = true;
		Vector3 target = player.transform.position;
		yield return StartCoroutine(RotateToPlayer(target));
		float forceStrength = 300f * (Vector2.Distance(transform.position, player.transform.position) / 7);
		rb.AddForce(forceStrength * transform.right, ForceMode2D.Impulse);
		yield return new WaitForSeconds(2);
		isLunging = false;
		Debug.Log("Done!");
	}
}
