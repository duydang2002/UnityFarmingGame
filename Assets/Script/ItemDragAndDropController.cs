using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDropController : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject itemIcon;
    RectTransform iconTransform;
    Image itemIconImage;
    MainCharacterControl mainCharacter;
    private void Awake()
    {
         mainCharacter = FindObjectOfType<MainCharacterControl>();
    }


    // Start is called before the first frame update
    void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    private void Update()
    {
        if(itemIcon.activeInHierarchy == true)
        {
            iconTransform.position = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                    
                if (EventSystem.current.IsPointerOverGameObject() == false )
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    Vector2 pos = new Vector2(worldPosition.x, worldPosition.y);
                    
                    mainCharacter.setPosition(pos);
                    mainCharacter.setAuto(true);

                    ItemSpawnManager.instance.SpawnItem(
                        worldPosition,
                        itemSlot.item,
                        itemSlot.count);

                    //MainCharacterControl.
                    itemSlot.Clear();
                    itemIcon.SetActive(false);
                }
            }
        }
    }
    internal void OnClick(ItemSlot itemSlot)
    {
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();

        }
        else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count; 

            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
        }
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (itemSlot.item == null)
        {
            itemIcon.SetActive(false);
        }
        else
        {
            itemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.icon;
            itemIconImage = itemIcon.GetComponent<Image>();


        }
    }
}
