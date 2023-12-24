using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ToolHit : MonoBehaviour
{
    public virtual void Hit(Character character)
    {
        
    }
    public virtual bool CanBeHit(List<ResourceNodeType> CanBeHit)
    {
        return true;
    }
}
