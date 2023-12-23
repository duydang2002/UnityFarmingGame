using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shopUI;
    public ShopColliderTrigger shopTrigger;
    void Awake()
    {
        shopUI.SetActive(false);
    }
    void Update()
    {
        if (shopTrigger.isShopNear && Input.GetKeyUp(KeyCode.B))
        {
            Debug.Log("B key was pressed");
            shopUI.SetActive(!shopUI.activeInHierarchy);
        }
    }

}
