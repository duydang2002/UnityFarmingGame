using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    // Overriden o GatherResourceNode
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }
    // Overriden o Plow Tile TilePickUpAction va seed tile
    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;        
    }
    // Overriden o SeedTile
    public virtual void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        
    }
}
