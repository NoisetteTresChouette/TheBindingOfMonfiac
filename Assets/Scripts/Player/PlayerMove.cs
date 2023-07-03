using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{

    
    private Vector3 _direction;
    private Quaternion _rotation;

    [SerializeField]
    [Tooltip("Vitesse de déplacement du personnage pendant la course")]
    private float _runnningSpeed = 12f;
    [SerializeField]
    [Tooltip("Vitesse de déplacement du personnage pendant le tir")]
    private float _shootingSpeed = 8f;
    private float _speed;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    #region UnityLifeCycle
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _speed = _runnningSpeed;
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
    #endregion

    public void SwitchSpeed()
    {
        if (_speed == _shootingSpeed)
        {
            _speed = _runnningSpeed;
        }
        else
        {
            _speed = _shootingSpeed;
        }
    }

    public void MoveAction(InputAction.CallbackContext context)
    {
        _direction = context.action.ReadValue<Vector2>();
    }

    public void GamepadRotateAction(InputAction.CallbackContext context)
    {
        Vector3 orientation = (Vector3)context.action.ReadValue<Vector2>();
        if (orientation != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(Vector3.forward, orientation);
        }
    }

    public void MouseRotateAction(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = (Vector3) Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        _rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position);
    }
}
