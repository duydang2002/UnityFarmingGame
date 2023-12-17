using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalSpawner : MonoBehaviour
{
    DialogueAssetCreator assetCreator;
    [SerializeField] NPCController npcController;
    [SerializeField] TextMeshProUGUI questText;
    #region MonoBehavioiurs
    void Start()
    {
        List<Objective> goals = new List<Objective>(5);
        for (int i = 0; i < 5; i++)
        {
            goals.Add(ObjectiveFactory.CreateGoal(i + 1));
        }
        assetCreator = new DialogueAssetCreator();
        assetCreator.CreateDialogueAsset(goals);
        foreach (Objective obj in goals)
        {
            questText.text += obj.Description + "\n";
        }
        questText.gameObject.SetActive(false);
    }
    #endregion MonoBehavioiurs
}