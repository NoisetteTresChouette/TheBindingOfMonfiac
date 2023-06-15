using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletTimeStates
{
    Use,
    Cooldown,
    Reload
}

public class PlayerBulletTime : MonoBehaviour
{
    [SerializeField]
    [Tooltip("the total duration of bullet time")]
    private float _maxDuration;

    [SerializeField]
    [Tooltip("the time to wait before the fuel charge back up")]
    private float _cooldown;

    [SerializeField]
    [Tooltip("the speed at which the fuel charge up")]
    private float _reloadTime;

    [SerializeField]
    [Tooltip("how much the time is reduced")]
    private float _timeScale;

    private float _timer;
    private float _fuelQuantity = 1f;/* between 0 and 1, the proportion of total fuel available*/

    private BulletTimeStates _currentState = BulletTimeStates.Reload;

    private float _initTimeScale;


    private void Awake()
    {
        _initTimeScale = Time.timeScale;
        _timer = _reloadTime;
    }

    private void Update()
    {
        
        switch (_currentState)
        {
            case BulletTimeStates.Use:
                _timer = Mathf.Max(0f, _timer - Time.unscaledDeltaTime);
                if (Input.GetButtonUp("BulletTime") || _timer==0)
                {
                    TransitionTo(BulletTimeStates.Cooldown);
                }
                break;

            case BulletTimeStates.Cooldown:
                _timer -= Time.unscaledDeltaTime;
                if (_timer <= 0)
                {
                    TransitionTo(BulletTimeStates.Reload);
                }
                break;

            case BulletTimeStates.Reload:
                _timer = Mathf.Min(_reloadTime, _timer + Time.unscaledDeltaTime);
                if (Input.GetButtonDown("BulletTime"))
                {
                    TransitionTo(BulletTimeStates.Use);
                }
                break;
        }
    }

    private void TransitionTo(BulletTimeStates nextState)
    {

        OnStateExit(_currentState);

        OnStateEnter(nextState);

        _currentState = nextState;
    }


    private void OnStateExit(BulletTimeStates exitingState)
    {
        switch (exitingState)
        {
            case BulletTimeStates.Use:
                _fuelQuantity = _timer / _maxDuration;
                Time.timeScale = _initTimeScale;
                break;
            case BulletTimeStates.Reload:
                _fuelQuantity = _timer / _reloadTime;
                break;
        }
    }

    private void OnStateEnter(BulletTimeStates enteringState)
    {
        switch (enteringState)
        {
            case BulletTimeStates.Cooldown:
                _timer = _cooldown;
                break;

            case BulletTimeStates.Reload:
                _timer = _fuelQuantity * _reloadTime;
                break;

            case BulletTimeStates.Use:
                _timer = _fuelQuantity * _maxDuration;
                Time.timeScale = _timeScale;
                break;
        }
    }

}