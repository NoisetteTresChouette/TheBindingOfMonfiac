using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{

    [SerializeField]
    [Tooltip("the amount of health points this entity has")]
    private int _health;

    private void FixedUpdate()
    {
        if (_health < 1)
        {
            Die();
        }
    }

    protected void GetHit(int damageAmount)
    {
        _health -= damageAmount;
        Debug.Log(name + " took " + damageAmount + " damages");
    }

    protected void Die()
    {
        Destroy(gameObject);
        Debug.Log(name + " died");
    }

}
