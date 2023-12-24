using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalSpawner : MonoBehaviour
{
    [SerializeField] DialogueAssetCreator assetCreator;
    [SerializeField] NPCController npcController;
    [SerializeField] TextMeshProUGUI questText;
    [SerializeField] CreateGoalAsset goalContainerCreator;
    int lastLevel = LevelManager.currentLevel;
    //[SerializeField] GoalController goalController;

    #region MonoBehavioiurs
    void Awake()
    {
        SpawnGoal();
    }
    public void SpawnGoal()
    {
        List<Objective> goals = new List<Objective>(5);
        for (int i = 0; i < LevelManager.currentLevel % 5; i++)
        {   
            goals.Add(ObjectiveFactory.CreateGoal(i +1));
        }
        //goalController = new GoalController(goals);
        assetCreator.CreateDialogueAsset(goals);
        goalContainerCreator.CreateGoal(goals);

    }
    void Update()
    {
        if (lastLevel < LevelManager.currentLevel)
        {
            SpawnGoal();
            lastLevel = LevelManager.currentLevel;
        }    
    }
    #endregion MonoBehavioiurs
}