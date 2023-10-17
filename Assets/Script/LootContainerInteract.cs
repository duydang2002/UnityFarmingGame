using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool opened;
    public override void Interact(Character character)
    {
        if(opened == false)
        {
            opened = true;
            closedChest.SetActive(false);
            openedChest.SetActive(true);
        }
    }
}
