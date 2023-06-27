using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{

    public Transform m_runingAnchor;

    public Transform m_shootingAnchor;

    [SerializeField]
    [Tooltip("the time at which the camera switch from one mode to an other")]
    private float _switchTime;

    [SerializeField]
    private float _smoothTimeRun;

    [SerializeField]
    private float _smoothTimeShoot;

    private float _currentTime;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (GetComponentInParent<PlayerShoot>().IsStartingShooting() || GetComponentInParent<PlayerShoot>().IsStopingShooting())
        {
            _currentTime = _switchTime;
        }

        if (GetComponentInParent<PlayerShoot>().IsShooting())
        {
            if (_currentTime > 0f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, m_shootingAnchor.position, ref _velocity, _currentTime);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, m_shootingAnchor.position, ref _velocity, _smoothTimeShoot);
            }
        }
        else
        {
            if (_currentTime > 0f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, m_runingAnchor.position, ref _velocity, _currentTime);
            }
            else transform.position = Vector3.SmoothDamp(transform.position, m_runingAnchor.position, ref _velocity, _smoothTimeRun);
        }
        _currentTime = Mathf.Max(0f, _currentTime - Time.deltaTime);
    }

}
