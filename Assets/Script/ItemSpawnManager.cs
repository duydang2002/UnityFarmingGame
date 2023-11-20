using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;


    private void Awake()
    {
        instance = this;
    }
    [SerializeField] GameObject pickUpItemPrefab;
    public void SpawnItem(Vector3 position, Item item, int count)
    {
       GameObject gameObject =  Instantiate(pickUpItemPrefab,position, Quaternion.identity);
       gameObject.GetComponent<PickUpItem>().Set(item, count);
    }
}
