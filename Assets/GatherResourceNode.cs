using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(menuName = "Data/Tool action/Gather Resource Node")]
public class GatherResourceNode : ToolAction
{
    Character character;
    [SerializeField] float sizeOfInteractableArea = 1f;
    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] collider2s = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);
            
        foreach (Collider2D c in collider2s)
            {
                ToolHit hit = c.GetComponent<ToolHit>();
                if (hit != null)
                {
                    hit.Hit(character);
                    return true;
                }
            }
        return false;
    }
}
    //Rigidbody2D rgbd2d;        //rgbd2d = GetComponent<Rigidbody2D>();
