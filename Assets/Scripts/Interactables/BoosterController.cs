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
                /* Old script, instead of getting the direction and adding it as velocity.
                if (transform.rotation.eulerAngles.y is < 45 and >= 0 or < 360 and >= 315)
                {
                    _rbPlayer.velocity += Vector3.back;
                    // Debug.Log("1");
                }
                else if (transform.rotation.eulerAngles.y is < 315 and >= 225)
                {
                    //_rbPlayer.velocity += Vector3.left;
                    _rbPlayer.velocity += Vector3.right;
                    // Debug.Log("2");
                }
                else if (transform.rotation.eulerAngles.y is < 225 and >= 135)
                {
                    //_rbPlayer.velocity += Vector3.right;
                    _rbPlayer.velocity += Vector3.forward;
                    // Debug.Log("3");
                }
                else if (transform.rotation.eulerAngles.y is < 135 and >= 45)
                {
                    //_rbPlayer.velocity += Vector3.back;
                    _rbPlayer.velocity += Vector3.left;
                    // Debug.Log("4");
                }*/

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
