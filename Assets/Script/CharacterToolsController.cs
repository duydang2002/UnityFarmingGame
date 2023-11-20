using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


    public class CharacterToolsController : MonoBehaviour
    {
        MainCharacterControl character;
        Rigidbody2D rgbd2d;
        [SerializeField] float offsetDistance = 1f;
        [SerializeField] float sizeOfInteractableArea = 0.4f;
        [SerializeField] private LayerMask playerMask;
        private void Awake()
        {
            character = GetComponent<MainCharacterControl>();
            rgbd2d = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                UseTool();
            }
        }
        private void UseTool()
        {
        Vector2 position = rgbd2d.position + character.lastMotionVector*offsetDistance;
            
            Collider2D[] collider2s = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
            
            foreach (Collider2D c in collider2s)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }
        }
        
    }
