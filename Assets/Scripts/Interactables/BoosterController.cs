using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterController : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    private Vector3 _direction;
    private Vector3 _thisTransformPosition;

    private Rigidbody _rbPlayer = null;

    private void Start()
    {
        _thisTransformPosition = transform.position;
        
        if (_targetTransform != null)
        {
            _direction = (_targetTransform.position - _thisTransformPosition).normalized;
        }
    }

    private void Update()
    {
        Debug.DrawLine(_thisTransformPosition, _thisTransformPosition + _direction * 10, Color.red, Mathf.Infinity);;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent<Rigidbody>(out _rbPlayer);
            if (_rbPlayer != null)
            {
                _rbPlayer.velocity += _direction;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent<Rigidbody>(out _rbPlayer);
            if (_rbPlayer == null)
            {
                Debug.Log("Player not detected on trigger boost");
            }
        }
    }
}
