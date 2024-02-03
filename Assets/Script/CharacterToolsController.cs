using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class CharacterToolsController : MonoBehaviour
    {
        MainCharacterControl characterController; 
        Rigidbody2D rgbd2d;
        Character character;
        ToolbarController toolbarController;
        [SerializeField] float offsetDistance = 1f;
        [SerializeField] float sizeOfInteractableArea = 0.4f;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] MarkerManager markerManager;
        [SerializeField] TileMapReadController tileMapReadController;
        [SerializeField] float maxDistance = 2f;
        [SerializeField] ToolAction onTilePickUp;

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
        // Lay vi tri con tro chuot
        private void SelectTile()
        {
            selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        }
        
        // Kiem tra xem co nam trong pham vi tuong tac duoc khong
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
        // Dung len 1 Object khac
        private bool UseToolWorld()
        {
            Vector2 position = rgbd2d.position + characterController.lastMotionVector*offsetDistance;
            
            Item item = toolbarController.GetItem;
            if (item == null) { return false; }
            // neu Item do khong co action len item khac
            if (item.onAction == null) { return false;}
            // OnAction chinh la 1 Object khac co kieu co the duoc su dung len boi Tool nay
            bool complete = item.onAction.OnApply(position);
            if (complete == true)
            {
                // Neu ma Item do la 1 Item tieu thu thi - di 1
                if (item.OnItemUsed != null)
                {
                    item.OnItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
                }
            }             
            return complete;
        }
        // Dung len map vi du nhu dao dat
        private void UseToolGrid()
        {
        if (selectable == true)
            {
                Item item = toolbarController.GetItem;
                    if (item == null) {
                // khi khong co item thi co the no la thu hoach
                    PickUpTile();
                        return ; 
                }
                if (item.onTileMapAction == null) { return ;}

                bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController,item);

                if (complete == true)
                {
                    if (item.OnItemUsed != null)
                    {
                        item.OnItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
                    }
                } 
            }
        }

    private void PickUpTile()
    {
        if (onTilePickUp == null) { return; }
        // TileMapReadController la 1 script

        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
    }
}
