using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    [Tooltip("direction de déplacement du personnage")]
    private Vector3 _direction;
    [Tooltip("Vitesse de déplacement du personnage")]
    public float _speed = 8f;
    [SerializeField]
    [Tooltip("_rotation du personnage")]
    private Quaternion _rotation;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
        if (_direction.magnitude > 1) _direction = _direction.normalized;

        float HRightJoystick = Input.GetAxis("HorizontalRightJoystick");
        float VRightJoystick = Input.GetAxis("VerticalRightJoystick");

        if (HRightJoystick != 0f || VRightJoystick != 0f)
        {
            _rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(HRightJoystick, VRightJoystick, 0));
        }

    }

    void FixedUpdate()
    {

        _rigidbody.velocity = _direction * _speed;

        _rigidbody.SetRotation(_rotation);

    }
}
