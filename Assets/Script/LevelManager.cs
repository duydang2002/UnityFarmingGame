using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour

{
    public TextMeshProUGUI levelStr;
    public RectTransform expImg;
    public static int currentLevel=1;
    public static int maxLevel = 100;
    public static int[] expToNextLevel = new int[maxLevel];
    public static int currentExp = 0;

    private float expBarMaxWidth = 64;
    private float expBarCurrentWidth;
    private float expBarHeight = 43;
    public static int getCurrentMaxExp(int currentLevel)
    {
        return expToNextLevel[currentLevel];
    }
    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel[1] = 100;
        for(int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.25f);
        }
        setLevelString();
        setExpBarWidth();
    }

    // Update is called once per frame
    void Update()
    {
        setLevelString();
        setExpBarWidth();
        if (currentExp >= expToNextLevel[currentLevel] && currentLevel < maxLevel)
        {
            currentLevel += 1;
            currentExp -= expToNextLevel[currentLevel - 1];
        }
    }
    public void AddExp(int expToAdd)
    {
        currentExp += expToAdd;
        /*if(currentExp >= expToNextLevel[currentLevel] && currentLevel < maxLevel) {
            currentLevel += 1;
            currentExp -= expToNextLevel[currentLevel-1];
        }*/
    }

   /* private void FixedUpdate()
    {
      
    }*/
    public void setExpBarWidth()
    {
        expBarCurrentWidth = (float)currentExp / (float)expToNextLevel[currentLevel] * expBarMaxWidth;
        expImg.sizeDelta = new Vector2(expBarCurrentWidth, expBarHeight);
        //Debug.Log(expBarCurrentWidth);
    }
    public void setLevelString()
    {
        levelStr.text = currentLevel.ToString();
    }
    public string getLevel()
    {
        return currentLevel.ToString();
    }
}
