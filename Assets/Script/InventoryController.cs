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
    }
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.I))
        {
            Debug.Log(panel.activeSelf);

            if (panel.activeSelf == true)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
            //panel.SetActive(!panel.activeSelf);
            toolbarPanel.SetActive(!toolbarPanel.activeSelf);            
        }
    }
}