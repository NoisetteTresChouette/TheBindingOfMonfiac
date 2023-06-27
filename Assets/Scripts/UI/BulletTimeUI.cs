using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTimeUI : MonoBehaviour
{

    public BulletTime m_bulletTime;

    private Slider _slider;
    [SerializeField]
    private Image _cooldownDisc;

    [SerializeField]
    [Tooltip("the player it attaches to")]
    private Transform _playerTransform;

    [SerializeField]
    [Tooltip("offset from the player")]
    private Vector3 _offset = new Vector3(0f, -2.5f, 0f);

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {

        switch (m_bulletTime.CurrentState)
        {
            case BulletTimeStates.Use:
                if (Time.timeScale != 0)
                {
                    _slider.value -= Time.unscaledDeltaTime / m_bulletTime.MaxDuration;
                }
                break;

            case BulletTimeStates.Cooldown:
                _cooldownDisc.fillAmount += Time.deltaTime / m_bulletTime.Cooldown;
                break;

            case BulletTimeStates.Reload:
                _cooldownDisc.fillAmount = 0f;
                float delta = Time.deltaTime / m_bulletTime.ReloadTime;
                _slider.value = Mathf.Min(_slider.value + delta, 1);
                break;
        }
    }

    private void LateUpdate()
    {
        transform.position = _playerTransform.position + _offset;
    }

}
