using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] private int toolbarSize = 8;
    public ItemContainer inventory;
    private int selectedTool;

    public Action<int> onChange;

    public Item GetItem
    {
        get
        {
            return GameManager.instance.inventoryContainer.slot[selectedTool].item;
        }
    }
    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            selectedTool += 1;
            selectedTool = selectedTool >= toolbarSize ? 0 : selectedTool;
        }
        else
        {
            selectedTool -= 1;
            selectedTool = selectedTool <= 0 ? toolbarSize - 1 : selectedTool;
        }
        // onChange?.Invoke(selectedTool);
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }
}
