using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLife : LifeSystem
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetHit(1);
        }
    }

}
