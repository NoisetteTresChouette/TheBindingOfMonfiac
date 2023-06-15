using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : Shooter
{

    private bool _isShooting;

    private bool _startShooting;

    private bool _stopShooting;

    void Update()
    {
        if (Input.GetButton("Shoot"))
        {
            Shoot(tag);
            _isShooting = true;
        }

        if (Input.GetButtonUp("Shoot"))
        {
            _isShooting = false;
            _stopShooting = true;
        }
        else
        {
            _stopShooting = false;
        }

        if (Input.GetButtonDown("Shoot"))
        {
            _startShooting = true;
        }
        else _startShooting = false;
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

}
