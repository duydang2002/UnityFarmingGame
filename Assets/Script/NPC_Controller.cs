using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NPC_Controller : Interactable
{

    private BoxCollider2D boxCollider;
    Transform player;
    Animator animatorMove;

    [SerializeField] GameObject introPanel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
        boxCollider = GetComponent<BoxCollider2D>();
        animatorMove = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    public override void Interact(Character character)
    {
        float horizontal = transform.position.y - player.position.y;
        float vertical = transform.position.x - player.position.x;
        
        Debug.Log(horizontal + " " + vertical);
        animatorMove.SetFloat("Horizontal", -horizontal);
        animatorMove.SetFloat("Vertical", -vertical);

        introPanel.SetActive(true);

    }
    public void Hide()
    {
        introPanel.SetActive(false);
    }
}
