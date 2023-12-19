using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{

    // Start is called before the first frame update
    public string Name;
    public bool stackable;
    public Sprite icon;

    public float moneyValue;

    
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
    
}

