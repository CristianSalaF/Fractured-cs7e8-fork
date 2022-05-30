using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeathController : MonoBehaviour
{
    [SerializeField] private GameObject _respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController cc = other.GetComponent<PlayerController>();
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (cc != null)
            {
                cc.enabled = false;
            }
            
            if (rb != null)
            {
                //Stop Moving/Translating
                rb.velocity = Vector3.zero;

                //Stop rotating
                rb.angularVelocity = Vector3.zero;
            }

            other.transform.position = _respawnPoint.transform.position;

            StartCoroutine(CCEnableRoutine(cc));
        }
    }

    IEnumerator CCEnableRoutine(PlayerController controller)
    {
        yield return new WaitForSeconds(0.5f);

        controller.enabled = true;
    }
}
