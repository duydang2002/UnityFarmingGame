using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;
}

[CreateAssetMenu(menuName ="Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slot;

    public void Add(Item item, int count = 1)
    {
        if (item.stackable == true)
        {
            // Tim den vi tri co item
            ItemSlot itemSlot = slot.Find(x=> x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count++;
            }
            else
            {   
                // tim den vi tri khong co item
                itemSlot = slot.Find(x=>x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            // adding non- stackable items
            ItemSlot itemSlot = slot.Find(x => x.item == null);
            if (itemSlot != null )
            {
                itemSlot.item = item;
            }
            
        }
    }
    
}
