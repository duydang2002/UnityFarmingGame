using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NPCController : Interactable
{

    Transform player;
    Animator animatorMove;

    DialogueContainer dialogueContainer;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameObject introPanel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
        animatorMove = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        string assetPath = "Dialogues/" +"Lv" + levelManager.getLevel();  
        dialogueContainer= Resources.Load<DialogueContainer>(assetPath);

    }
    public override void Interact(Character character)
    {
        float horizontal = transform.position.y - player.position.y;
        float vertical = transform.position.x - player.position.x;
        
        Debug.Log(horizontal + " " + vertical);
        animatorMove.SetFloat("Horizontal", -horizontal);
        animatorMove.SetFloat("Vertical", -vertical);

        //introPanel.SetActive(true);
        GameManager.instance.dialogueSystem.Initialize(dialogueContainer);

    }
    public void Hide()
    {
        introPanel.SetActive(false);
    }
}
