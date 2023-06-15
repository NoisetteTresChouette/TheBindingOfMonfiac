using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRuningAnchor : MonoBehaviour
{

    [SerializeField]
    [Tooltip("the ratio of the camera offset relatively to the player over the velocity of the latter")]
    private float _offsetOverVelocity;

    private void Update()
    {

        Vector3 playerPosition = transform.parent.position;
        Vector2 velocity = GetComponentInParent<Rigidbody2D>().velocity;

        transform.position = playerPosition + new Vector3(_offsetOverVelocity * velocity.x, _offsetOverVelocity * velocity.y, -10f);
    }

}
