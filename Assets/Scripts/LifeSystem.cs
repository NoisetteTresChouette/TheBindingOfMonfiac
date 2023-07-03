using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class LifeSystem : MonoBehaviour
{

    [SerializeField]
    [Tooltip("the amount of health points this entity has")]
    protected int _health;
    public int Health { get => _health; }

    public AudioClip _hitSound;

    private void Update()
    {
        if (_health < 1)
        {
            Die();
        }
    }

    protected void GetHit(int damageAmount)
    {
        GameManager.m_instance.PlaySound(_hitSound);
        _health -= damageAmount;
        Debug.Log(name + " took " + damageAmount + " damages");
    }

    protected void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<Enemy>().Die();
            Destroy(gameObject);
        }
        else
        {
            LevelManager.m_instance.m_isPlayerAlive = false;
            gameObject.SetActive(false);
        }
    }

}
