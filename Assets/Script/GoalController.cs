using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    GoalContainer goalContainer;
    private bool isFinished = false;
    [SerializeField] TextMeshProUGUI questText;
    string assetPath = "Goals/" + "CurrentGoals";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
        {

            return; 
        }
        goalContainer = Resources.Load<GoalContainer>(assetPath);
        if (goalContainer != null)
        {
            if (goalContainer.CheckValueChange())
            {
                questText.text = "";
                for (int i = 0; i < goalContainer.keys.Count; i++)
                {
                    questText.text += $"{goalContainer.keys[i]} ({goalContainer.values[i]}/ {goalContainer.require[i]}) \n";
                }
                for (int i = 0; i < goalContainer.keys.Count; i++)
                {
                    if (goalContainer.values[i] < goalContainer.require[i])
                    {
                        return;
                    }
                }
                isFinished = true;

            }
        }
    }


}
