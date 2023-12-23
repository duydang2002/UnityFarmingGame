using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CreateGoalAsset : MonoBehaviour
{
    DialogueContainer dialogueContainer;

    GoalContainer goalContainer;

    [SerializeField] TextMeshProUGUI questText;
    public void CreateGoal(List<Objective> goal)
    {
        goalContainer = ScriptableObject.CreateInstance<GoalContainer>();
        goalContainer.require = new List<int>(goal.Count);
        goalContainer.keys = new List<string>(goal.Count);
        goalContainer.values = new List<int>(goal.Count);
        goalContainer.previousValues = new List<int>(goal.Count);
        foreach (Objective obj in goal)
        {
            goalContainer.keys.Add(obj.Name);
            goalContainer.require.Add(obj.TargetValue);
            goalContainer.values.Add(0);
            goalContainer.previousValues.Add(0);
        }
        
        for (int i = 0; i < goalContainer.keys.Count; i++)
        {
            questText.text += $"{goalContainer.keys[i]} ({goalContainer.values[i]}/ {goalContainer.require[i]}) \n";
        }
        // Create the asset file for DialogueContainer
#if UNITY_EDITOR
        string goalAssetPath = "Assets/Resources/Goals/CurrentGoals.asset"; // Set your desired path and filename
        AssetDatabase.CreateAsset(goalContainer, goalAssetPath);

        Debug.Log("Quest Asset created at: " + goalAssetPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
