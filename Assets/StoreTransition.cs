using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreTransition : MonoBehaviour
{
    public string sceneName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
