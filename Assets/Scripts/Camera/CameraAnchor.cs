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
    [Tooltip("while running, the time this anchor takes to get from its current position, to the runnning anchor")]
    private float _smoothTimeRun;

    [SerializeField]
    [Tooltip("while shooting, the time this anchor takes to get from its current position, to the shooting anchor")]
    private float _smoothTimeShoot;

    private float _currentSwitchingTime;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (GetComponentInParent<PlayerShoot>().IsStartingShooting() || GetComponentInParent<PlayerShoot>().IsStopingShooting())
        {
            _currentSwitchingTime = _switchTime;
        }

        if (GetComponentInParent<PlayerShoot>().IsShooting())
        {
            if (_currentSwitchingTime > 0f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, m_shootingAnchor.position, ref _velocity, _currentSwitchingTime);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, m_shootingAnchor.position, ref _velocity, _smoothTimeShoot);
            }
        }
        else
        {
            if (_currentSwitchingTime > 0f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, m_runingAnchor.position, ref _velocity, _currentSwitchingTime);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, m_runingAnchor.position, ref _velocity, _smoothTimeRun);
            }
        }
        _currentSwitchingTime = Mathf.Max(-1f, _currentSwitchingTime - Time.deltaTime);
    }

}
