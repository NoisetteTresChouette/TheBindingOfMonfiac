using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DetectionZone : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The radius of detection")]
    private float _detectionRadius;

    [SerializeField]
    [Tooltip("The layers that would be detected by the zone")]
    private List<string> _detectedLayers = new();
    
    private LayerMask _layerMask;

    private Transform _target;

    private bool _isAlert = false;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask(_detectedLayers.ToArray());
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _layerMask);

        if (colliders.Length == 0)
        {
            _isAlert = false;
        }
        else 
        {
            _isAlert = true;
            if (!colliders.Any(collider => collider.transform == _target))
            {
                _target = colliders[0].transform;
            }
        }
    }

    public bool IsAlert()
    {
        return _isAlert;
    }

    public Transform GetTarget()
    {
        return _target;
    }

}
