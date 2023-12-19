using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{

    private Transform Container;
    private Transform ShopItemTemplate;

    // Start is called before the first frame update
    private void Awake()
    {
        Container = transform.Find("Container");
        ShopItemTemplate = Container.Find("shopItemTemplate");
        ShopItemTemplate.gameObject.SetActive(false);
    }

    public void CreateItemButton()
    {

    }
}
