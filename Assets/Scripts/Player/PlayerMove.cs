using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    [Tooltip("direction de déplacement du personnage")]
    private Vector3 _direction;
    [SerializeField]
    [Tooltip("Vitesse de déplacement du personnage")]
    private float _speed = 8f;
    [SerializeField]
    [Tooltip("_rotation du personnage")]
    private Quaternion _rotation;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("isWalking", _rigidbody.velocity != Vector2.zero);
    }

    void FixedUpdate()
    {

        _rigidbody.velocity = _direction * _speed;

        _rigidbody.SetRotation(_rotation);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.action.ReadValue<Vector2>();
    }

    public void OnGamepadRotate(InputAction.CallbackContext context)
    {
        Vector3 orientation = (Vector3)context.action.ReadValue<Vector2>();
        if (orientation != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(Vector3.forward, orientation);
        }
    }

    public void OnMouseRotate(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = (Vector3) Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        _rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position);
    }
}
