using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : Shooter
{
    private bool _isShooting;

    private bool _startShooting;

    private bool _stopShooting;

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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            _isShooting = true;
            _startShooting = true;
        }
        if (context.action.WasReleasedThisFrame())
        {
            _isShooting = false;
            _stopShooting = true;
        }
    }


}
