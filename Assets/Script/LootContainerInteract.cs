using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] NPCController nPCController;
    [SerializeField] bool opened;
    public override void Interact(Character character)
    {
        if(opened == false)
        {
            opened = true;
            Item Axe = Resources.Load<Item>("Item/Axe");
            GameManager.instance.inventoryContainer.Add(Axe);


            GameManager.instance.toolBarPanel.SetActive(false);
            GameManager.instance.toolBarPanel.SetActive(true);
            closedChest.SetActive(false);
            openedChest.SetActive(true);
            NPCController.start = false;
            nPCController.SetQuestOn();
        }
    }
}
