using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {
    
    protected override void Shoot()
    {
        Debug.Log("SHOTGUN SHOT");
    }
}
