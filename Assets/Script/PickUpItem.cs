using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : Interactable
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f;
    [SerializeField] float waitingSecond = 1f;

    private BoxCollider2D boxCollider;
    bool pickUp = false;
    public Item item;
    public int count = 1;

    private void Awake()
    {
        player = GameManager.instance.player.transform;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Delete if not pick up in time
        if(waitingSecond > 0f)
        {
            waitingSecond--;
            return;
        }
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
        }
        // Use Vector3 for better Compatibility and flexibility
        float distance = Vector3.Distance(transform.position, player.position);
        
        // Guard clause
        if(distance > pickUpDistance)
        {
            return;
        }

        if (Input.GetKeyDown("f"))
        {
            pickUp = true;
            boxCollider.enabled = false;
        }

        if(pickUp == false)
        {
            return;
        }
        

        // Use to smoothy move from lag to player
        // Mul deltaTime to make it frame rate independent
        transform.position = Vector3.MoveTowards(
            transform.position, 
            player.position, 
            speed * Time.deltaTime);

        if (distance < 0.1f){
            // *TODO* should be moved into specified controller rather than being checked here
            if (GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(item, count);
            }
            else
            {
                Debug.LogWarning("No inventory container attached to the game manager");
            }
            Destroy(gameObject);
            GameManager.instance.toolBarPanel.SetActive(false);
            GameManager.instance.toolBarPanel.SetActive(true);
            boxCollider.enabled = true;
        }
    }

    
    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }
}
