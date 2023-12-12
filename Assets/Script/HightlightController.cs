using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightlightController : MonoBehaviour
{
    [SerializeField] GameObject hightlighter;
    [SerializeField] GameObject questMark;
    GameObject currentTarget;
    public void Hightlight(GameObject target)
    {
        if (currentTarget == target)
        {
            return;
        }
        currentTarget = target;
        Vector3 position = target.transform.position;
        Hightlight(position);
    }
    public void Hightlight(Vector3 position)
    {
        hightlighter.SetActive(true);
        position.y += 0.7f;
        hightlighter.transform.position = position;
        
    }
    public void QuestAppear(GameObject target)
    {
        if (currentTarget == target)
        {
            return;
        }
        currentTarget = target;
        Vector3 position = target.transform.position;
        QuestAppear(position);
    }
    public void QuestAppear(Vector3 position)
    {
        questMark.SetActive(true);
        position.y += 0.5f;
        questMark.transform.position = position;
    }

    public void Hide()
    {
        currentTarget = null;
        hightlighter.SetActive(false);
    }
}
