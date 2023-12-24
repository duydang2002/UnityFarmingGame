using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
    public ToolAction onAction;

    public int moneyValue;

    
    public static Sprite getSprite(Item item)
    {
        return item.icon;
    }

    public static string getName(Item item)
    {
        return item.Name;
    }

    public static float getMoneyValue(Item item)
    {
        return item.moneyValue;
    }
    
    public ToolAction onTileMapAction;
    public ToolAction OnItemUsed;
}

