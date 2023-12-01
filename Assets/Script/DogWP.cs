using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogWP : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 0.5f;
    int waypointIndex = 0;
    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.Lerp(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, waypoints[waypointIndex].position);
        if (distance < 0.01f)
        {
            waypointIndex++;
            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }
}
