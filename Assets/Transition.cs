using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionType
{
    Warp,
    Scene
}

public class Transition : MonoBehaviour
{
    [SerializeField] TransitionType transitionType = TransitionType.Warp;
    [SerializeField] string sceneNameToTransition;
    [SerializeField] Vector3 targetPosition;

    // Start is called before the first frame update
    Transform destination;

    void Start()
    {
        destination = transform.GetChild(1);
    }

    // Update is called once per frame
    internal void InitiateTransition(Transform toTransition)
    {
        switch(transitionType)
        {
            case TransitionType.Warp:
                toTransition.position = new Vector3(
                    destination.position.x,
                    destination.position.y,
                    toTransition.position.z);
                break;
            case TransitionType.Scene:
                GameSceneManager.instance.SwitchScene(sceneNameToTransition, targetPosition);
                break;
            default:
                break;
        }

         
        
    }
}
