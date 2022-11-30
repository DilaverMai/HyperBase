﻿using UnityEditor;
using UnityEngine;

public class CustomTileData : PropertyDrawer 
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        
        Rect newPosition = position;
        newPosition.y += 18f;
        SerializedProperty rows = property.FindPropertyRelative("Inventory");
        
        for(int i=0; i < 10; i++)
        {
            SerializedProperty row = rows.GetArrayElementAtIndex(i).FindPropertyRelative("InventoryGrid");
            newPosition.height = 20;

            if (row.arraySize != 10)
                row.arraySize = 10;

            newPosition.width = 70;

            for(int j=0; j < 10; j++)
            {
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                newPosition.x += newPosition.width;
            }

            newPosition.x = position.x;
            newPosition.y += 20;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20 * 12;
    }
}