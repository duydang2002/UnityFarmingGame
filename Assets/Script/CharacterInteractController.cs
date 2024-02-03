using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInteractController : MonoBehaviour
{
    MainCharacterControl characterControl;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 0.4f;
    Character character;
    [SerializeField] HightlightController hightlightController;

    public void Awake()
    {
        characterControl = GetComponent<MainCharacterControl>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Check();
        if (Input.GetKeyDown("f"))
        {
            Interact();
        }
    }
    // Neu co colide va sau kho an F thi goi ham interact
    private void Interact()
    {
        // Tinh vi tri cua nhan vat cong them vi tri truoc mat cua MC 
        Vector2 position = rgbd2d.position + characterControl.lastMotionVector*offsetDistance;

        Collider2D[] collider2s = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in collider2s)
        {
            Interactable hit = c.GetComponent<Interactable>();

            if (hit != null)
            {
                //Debug.Log(hit);
                hit.Interact(character);
                break;
            }
        }
    }
    // Kiem tra xem co collide khong
    public void Check()
    {
        Vector2 position = rgbd2d.position + characterControl.lastMotionVector * offsetDistance;

        Collider2D[] collider2s = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in collider2s)
        {
            Interactable hit = c.GetComponent<Interactable>();

            if (hit != null)
            {
                    hightlightController.Hightlight(hit.gameObject);
                    return;
            }
        }
        hightlightController.HideHighlighter();
    }
}
