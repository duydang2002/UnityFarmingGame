using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class CharacterToolsController : MonoBehaviour
    {
        MainCharacterControl characterController; //vid: CharacterController2D character
        Rigidbody2D rgbd2d;
        Character character;
        ToolbarController toolbarController;
        [SerializeField] float offsetDistance = 1f;
        [SerializeField] float sizeOfInteractableArea = 0.4f;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] MarkerManager markerManager;
        [SerializeField] TileMapReadController tileMapReadController;
        [SerializeField] float maxDistance = 2f;
        [SerializeField] CropsManager cropsManager;
        [SerializeField] TileData plowableTiles;

        Vector3Int selectedTilePosition;
        bool selectable;

        private void Awake()
        {
            characterController = GetComponent<MainCharacterControl>();
            rgbd2d = GetComponent<Rigidbody2D>();
            toolbarController = GetComponent<ToolbarController>();
        }
        
        void Update()
        {
            SelectTile();
            CanSelectCheck();
            Marker();
            if (Input.GetKeyDown("f"))
            {
                if (UseToolWorld() == true)
                {
                    return;
                }
                UseToolGrid();
            }
        }

        private void SelectTile()
        {
            selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        }

        void CanSelectCheck()
        {
            Vector2 characterPosition = transform.position;
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
            markerManager.Show(selectable);
        
        }

        private void Marker()
        {
            //Vector3Int gridPosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
            markerManager.markedCellPosition = selectedTilePosition;

        }

        private bool UseToolWorld()
        {
            Vector2 position = rgbd2d.position + characterController.lastMotionVector*offsetDistance;
            
            Item item = toolbarController.GetItem;
            if (item == null) { return false; }
            if (item.onAction == null) { return false;}

            bool complete = item.onAction.OnApply(position);
            
            return complete;
        }
        
        private void UseToolGrid()
        {
        if (selectable == true)
            {
                TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
                TileData tileData = tileMapReadController.GetTileData(tileBase);

                if (tileData != plowableTiles )
                {
                    return;
                }
                if (cropsManager.Check(selectedTilePosition))
                {
                    cropsManager.Seed(selectedTilePosition);
                }
                cropsManager.Plow(selectedTilePosition);
            }
        }
    }
