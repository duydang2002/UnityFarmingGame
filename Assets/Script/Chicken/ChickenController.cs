using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    
    [SerializeField] float speed = 3f;


    Animator animatorMove;
    private Direction baseDirection;
 
    private float currentTimeBetweenTicks = 0;
    private float maxTimeBetweenTick = 5; // 10s
    
    Vector2 motionVector;

    private void Awake()
    {
        animatorMove = GetComponent<Animator>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        baseDirection = getRandomDirection();
    }

    void Update()
    {
        Move();
        currentTimeBetweenTicks += Time.deltaTime;
        if (currentTimeBetweenTicks >= maxTimeBetweenTick)
        {
            currentTimeBetweenTicks = 0;
            baseDirection = getRandomDirection();
        
        }
        
    }
    private void Move()
    {

        if (baseDirection == Direction.Right)
        {
            transform.Translate(0.1f*Time.deltaTime*speed, 0, 0);
            animatorMove.SetFloat("Horizontal", 1);
            animatorMove.SetFloat("Vertical", 0);
        }
        else if (baseDirection == Direction.Left)
        {
            transform.Translate(-0.1f*Time.deltaTime*speed, 0, 0);
            animatorMove.SetFloat("Horizontal", -1);
            animatorMove.SetFloat("Vertical", 0);
        }
        else if (baseDirection == Direction.Up)
        {
            transform.Translate(0, 0.1f* Time.deltaTime*speed, 0);
            animatorMove.SetFloat("Vertical", 1);
            animatorMove.SetFloat("Horizontal", 0);
        }
        else
        {
            transform.Translate(0, -0.1f* Time.deltaTime, 0);
            animatorMove.SetFloat("Vertical", -1);
            animatorMove.SetFloat("Horizontal",0);
        }

    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if(baseDirection == Direction.Up)
        {
            baseDirection = Direction.Down;
        }else if (baseDirection == Direction.Down)
        {
            baseDirection = Direction.Up;
        }else if (baseDirection == Direction.Right)
        {
            baseDirection = Direction.Left;
        }else
        {
            baseDirection = Direction.Right;
        }
    }
    private Direction getRandomDirection()
    {
        int randomDirection = UnityEngine.Random.Range(0, 4);
        return (Direction) randomDirection;
    }
    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
}
