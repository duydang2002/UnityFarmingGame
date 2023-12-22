using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopColliderTrigger : MonoBehaviour
{
    public bool isShopNear = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered shop");
            isShopNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left shop");
            isShopNear = false;
        }
    }
}
