using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;


    public class CharacterToolsController : MonoBehaviour
    {
        MainCharacterControl characterController;
        Rigidbody2D rgbd2d;
        Character character;
        [SerializeField] float offsetDistance = 1f;
        [SerializeField] float sizeOfInteractableArea = 0.4f;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] MarkerManager markerManager;
        [SerializeField] TileMapReadController tileMapReadController;

        private void Awake()
        {
        characterController = GetComponent<MainCharacterControl>();
            rgbd2d = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Marker();
            if (Input.GetMouseButtonDown(0))
            {
                UseTool();
            }
        }

        private void Marker()
        {
            Vector3Int gridPosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
            markerManager.markedCellPosition = gridPosition;

        }
        private void UseTool()
        {
        Vector2 position = rgbd2d.position + characterController.lastMotionVector*offsetDistance;
            
            Collider2D[] collider2s = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
            
            foreach (Collider2D c in collider2s)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            
            if (hit != null)
            {
                hit.Hit(character);
                break;
            }
        }
        }
        
    }
