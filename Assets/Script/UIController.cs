using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject panel;
    public ShopColliderTrigger shopTrigger; // Assign in Inspector

    void Start()
    {
        shopUI.SetActive(false);
        panel.SetActive(false);
    }

    void Update()
    {
        if (shopTrigger.isShopNear && Input.GetKeyUp(KeyCode.B))
        {
            shopUI.SetActive(!shopUI.activeInHierarchy);
            panel.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            panel.SetActive(!panel.activeInHierarchy);
            shopUI.SetActive(false);
        }
    }
}
