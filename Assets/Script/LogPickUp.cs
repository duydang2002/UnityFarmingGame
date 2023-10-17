using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPickUp : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f;
    [SerializeField] float waitingSecond = 1f;

    private void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Delete if not pick up in time
        if(waitingSecond > 0f)
        {
            waitingSecond--;
            return;
        }
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
        }
        // Use Vector3 for better Compatibility and flexibility
        float distance = Vector3.Distance(transform.position, player.position);
        
        // Guard clause
        if(distance > pickUpDistance)
        {
            return;
        }
        
        // Use to smoothy move from lag to player
        // Mul deltaTime to make it frame rate independent
        transform.position = Vector3.MoveTowards(
            transform.position, 
            player.position, 
            speed * Time.deltaTime);

        if (distance < 0.1f){
            Destroy(gameObject);
        }
    }
}
