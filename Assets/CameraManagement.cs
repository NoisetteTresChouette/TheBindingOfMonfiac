using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{

    public GameObject m_cameraShootingAnchor;

    public GameObject m_player;

    public Transform m_mainCameraAnchor;

    [SerializeField]
    [Tooltip("the max dezoom possible during shooting")]
    private float _shootingMaxDezoom;

    private float _initDezoom;

    private float _shootingDezoomTime;
    private float _currentDezoomTime = 0f;

    private void Awake()
    {
        _initDezoom = Camera.main.orthographicSize;
        _shootingDezoomTime = m_cameraShootingAnchor.GetComponent<CameraShootingAnchor>().m_dezoomTime;
    }

    private void LateUpdate()
    {
        Camera.main.orthographicSize = Mathf.Lerp(_initDezoom, _shootingMaxDezoom, _currentDezoomTime / _shootingDezoomTime);
        if (m_player.GetComponent<PlayerShoot>().IsShooting())
        {
            _currentDezoomTime = Mathf.Min(_shootingDezoomTime,_currentDezoomTime + Time.deltaTime);
        }
        else
        {
            _currentDezoomTime = Mathf.Max(0f, _currentDezoomTime - Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        transform.position = m_mainCameraAnchor.position;
    }

}
