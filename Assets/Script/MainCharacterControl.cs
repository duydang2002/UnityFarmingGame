using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class MainCharacterControl : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    [SerializeField] float speed = 2f;
    

    Vector2 motionVector;
    Vector2 position;
    bool auto = false;
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
        if (auto) {
            MoveTo(position);
        }
        else
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
                lastMotionVector = new Vector2(horizontal, vertical).normalized;
                animatorMove.SetFloat("lastHorizontal", horizontal);
                animatorMove.SetFloat("lastVertical", vertical);
            }
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
    public void MoveTo(Vector2 targetPosition)
    {
            if (Vector2.Distance(transform.position, targetPosition) < 1.0f)
            {
                rigidbody2D.velocity = Vector2.zero;
                auto = false;
                moving = false;
                animatorMove.SetBool("moving", moving);
            }
            
            motionVector = (targetPosition - (Vector2)transform.position).normalized;

            // Update the animator parameters if needed
            float horizontal = motionVector.x;
            float vertical = motionVector.y;

            animatorMove.SetFloat("Horizontal", horizontal);
            animatorMove.SetFloat("Vertical", vertical);

            moving = true;
            animatorMove.SetBool("moving", moving);

            lastMotionVector = motionVector;
            position = targetPosition;
        
        

    }
    public void setPosition(Vector2 pos)
    {
        this.position = pos;
        
    }
    public void setAuto(bool auto)
    {
        this.auto = auto;
    }

}
