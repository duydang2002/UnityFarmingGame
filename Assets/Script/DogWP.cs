using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogWP : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;

    [SerializeField] float moveSpeed = 0.5f;
    private int waypointIndex = 0;
    private Animator animatorMove;

    private void Awake()
    {
        animatorMove = GetComponent<Animator>();
    }

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

        Vector2 direction = (Vector2)waypoints[waypointIndex].position - (Vector2)transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);


        float distance = Vector2.Distance(transform.position, waypoints[waypointIndex].position);
        if (distance < 0.01f)
        {
            waypointIndex++;
            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }

        animatorMove.SetFloat("horizontal", direction.x);
        animatorMove.SetFloat("vertical", direction.y);
    }
}
