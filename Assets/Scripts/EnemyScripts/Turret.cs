using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{

    private DetectionZone _detectionZone;

    private Animator _animator;

    private void Awake()
    {
        _detectionZone = GetComponent<DetectionZone>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        
        if (_detectionZone.IsAlert())
        {
            transform.up = _detectionZone.GetTarget().position - transform.position;
        }

        _animator.SetBool("isAlert", _detectionZone.IsAlert());

    }

}
