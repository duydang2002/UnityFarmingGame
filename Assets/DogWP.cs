using System.Collections;
using UnityEngine;

public class DogWP : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float moveSpeed = 2f;

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
        Vector2 direction = (Vector2)waypoints[waypointIndex].position - (Vector2)transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

        // Check if reached the waypoint
        float distance = Vector2.Distance(transform.position, waypoints[waypointIndex].position);
        if (distance < 0.01f)
        {
            waypointIndex++;
            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }

        // Update animator parameters based on direction
        animatorMove.SetFloat("Horizontal", direction.x);
        animatorMove.SetFloat("Vertical", direction.y);
    }
}
