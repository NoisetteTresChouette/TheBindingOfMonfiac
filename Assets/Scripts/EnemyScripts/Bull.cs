using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bull : Enemy
{
    [SerializeField]
    [Tooltip("speed of the bull")]
    private float _speed;

    private DetectionZone _detectionZone;

    private Rigidbody2D _rigidbody;

    private Animator _animator;

    private void Awake()
    {
        _detectionZone = GetComponent<DetectionZone>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_detectionZone.IsAlert())
        {
            transform.up = _detectionZone.GetTarget().position - transform.position;

            _rigidbody.velocity = _speed * transform.up;
        }
        else _rigidbody.velocity = Vector2.zero;

        _animator.SetBool("isMoving", _rigidbody.velocity != Vector2.zero);
    }

}
