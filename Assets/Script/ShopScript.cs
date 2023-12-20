using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    private Transform Container;
    private Transform ShopItemTemplate;
    public MoneyManager moneyManager; // Assign in Inspector
    public ItemContainer inventory;

    // Start is called before the first frame update
    private void Awake()
    {
        Container = transform.Find("Container");
        ShopItemTemplate = Container.Find("ShopItemTemplate");
        ShopItemTemplate.gameObject.SetActive(false);
    }

    public void CreateItemButton(Item item)
    {
        // Instantiate a new shop item from the template
        Transform shopItemTransform = Instantiate(ShopItemTemplate, Container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        shopItemRectTransform.anchoredPosition = new Vector2(0, -100 * Container.childCount);
        shopItemTransform.gameObject.SetActive(true);

        // Debugging
        //Debug.Log(shopItemTransform.Find("nameText") != null ? "Found nameText" : "Did not find nameText");
        //Debug.Log(shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>() != null ? "Found TextMeshProUGUI" : "Did not find TextMeshProUGUI");
        //Debug.Log(item != null ? $"Item: {item.Name}" : "Item is null");

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(item.Name);
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = item.icon;
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(item.moneyValue.ToString());

        Button button = shopItemTransform.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => PurchaseItem(item));
        }
        else
        {
            Debug.LogError("No Button component found on shop item template.");
        }


    }

    void PurchaseItem(Item item)
    {
        if (item == null)
        {
            Debug.Log($"inventory: {inventory}");
            Debug.Log($"item: {item}");
            Debug.LogError("Item is null");
            return;
        }
        else
        { 
            Debug.Log($"Item: {item.Name}");

        }
        // rest of your code
    }


    void Start()
    {
        Item sword = Resources.Load<Item>("Item/Sword");
        //       Debug.Log(sword ? $"Loaded item yes: {sword.Name}" : "Failed to load item: Sword");
        Item stone = Resources.Load<Item>("Item/Stone");
        Item wood = Resources.Load<Item>("Item/Wood");
        
        CreateItemButton(sword);
        CreateItemButton(stone);
        CreateItemButton(wood);
    }

}
