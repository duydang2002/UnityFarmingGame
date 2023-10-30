using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(ItemContainer))]

public class ItemContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemContainer container = target as ItemContainer;
        if (GUILayout.Button("Clear container"))
        {
            for (int i = 0; i < container.slot.Count; ++i)
            {
                container.slot[i].item = null;
                container.slot[i].count = 0;
            }
        }
        DrawDefaultInspector();
    }
}
