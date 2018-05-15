using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Area))]
//[CanEditMultipleObjects]
public class AreaEditor : Editor {
    
    bool isFold = false;
    SerializedProperty tiles;

    void OnEnable()
    {
        tiles = serializedObject.FindProperty("tiles");
    }

    public override void OnInspectorGUI()
    {
        isFold = EditorGUILayout.Foldout(isFold, "Tiles", false);
        if (isFold)
        {
            for(int i = 0; i < 7; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(tiles, true);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EnumFlagsField("TilesType", TileTypes.normaltile);
        serializedObject.ApplyModifiedProperties();
    }
    
    
}
