using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shopUI;
    void Awake()
    {
        shopUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            Debug.Log("B key was pressed");
            shopUI.SetActive(!shopUI.activeInHierarchy);
        }
    }

}
