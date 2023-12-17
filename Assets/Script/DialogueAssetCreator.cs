using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DialogueAssetCreator : MonoBehaviour
{
    
    [ContextMenu("Create Dialogue Asset")]
    public void CreateDialogueAsset(List<Objective> goal)
    {
        // Reference the existing NPC asset
        NPC existingNPC = Resources.Load<NPC>("NPC"); // Adjust the path as needed

        // Create a new instance of DialogueContainer
        DialogueContainer dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        dialogueContainer.line = new List<string>();
        dialogueContainer.line.Add("Here is your quest");
        foreach (Objective obj in goal)
        {
            dialogueContainer.line.Add(obj.Description);
        }
        dialogueContainer.npc = existingNPC;

        // Create the asset file for DialogueContainer
        string dialogueAssetPath = "Assets/Resources/Dialogues/CurrentDialogue.asset"; // Set your desired path and filename
        AssetDatabase.CreateAsset(dialogueContainer, dialogueAssetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Dialogue Asset created at: " + dialogueAssetPath);
    }
}