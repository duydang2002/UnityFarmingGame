using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Ore
}

[CreateAssetMenu(menuName = "Data/Tool Action/Gather Resource Node")]

public class GatherResourceNode : ToolAction
{
    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] private List<ResourceNodeType> canHitNodesOfType;
    
    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] collider2d = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);
            
        foreach (Collider2D c in collider2d)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.CanBeHit(canHitNodesOfType) == true)
                {
                    hit.Hit();
                    return true;
                }
            }
        }
        return false;
    }
}
