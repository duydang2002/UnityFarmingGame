using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class MainCharacterControl : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float speed = 2f;
    

    Vector2 motionVector;
    public Vector2 lastMotionVector;

    Animator animatorMove;

    bool moving;

    // Awake is called when a script is initialized, before start
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animatorMove = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        motionVector = new Vector2(
           Input.GetAxis("Horizontal"),
           Input.GetAxis("Vertical")
           );
        animatorMove.SetFloat("Horizontal", horizontal);
        animatorMove.SetFloat("Vertical", vertical);

        moving = horizontal != 0 || vertical != 0;
        animatorMove.SetBool("moving", moving);

        if (moving)
        {
            lastMotionVector= new Vector2(horizontal, vertical).normalized;
            animatorMove.SetFloat("lastHorizontal", horizontal);
            animatorMove.SetFloat("lastVertical", vertical);
        }

    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
       rigidbody2D.velocity = motionVector*speed;
    }
    
}
