using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public TextMeshProUGUI expStr;
    [SerializeField]public int currentExp=0;
    [SerializeField]public int maxExp=0;
    [SerializeField] public int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
        getExpInfo();
        expStr.text = currentExp.ToString() +"/" +maxExp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        getExpInfo();
        expStr.text = currentExp.ToString() + "/" + maxExp.ToString();
    }
    public void getExpInfo()
    {
        currentExp = LevelManager.currentExp;
        currentLevel= LevelManager.currentLevel;
        maxExp = LevelManager.getCurrentMaxExp(currentLevel) ;
    }
}
