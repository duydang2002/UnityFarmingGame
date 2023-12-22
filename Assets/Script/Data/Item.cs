using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{

    // Start is called before the first frame update
    public string Name;
    public bool stackable;
    public Sprite icon;
    public ToolAction onAction;
}
