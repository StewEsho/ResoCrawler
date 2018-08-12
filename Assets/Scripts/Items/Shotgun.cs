using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {
    
    [SerializeField] public GameObject Bullet;
    
    protected override void Shoot()
    {
        Vector2 dir = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1);
        if (dir.sqrMagnitude < 0.01f)
            dir = transform.right;
        for (int i = 0; i < 5; i++)
        {
            Vector2 bulletDir = Quaternion.AngleAxis(Random.Range(-6, 6), Vector3.forward) * dir;
            Debug.Log(bulletDir);
            GameObject shot = Instantiate(Bullet, transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().AddForce(25 * bulletDir, ForceMode2D.Impulse);
        }
        Debug.Log("SHOTGUN SHOT");
    }
}
