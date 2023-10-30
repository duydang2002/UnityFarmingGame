using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] List<InventoryButton> buttons;
    private void Start()
    {
        SetIndex();
        Show();
    }
    private void OnEnable()
    {
        Show();
    }

    private void SetIndex()
    {
        for (int i=0; i< inventory.slot.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    private void Show()
    {
        for (int i=0; i< inventory.slot.Count; i++)
        {
            if (inventory.slot[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slot[i]);
            }
        }
    }
}
