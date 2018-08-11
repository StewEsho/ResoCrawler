using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        dir = speed * Time.deltaTime * Vector2.ClampMagnitude(dir, 1);
        transform.Translate(dir);
    }

    void LateUpdate()
    {
        transform.position.Set(Mathf.Round(transform.position.x / 16) * 16, Mathf.Round(transform.position.y / 16) * 16,
            0);
    }
}