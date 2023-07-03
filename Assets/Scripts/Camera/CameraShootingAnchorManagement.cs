using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShootingAnchor : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The maximum dezoom while the player is shooting")]
    private float _maxDezoom;
    private Vector3 _maxDezoomVector;

    private Vector3 _initOffset;

    [SerializeField]
    [Tooltip("the time the camera takes to dezoom")]
    public float m_dezoomTime;

    private Vector3 _dezoomVelocity = Vector3.zero;
    private float _dezoomRemaingTime = 0f;

    private void Awake()
    {
        _maxDezoomVector = new Vector3(transform.localPosition.x, _maxDezoom, transform.localPosition.z);
        _initOffset = transform.localPosition;
    }

    private void Update()
    {
        if (GetComponentInParent<PlayerShoot>().IsStartingShooting() || GetComponentInParent<PlayerShoot>().IsStopingShooting())
        {
            _dezoomRemaingTime = m_dezoomTime - _dezoomRemaingTime;
        }
        if (GetComponentInParent<PlayerShoot>().IsShooting())
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _maxDezoomVector, ref _dezoomVelocity, _dezoomRemaingTime);
        }
        else
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _initOffset, ref _dezoomVelocity, _dezoomRemaingTime);
        }

        _dezoomRemaingTime = Mathf.Max(0f,_dezoomRemaingTime - Time.deltaTime);
    }
}
