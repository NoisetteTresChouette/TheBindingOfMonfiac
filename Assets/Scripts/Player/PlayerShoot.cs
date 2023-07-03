using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerShoot : Shooter
{
    private bool _isShooting;

    private bool _startShooting;

    private bool _stopShooting;

    public UnityEvent OnStartShooting;
    public UnityEvent OnStopShooting;

    private void Update()
    {
        if (_isShooting)
        {
            Shoot(tag);
        }
    }

    private void LateUpdate()
    {
        if (_startShooting) _startShooting = false;
        if (_stopShooting) _stopShooting = false;
    }

    public bool IsShooting()
    {
        return _isShooting;
    }

    public bool IsStartingShooting()
    {
        return _startShooting;
    }

    public bool IsStopingShooting()
    {
        return _stopShooting;
    }

    public void ShootAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isShooting = true;
            _startShooting = true;
            OnStartShooting.Invoke();
        }
        if (context.action.WasReleasedThisFrame())
        {
            _isShooting = false;
            _stopShooting = true;
            OnStopShooting.Invoke();
        }
    }


}
