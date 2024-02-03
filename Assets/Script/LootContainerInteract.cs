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

            // Load items from resources and add them to the inventory container
            Item Axe = Resources.Load<Item>("Item/Axe");
            GameManager.instance.inventoryContainer.Add(Axe);
            Item PickAxe = Resources.Load<Item>("Item/PickAxe");
            GameManager.instance.inventoryContainer.Add(PickAxe);
            Item Plow = Resources.Load<Item>("Item/Plow");
            GameManager.instance.inventoryContainer.Add(Plow);
            Item Seed = Resources.Load<Item>("Item/Seeds");
            GameManager.instance.inventoryContainer.Add(Seed,5);

            // Disable and re-enable the toolbar panel to refresh the inventory display
            GameManager.instance.toolBarPanel.SetActive(false);
            GameManager.instance.toolBarPanel.SetActive(true);

            // Disable the closed chest and enable the opened chest
            closedChest.SetActive(false);
            openedChest.SetActive(true);

            // Disable the quest start and set the quest on
            NPCController.start = false;
            nPCController.SetQuestOn();
        }
    }
}
