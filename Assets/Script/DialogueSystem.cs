using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI targetText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image portrait;

    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercent;
    [SerializeField] float timePerletter = 0.05f;
    [SerializeField] GameObject levelManager;
    float totalTimetoType, currentTime;
    string lineToShow;

    DialogueContainer currentDialogue;
    int currentTextLine;

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PushText();
        }
        TypeOutText();
    }

    private void TypeOutText()
    {
        if(visibleTextPercent >= 1f)
        {
            return;
        }
        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimetoType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0f, 1f);
        UpdateText();
    }
    void UpdateText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercent);
        targetText.text = lineToShow.Substring(0, letterCount);
    }
    private void PushText()
    {
        if(visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            return;
        }

        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        } 
        else
        {
            CycleLine();
        }
    }
    void CycleLine()
    {
        lineToShow = currentDialogue.line[currentTextLine];
        totalTimetoType = lineToShow.Length * timePerletter;
        currentTime = 0f;
        visibleTextPercent = 0f;
        targetText.text = "";
        currentTextLine += 1;
    }

    public void Initialize(DialogueContainer dialogueContainer)
    {
        Show(true);
        currentDialogue = dialogueContainer;
        currentTextLine = 0;
        CycleLine();
        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        portrait.sprite = currentDialogue.npc.portrait;
        nameText.text = currentDialogue.npc.Name;
    }

    private void Conclude()
    {
        Debug.Log("The dialogue has ended");
        Show(false);
    }
    private void Show (bool state)
    {
        gameObject.SetActive(state);
    }
}
