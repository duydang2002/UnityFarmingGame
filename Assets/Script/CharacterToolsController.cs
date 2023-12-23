using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;
using Vector2 = UnityEngine.Vector2;


public class CharacterToolsController : MonoBehaviour
    {
        MainCharacterControl characterController;
        Rigidbody2D rgbd2d;
        Character character;
        ToolBarController toolbarController;
        [SerializeField] float offsetDistance = 1f;
        [SerializeField] float sizeOfInteractableArea = 0.4f;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] MarkerManager markerManager;
        [SerializeField] TileMapReadController tileMapReadController;
        [SerializeField] float maxDistance = 3f;
        [SerializeField] CropsManager cropsManager;
        [SerializeField] TileData plowableTiles;
        
        private Vector3Int selectedTilePosition;
        private bool selectable;
        
        private void Awake()
        {
            characterController = GetComponent<MainCharacterControl>();
            rgbd2d = GetComponent<Rigidbody2D>();
            toolbarController = GetComponent<ToolBarController>();
        }
        void Update()
        {
            SelectTile();
            CanSelectCheck();
            Marker();
            if (Input.GetMouseButtonDown(0))
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
            markerManager.markedCellPosition = selectedTilePosition;
        }
        
        private bool UseToolWorld()
        {
            Vector2 position = rgbd2d.position + characterController.lastMotionVector*offsetDistance;

            Item item = toolbarController.GetItem;
            if (item == null)
            {
                return false;
            }
            if (item.onAction == null)
            {
                return false;
            }

            bool complete = item.onAction.OnApply(position);
            
            return complete;
        }

        private void UseToolGrid()
        {
            if (selectable == true)
            {
                TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
                TileData tileData = tileMapReadController.GetTileData(tileBase);
                if (tileData != plowableTiles)
                {
                    return;
                }
                if (cropsManager.Check(selectedTilePosition))
                {
                    cropsManager.Seed(selectedTilePosition);
                }
                else
                {
                    cropsManager.Plow(selectedTilePosition);
                }
                cropsManager.Plow(selectedTilePosition);
            }
        }
    }
