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

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }
    public void Clear()
    {
        item = null;
        count = 0;
    }
    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
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

    public void Remove(Item item, int count = 1)
    {
        ItemSlot itemSlot = slot.Find(x => x.item == item);
        if (itemSlot != null)
        {
            itemSlot.count -= count;

            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            Debug.LogError("Item not in inventory");
        }
    }

    public bool Contains(Item item)
    {
        return slot.Exists(x => x.item == item);
    }

}
