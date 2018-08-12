using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour, IFireable
{
	[SerializeField] protected int Damage;
	[SerializeField] protected int DamageVariance = 2;
	[SerializeField] protected float FireRate = 0.1f;
	protected bool CanFire = true;
	protected Animator Anim;
	protected AudioSource AudioSource;
	protected Collider2D Col;
	[HideInInspector] public float DamageMultiplier = 1.0f;
	
	// Use this for initialization
	void Start ()
	{
		DamageMultiplier = 1.0f;
		Damage += Random.Range(-DamageVariance, DamageVariance);
		Damage = Mathf.Max(Damage, 1);
		Anim = GetComponentInChildren<Animator>();
		AudioSource = GetComponent<AudioSource>();
		Col = GetComponentInChildren<Collider2D>();
	}
	
	void OnDisable()
	{
		CanFire = true;
	}

	public abstract IEnumerator Fire();
}
