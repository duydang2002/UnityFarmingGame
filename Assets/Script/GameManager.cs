using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        characterController = FindObjectOfType<MainCharacterControl>();
    }

    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
    public MainCharacterControl characterController;
    public DialogueSystem dialogueSystem;
   /* public void MoveMainCharacterTo(Vector2 targetPosition)
    {
        
        if (characterController != null)
        {
            characterController.MoveTo(targetPosition);
        }
    }*/
}
