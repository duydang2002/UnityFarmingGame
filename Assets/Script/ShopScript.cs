using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    private Transform Container;
    private Transform ShopItemTemplate;
    public MoneyManager moneyManager;
    public ItemContainer inventory;
    private Item selectedItem;

    // Start is called before the first frame update
    private void Awake()
    {
        Container = transform.Find("Container");
        ShopItemTemplate = Container.Find("ShopItemTemplate");
        ShopItemTemplate.gameObject.SetActive(false);
    }

    void SelectedItem(Item item)
    {
        this.selectedItem = item;
        Debug.Log("Selected item: " + selectedItem.Name);
        // Find your buy and sell buttons in the scene
        Button buyButton = Container.Find("buyButton").GetComponent<Button>();
        Button sellButton = Container.Find("sellButton").GetComponent<Button>();

    // Remove old listeners
        buyButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();

    // Add new listeners
        buyButton.onClick.AddListener(PurchaseItem);
        sellButton.onClick.AddListener(SellItem);
    }

    public void CreateItemButton(Item item)
    {
        Transform shopItemTransform = Instantiate(ShopItemTemplate, Container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        shopItemRectTransform.anchoredPosition = new Vector2(0, -100 * (Container.childCount - 2));
        shopItemTransform.gameObject.SetActive(true);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(item.Name);
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = item.icon;
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(item.moneyValue.ToString());

        Button highlightButton = shopItemTransform.GetComponent<Button>();
        if (highlightButton != null)
        {
            highlightButton.onClick.AddListener(() => SelectedItem(item));
            Debug.Log($"2light button: {item.Name}");
        }
        else
        {
            Debug.LogError("No Button component found on shop item template.");
        }

       
    }

    void PurchaseItem()
    {
        if (selectedItem != null)
        {
            if (MoneyManager.currentMoney >= selectedItem.moneyValue)
            {
                moneyManager.SubtractMoney(selectedItem.moneyValue);

                GameManager.instance.inventoryContainer.Add(selectedItem);
            }
            else
            {
                Debug.LogError("Not enough money to purchase this item");
            }
        }
        else
        {
            Debug.LogError("No item selected");
        }
    }

    void SellItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.inventoryContainer.Contains(selectedItem))
            {
                GameManager.instance.inventoryContainer.Remove(selectedItem);

                moneyManager.AddMoney(selectedItem.moneyValue);
            }
            else
            {
                Debug.LogError("Item not in inventory");
            }
        }
        else
        {
            Debug.LogError("No item selected");
        }
    }


    void Start()
    {
        Item sword = Resources.Load<Item>("Item/Sword");
        Item stone = Resources.Load<Item>("Item/Stone");
        Item wood = Resources.Load<Item>("Item/Wood");
        
        CreateItemButton(sword);
        CreateItemButton(stone);
        CreateItemButton(wood);
    }

}
