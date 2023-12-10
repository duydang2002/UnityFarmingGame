using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NPC_Controller : Interactable
{

    private BoxCollider2D boxCollider;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    public override void Interact(Character character)
    {
        
    }
}
