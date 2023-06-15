using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : LifeSystem
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            GetHit(1);
        }

    }

}
