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
        // Initialize references and set initial quest states
        player = GameManager.instance.player.transform;
        animatorMove = GetComponent<Animator>();
        lastLevel = levelManager.getLevel();
        questText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Neu moi bat dau game thi load greeting
        if (start)
        {
            // Load initial greeting dialogue
            string assetPath = "Dialogues/" + "Greeting";
            dialogueContainer = Resources.Load<DialogueContainer>(assetPath);
            return;
        }
        else
        {
            // Reset dialogue container
            dialogueContainer = null;
        }
        // Neu len Level thi se bat quest len con khong thi de NPC la idle
        if (lastLevel != levelManager.getLevel())
        {
            questOn = true;
        }
        if (questOn)
        {
            string assetPath = "Dialogues/" + "CurrentDialogue";
            //Debug.Log(start);
            dialogueContainer = Resources.Load<DialogueContainer>(assetPath);
            hightlightController.QuestAppear(gameObject);
        }
        else
        {
            hightlightController.HideQuestMark();
            string assetPath = "Dialogues/" + "Idle";
            dialogueContainer = Resources.Load<DialogueContainer>(assetPath);

        }
        // Bat tien trinh nhiem vu
        if (!questAccept)
            questText.gameObject.SetActive(false);
        else questText.gameObject.SetActive(true);

    }
    public override void Interact(Character character)
    {
        interacting = true;
        // cho NPC quay nguoc ve vi tri cua nhan vat chinh
        float horizontal = transform.position.y - player.position.y;
        float vertical = transform.position.x - player.position.x;

        // Set animator parameters for NPC movement
        //Debug.Log(horizontal + " " + vertical);
        animatorMove.SetFloat("Horizontal", -horizontal);
        animatorMove.SetFloat("Vertical", -vertical);

        Debug.Log(dialogueContainer.line);
        // Khoi tao hop thoai cho NPC
        GameManager.instance.dialogueSystem.Initialize(dialogueContainer);

        // dat trang thai thanh Accept Quest
        if (questOn)
        {
            questAccept = true;
        }


    }
    // Hide intro panel
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
    // Set NPC to idle state and update quest-related variables
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
