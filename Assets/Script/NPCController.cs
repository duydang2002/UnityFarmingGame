using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    [SerializeField] HightlightController hightlightController;
    [SerializeField] TextMeshProUGUI questText;
    int lastLevel;
    [SerializeField] bool questOn = false;
    [SerializeField] bool questAccept = false;
    [SerializeField] bool interacting = false;
    public static bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
        animatorMove = GetComponent<Animator>();
        lastLevel = levelManager.getLevel();
        questText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            string assetPath = "Dialogues/" + "Greeting";
            dialogueContainer = Resources.Load<DialogueContainer>(assetPath);
            return;
        }
        if (lastLevel != levelManager.getLevel())
        {
            questOn = true;
          
        }
        if (questOn)
        {
            string assetPath = "Dialogues/" + "CurrentDialogue" ;
            dialogueContainer = Resources.Load<DialogueContainer>(assetPath);
            hightlightController.QuestAppear(gameObject);
        }
        else
        {
            hightlightController.HideQuestMark();
            string assetPath = "Dialogues/" + "Idle" ;
            dialogueContainer = Resources.Load<DialogueContainer>(assetPath);
            
        }
        if (!questAccept)
            questText.gameObject.SetActive(false);
        else questText.gameObject.SetActive(true);

    }
    public override void Interact(Character character)
    {
        interacting = true;
        float horizontal = transform.position.y - player.position.y;
        float vertical = transform.position.x - player.position.x;
        
        Debug.Log(horizontal + " " + vertical);
        animatorMove.SetFloat("Horizontal", -horizontal);
        animatorMove.SetFloat("Vertical", -vertical);

        //introPanel.SetActive(true);
        GameManager.instance.dialogueSystem.Initialize(dialogueContainer);
        if (questOn)
        {
            questAccept = true;
        }


    }
    public void Hide()
    {
        introPanel.SetActive(false);
    }
    public bool getInteracting()
    {
        return interacting;
    }
    public void setInteracting(bool interact)
    {
        interacting = interact;
       
    }
    public void setIdle()
    {
        questOn = false;
        questText.gameObject.SetActive(false);
        questText.text = "";
        questAccept = false;
        lastLevel = LevelManager.currentLevel;
    }
    public bool getQuestAccept()
    {
        return questAccept;
    }
    public bool GetQuestOn()
    {
        return questOn;
    }
    public void SetQuestOn()
    {
        questOn = true;
    }
}
