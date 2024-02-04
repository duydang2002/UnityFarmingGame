using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class DialogueAssetCreator : MonoBehaviour
{
    [SerializeField] NPCController nPCController;
    [ContextMenu("Create Dialogue Asset")]

    public void CreateQuest(List<Objective> goal) 
    { 
        CreateDialogueAsset(goal);
    }
    public void CreateDialogueAsset(List<Objective> goal)
    {
        // Reference the existing NPC asset
        NPC existingNPC = Resources.Load<NPC>("NPC"); // Adjust the path as needed

        // Create a new instance of DialogueContainer
        DialogueContainer dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        dialogueContainer.line = new List<string>(goal.Count + 1)
        {
            "Here is your quest"
        };

        for (int i =0; i < goal.Count; i++) 
        {
            
            dialogueContainer.line.Add($"{goal.ElementAt(i).Description}");

        }
        // dinh npc cho container
        dialogueContainer.npc = existingNPC;

        // Create the asset file for DialogueContainer
#if UNITY_EDITOR
        string dialogueAssetPath = "Assets/Resources/Dialogues/CurrentDialogue.asset"; // Set your desired path and filename
        AssetDatabase.CreateAsset(dialogueContainer, dialogueAssetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Dialogue Asset created at: " + dialogueAssetPath);
#endif

    }

}