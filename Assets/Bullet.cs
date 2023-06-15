using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed of the bullet")]
    private float _speed = 10;

    private Rigidbody2D _rigidbody;

    #region Unity Life Cycle
    private void Awake()
    { 
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
    }

    void Start()
    {
        _rigidbody.velocity = transform.up * _speed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && collision.gameObject.tag != tag)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void Init(string teamTag)
    {
        tag = teamTag;
    }

}
