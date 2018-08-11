﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        dir = speed * Time.deltaTime * Vector2.ClampMagnitude(dir, 1);
        rb.MovePosition(transform.position + dir);
    }

    void LateUpdate()
    {
        transform.position.Set(Mathf.Round(rb.position.x / 16) * 16, Mathf.Round(rb.position.y / 16) * 16,
            0);
    }
}