using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{

    public Transform m_shootingAnchor;
    public Transform m_runningAnchor;

    public PlayerShoot m_playerShoot;


    #region DezoomProperties
    [SerializeField]
    [Tooltip("the max dezoom possible during shooting")]
    private float _shootingMaxDezoom;

    private float _initDezoom;

    private float _shootingDezoomTime;
    private float _currentDezoomTime = 0f;
    #endregion

    #region PositioProperties

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

    #endregion

    private void Awake()
    {
        _initDezoom = Camera.main.orthographicSize;
        _shootingDezoomTime = m_shootingAnchor.GetComponent<CameraShootingAnchor>().m_dezoomTime;
    }

    private void LateUpdate()
    {

        /*Dezoom Set up*/

        Camera.main.orthographicSize = Mathf.Lerp(_initDezoom, _shootingMaxDezoom, _currentDezoomTime / _shootingDezoomTime);
        if (m_playerShoot.IsShooting())
        {
            _currentDezoomTime = Mathf.Min(_shootingDezoomTime,_currentDezoomTime + Time.deltaTime);
        }
        else
        {
            _currentDezoomTime = Mathf.Max(0f, _currentDezoomTime - Time.deltaTime);
        }



        /*Position Set up*/

        if (m_playerShoot.IsStartingShooting() || m_playerShoot.IsStopingShooting())
        {
            _currentSwitchingTime = _switchTime;
        }

        if (m_playerShoot.IsShooting())
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
                transform.position = Vector3.SmoothDamp(transform.position, m_runningAnchor.position, ref _velocity, _currentSwitchingTime);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, m_runningAnchor.position, ref _velocity, _smoothTimeRun);
            }
        }
        _currentSwitchingTime = Mathf.Max(-1f, _currentSwitchingTime - Time.deltaTime);

    }

}
