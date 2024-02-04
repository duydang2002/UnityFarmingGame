using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody2D))]
public class MainCharacterControl : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    [SerializeField] float speed = 2f;
    [SerializeField] NPCController nPCController;

    Vector2 motionVector;
    Vector2 position;
    bool auto = false;
    public Vector2 lastMotionVector;

    float diagonalSpeedMultiplier = 0.8f;
    float adjustedSpeed;

    Animator animatorMove;

    bool moving;

    // Awake is called when a script is initialized, before start
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animatorMove = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        // Ngat khi dang tuong tac voi NPC
        if (nPCController.getInteracting())
        {
            animatorMove.SetBool("moving", false);
            return;
        }
        if (auto) {
            MoveTo(position);
        }
        else
        {
            // Lay input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            motionVector = new Vector2(
               Input.GetAxis("Horizontal"),
               Input.GetAxis("Vertical")
               );
            // Set cac animation phu hop voi vector
            animatorMove.SetFloat("Horizontal", horizontal);
            animatorMove.SetFloat("Vertical", vertical);

            moving = horizontal != 0 || vertical != 0;
            animatorMove.SetBool("moving", moving);

            // Chinh toc do khi di cheo
            adjustedSpeed = (horizontal != 0 && vertical != 0) ? speed * diagonalSpeedMultiplier : speed;
            
            // Set huong cua nhan vat sau khi di chuyen
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
    // Tao van toc cho Maincharacter
    private void Movement()
    {
        if (nPCController.getInteracting())
        {
            rigidbody2D.velocity = new Vector2(0, 0);
            return;
        }
        rigidbody2D.velocity = motionVector* adjustedSpeed;

        
    }
    public void MoveTo(Vector2 targetPosition)
    {
        // Kiem tra xem da toi gan vi tri de dat do chua
            if (Vector2.Distance(transform.position, targetPosition) < 1.0f)
            {
                rigidbody2D.velocity = Vector2.zero;
                auto = false;
                moving = false;
                animatorMove.SetBool("moving", moving);
            }
            // Dua ve 1 vector voi do lon = 1
            motionVector = (targetPosition - (Vector2)transform.position).normalized;

            // Update the animator parameters if needed
            float horizontal = motionVector.x;
            float vertical = motionVector.y;

            animatorMove.SetFloat("Horizontal", horizontal);
            animatorMove.SetFloat("Vertical", vertical);

            moving = true;
            animatorMove.SetBool("moving", moving);

            lastMotionVector = motionVector;
                

    }
    public void setPosition(Vector2 pos)
    {
        this.position = pos;
        
    }
    public void setAuto(bool auto)
    {
        this.auto = auto;
    }
    public bool getAuto()
    {
        return this.auto;
    }
}
