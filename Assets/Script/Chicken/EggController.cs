using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class EggsController : MonoBehaviour
{
    //[SerializeField] GameObject chickenEggsBorder;
    [SerializeField] GameObject chickenEggsProgress;
    [SerializeField] RectTransform chickenEggsRectTransform;
    [SerializeField]GameObject chickenEggs;
    [SerializeField] GameObject highlightIcon;
    Transform player;
    public Item item;
    public int count = 3;
    [SerializeField] GameObject character;
    

    [SerializeField] float speed = 5f;

   
    private float eggBarMaxWidth = 4f;
    private float eggBarCurrentWidth;
    private float eggBarHeight = 1f;
    bool pickUp = false;
    public float currentEggTime = 0;
    public float maxBetweenEggTime = 10; // 10s
    private bool eggState = true;
    public bool isCollision;
    // Start is called before the first frame update
    void Start()
    {

        player = character.transform;
       
        if (eggState)
        {
            chickenEggs.SetActive(true);
            chickenEggsProgress.SetActive(false);
        }
        else
        {
            chickenEggs.SetActive(false);
            chickenEggsProgress.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!eggState)
        {
            currentEggTime += Time.deltaTime;
        
            //Debug.Log(currentEggTime);
            setChickensEggProgress();
            if (currentEggTime >= maxBetweenEggTime)
            {
                currentEggTime = 0;

                chickenEggsProgress.SetActive(false);
                chickenEggs.SetActive(true);
                eggState = true;

            }
        }
        else
        {
            collectEggs();
        }





    }


    public void setChickensEggProgress()
    {
        chickenEggsProgress.SetActive(true);
        eggBarCurrentWidth = (float)currentEggTime / (float)maxBetweenEggTime * eggBarMaxWidth;
       
        chickenEggsRectTransform.localScale = new Vector2(eggBarCurrentWidth, eggBarHeight);
    }
    

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if (eggState)
            {
                highlightIcon.SetActive(true);
            }
            isCollision = true;

        }
    }
    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collision");
            highlightIcon.SetActive(false);
            isCollision = false;
        }
    }
    public void collectEggs()
    {

        if (eggState == true)
        {

            if (Input.GetKeyDown("f"))
            {
                pickUp = true;
                chickenEggs.GetComponent<BoxCollider2D>().enabled = false;
            }
            else pickUp = false;

            if (pickUp == false)
            {
                return;
            }

            Debug.Log("Pick up " + pickUp);


            if (isCollision && pickUp)
            {
                // Use to smoothy move from lag to player
                // Mul deltaTime to make it frame rate independent
                chickenEggs.transform.position = Vector3.MoveTowards(
                    chickenEggs.transform.position,
                    player.position,
                    speed * Time.deltaTime);
                // *TODO* should be moved into specified controller rather than being checked here
                if (GameManager.instance.inventoryContainer != null)
                {
                    GameManager.instance.inventoryContainer.Add(item, count);
                }
                else
                {
                    Debug.LogWarning("No inventory container attached to the game manager");
                }

                GameManager.instance.toolBarPanel.SetActive(false);
                GameManager.instance.toolBarPanel.SetActive(true);
                eggState = false;
                chickenEggs.GetComponent<BoxCollider2D>().enabled= true;
                chickenEggs.SetActive(false);
                highlightIcon.SetActive(false);
                pickUp = false;
              
                //gameObject.SetActive(false);
                //Destroy(chickenEggs);

            }
        }
    }
}
