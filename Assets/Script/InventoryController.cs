using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolbarPanel;
    private void Awake()
    {
        panel.SetActive(false);
        toolbarPanel.SetActive(true);
    }
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.I))
        {
            panel.SetActive(!panel.activeSelf);
            
        }
    }
}