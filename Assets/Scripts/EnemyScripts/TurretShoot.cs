using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetectionZone))]
public class TurretShoot : Shooter
{

    private bool _isShootingFrame = false;

    private void Update()
    {
        if (GetComponent<DetectionZone>().IsAlert())
        {
            _isShootingFrame = Shoot(tag);
        }
        else _isShootingFrame = false;
    }

    public bool IsShootingFrame()
    {
        return _isShootingFrame;
    }

}
