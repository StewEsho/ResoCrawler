using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManagement : MonoBehaviour
{
	private int maxHealth = 50;
	private int health;
	private ResizeScreen rs;
	private ItemManagement im;
	private PlayerMovement pm;
	private Animator anim;
	public GameObject DiedPanel;

	private bool canRestart;

	// Use this for initialization
	void Start ()
	{
		health = maxHealth;
		rs = Camera.main.gameObject.GetComponent<ResizeScreen>();
		im = GetComponentInChildren<ItemManagement>();
		pm = GetComponentInChildren<PlayerMovement>();
		anim = GetComponentInChildren<Animator>();
		if (rs == null)
		{
			Debug.LogError("Camera does not have ResizeScreen Component");
		}
		else
		{
			rs.SetUnits(health);
		}
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire") && canRestart)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void AdjustHealth(int amount)
	{
		health += amount;
		Debug.Log("Health: " + health);
		rs.SetUnits(health);
		if (IsDead())
		{
			StartCoroutine(Die());
		}
	}

	public bool IsDead()
	{
		return health <= 0;
	}

	public int GetHealth()
	{
		return health;
	}

	private IEnumerator Die()
	{
		im.ClearInventory();
		im.enabled = false;
		pm.enabled = false;
		rs.SetUnits(4);
		Screen.SetResolution(800, 800, false);
		anim.Play("Death");
		yield return new WaitForSeconds(1.5f);
		DiedPanel.SetActive(true);
		canRestart = true;
	}
}
