using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    
    protected override void Shoot()
    {
        Vector2 dir = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1);
        if (dir.sqrMagnitude < 0.01f)
            dir = transform.right;
        GameObject shot = Instantiate(Bullet, transform.position, Quaternion.identity);
        shot.GetComponent<Bullet>().Damage = this.Damage;
        shot.GetComponent<Rigidbody2D>().AddForce(25 * dir, ForceMode2D.Impulse);
        Debug.Log("Pistol Shot");
    }
}
