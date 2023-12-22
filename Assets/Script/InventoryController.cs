using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private void Awake()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            Debug.Log("I key was pressed");
            panel.SetActive(!panel.activeInHierarchy);
        }
    }

}
