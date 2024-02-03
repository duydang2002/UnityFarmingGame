using System.Collections;
using UnityEngine;

public class DogWP : MonoBehaviour
{
    // Array of waypoints for the dog to follow
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
        // Set the initial position of the dog
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Calculate the direction vector
        Vector2 direction = (Vector2)waypoints[waypointIndex].position - (Vector2)transform.position;

        // Move the dog towards the waypoint
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

        // Check if the dog has reached the current waypoint
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
