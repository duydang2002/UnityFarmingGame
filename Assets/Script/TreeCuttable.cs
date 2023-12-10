using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TreeCuttable : Interactable
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;
    [SerializeField] GameObject highLightMarker;

    public override void Interact(Character character)
    {
        while (dropCount > 0)
        {
            dropCount--;
            Vector3 position = transform.position;
            position.x += spread *UnityEngine.Random.value-spread/2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(pickUpDrop);
            GameObject go2 = Instantiate(highLightMarker);
            
            go.transform.position = position;
            position.y += 0.5f;
            go2.transform.position = position;
        }
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
