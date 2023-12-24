using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    //[SerializeField] Item item;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;
    [SerializeField] GameObject highLightMarker;
    [SerializeField] ResourceNodeType nodeType;


    public override void Hit(Character character)
    {
        while (dropCount > 0)
        {
            dropCount--;
            Vector3 position = transform.position;
            position.x += spread *UnityEngine.Random.value-spread/2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(pickUpDrop);
            go.transform.position = position;
            //ItemSpawnManager.instance.SpawnItem(position,item,itemCountInOneDrop);

        }
        Destroy(gameObject);
        
    }
     
    public override bool CanBeHit(List<ResourceNodeType> CanBeHit)
    {
        return CanBeHit.Contains(nodeType);
    }
}
