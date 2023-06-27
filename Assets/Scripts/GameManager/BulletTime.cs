using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.Audio;

public enum BulletTimeStates
{
    Use,
    Cooldown,
    Reload
}

public class BulletTime : MonoBehaviour
{
    #region GameplayFields
    [Header("Gameplay Fields")]

    [SerializeField]
    [Tooltip("how much the time is reduced")]
    private float _timeScale;

    [SerializeField]
    [Tooltip("the total duration of bullet time")]
    private float _maxDuration;

    public float MaxDuration { get => _maxDuration;}

    [SerializeField]
    [Tooltip("the time to wait before the fuel charge back up")]
    private float _cooldown;

    public float Cooldown { get => _cooldown;}

    [SerializeField]
    [Tooltip("the speed at which the fuel charge up")]
    private float _reloadTime;

    public float ReloadTime { get => _reloadTime; }
    #endregion

    #region AudioFields
    [Header("Audio Fields")]

    [SerializeField]
    [Tooltip("The audio mixer bullet time will alter the pitch of")]
    private AudioMixer _audioMixer;

    [SerializeField]
    [Tooltip("The pitch the audio mixer will be set to during bullet time")]
    private float _pitch;

    [SerializeField]
    [Tooltip("The pitch is selected automaticaly based on the time scaled choosed")]
    private bool _automaticPitch;
    #endregion

    #region Events
    [Header("Events")]
    public UnityEvent OnStartBulletTime = new();
    public UnityEvent OnStopBulletTime = new();
    #endregion

    private float _timer;
    private float _fuelQuantity = 1f;/* between 0 and 1, the proportion of total fuel available*/

    private BulletTimeStates _currentState = BulletTimeStates.Reload;

    public BulletTimeStates CurrentState { get => _currentState; }

    private float _initTimeScale;

    private bool _usingBulletTime = false;


    #region UnityLifeCycle
    private void Awake()
    {
        _initTimeScale = Time.timeScale;
        _timer = _reloadTime;
    }

    private void Start()
    {
        if (_automaticPitch)
        {
            _pitch = _timeScale;
        }
    }

    private void Update()
    {
        
        switch (_currentState)
        {
            case BulletTimeStates.Use:
                _timer = Mathf.Max(0f, _timer - (Time.deltaTime/_timeScale));
                if ( (!_usingBulletTime) || _timer==0)
                {
                    TransitionTo(BulletTimeStates.Cooldown);
                }
                break;

            case BulletTimeStates.Cooldown:
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    TransitionTo(BulletTimeStates.Reload);
                }
                break;

            case BulletTimeStates.Reload:
                _timer = Mathf.Min(_reloadTime, _timer + Time.deltaTime);
                if (_usingBulletTime)
                {
                    TransitionTo(BulletTimeStates.Use);
                }
                break;
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = _initTimeScale;
        _audioMixer.SetFloat("MainPitch", 1f);
    }
    #endregion

    #region FinalStateMachinMethods
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
                _usingBulletTime = false;
                Time.timeScale = _initTimeScale;
                _audioMixer.SetFloat("MainPitch", 1f);
                OnStopBulletTime.Invoke();
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
                _audioMixer.SetFloat("MainPitch", _pitch);
                OnStartBulletTime.Invoke();
                break;
        }
    }
    #endregion

    public void OnBulletTime(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
        {
            if (context.action.WasPressedThisFrame())
            {
                _usingBulletTime = true;
            }
            else if (context.action.WasReleasedThisFrame())
            {
                _usingBulletTime = false;
            }
        }
    }

}