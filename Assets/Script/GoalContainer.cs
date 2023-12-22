using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quest/Quest")]
public class GoalContainer : ScriptableObject
{
    [SerializeField]
    public List<string> keys;

    [SerializeField]
    public List<int> values;

    [SerializeField]
    public List<int> require;


    public List<int> previousValues;

    public event Action<int> OnValueChange;

    public void AddEntry(string key, int value, int value2)
    {
        keys.Add(key);
        values.Add(value);
        require.Add(value2);
    }

    public void ClearEntries()
    {
        keys.Clear();
        values.Clear();
        require.Clear();
    }

    public void getInfor()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            // Your code here
        }
    }

    public bool CheckValueChange()
    {
       // Debug.Log(values.Count + " " + previousValues.Count);
        for (int i = 0; i < values.Count; i++)
        {  
            if (OnValueChange != null && values[i] != previousValues[i])
            {
                OnValueChange.Invoke(values[i]);
                return true;
            }
        }
        
        previousValues = new List<int>(values);
        return false;
    }

    private void HandleValueChange(int changedValue)
    {
        
    }

    private void OnEnable()
    {
        OnValueChange += HandleValueChange;
    }

    private void OnDisable()
    {
        OnValueChange -= HandleValueChange;
    }

}
