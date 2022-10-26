using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Range(0.5f,5.0f), SerializeField] private float _speed = 1.0f;
    [Range(0.5f,5.0f),SerializeField] public float _waitTime = 3.0f;
    
    [SerializeField] private GameObject []_waypoints;
    
    private int _currWaypointIndex = 0;

    private bool _canMove = true;

    // Update is called once per frame

    void FixedUpdate()
    {
        if (_canMove)
        {
            if (Vector3.Distance(transform.position, _waypoints[_currWaypointIndex].transform.position) < 0.1f)
            {
                _currWaypointIndex++;
                
                if (_currWaypointIndex >= _waypoints.Length)
                {
                    _currWaypointIndex = 0;
                }
                
                StartCoroutine(WaitIdle(_waitTime));
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                _waypoints[_currWaypointIndex].transform.position,
                _speed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.transform.SetParent(this.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.transform.SetParent(null);
            }
        }
    }
    
    IEnumerator WaitIdle(float time)
    {
        _canMove = false;
        
        //Debug.Log("Waiting: " + time);
        yield return new WaitForSeconds(time);
        
        _canMove = true;
    }
}
