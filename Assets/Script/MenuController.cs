using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openMenuPanel()
    {
        if(menuPanel!=null)
        {
            menuPanel.SetActive(true);
        }
    }
    public void closeMenuPanel()
    {
        if(menuPanel!=null) { 
        menuPanel.SetActive(false);
        }
    }
}
