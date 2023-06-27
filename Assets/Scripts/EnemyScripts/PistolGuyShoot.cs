using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolGuyShoot : Shooter
{

    private bool _isShootingFrame = false;

    private DetectionZone _detectionZone;

    private void Awake()
    {
        _detectionZone = GetComponent<DetectionZone>();
    }

    private void Update()
    {
        if (_detectionZone.IsAlert())
        {
            if (Vector2.Distance(_detectionZone.GetTarget().position, transform.position) <= _range)
            {
                _isShootingFrame = Shoot(tag);
            }
        }
        else _isShootingFrame = false;
    }

    public bool IsShootingFrame()
    {
        return _isShootingFrame;
    }

}
