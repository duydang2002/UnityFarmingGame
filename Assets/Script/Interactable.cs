using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

// This class can be overrided by sub-classes of Interactable to
// process different interact activities between characters and 
// objects in the game
public class Interactable : MonoBehaviour
{
  public virtual void Interact(Character character)
  {

  }
}
