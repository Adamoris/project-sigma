using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Equipment))]
public class EquipmentInspector : Editor
{
    SerializedProperty equipmentName;
    SerializedProperty slot;
    SerializedProperty description;
    SerializedProperty cost;
    SerializedProperty prerequisite;
    SerializedProperty icon;

    private void OnEnable()
    {
        equipmentName = serializedObject.FindProperty("name");
        slot = serializedObject.FindProperty("slot");
        description = serializedObject.FindProperty("description");
        cost = serializedObject.FindProperty("cost");
        prerequisite = serializedObject.FindProperty("prerequisite");
        icon = serializedObject.FindProperty("icon");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(equipmentName, new GUIContent("Name"));
        EditorGUILayout.PropertyField(slot);
        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(cost);
        EditorGUILayout.PropertyField(prerequisite);

        EditorGUILayout.PropertyField(icon);

        Equipment equipment = target as Equipment;

        switch (equipment.slot)
        {
            case Equipment.Slot.A:
                break;
            case Equipment.Slot.B:
                break;
            case Equipment.Slot.C:
                break;
            case Equipment.Slot.D:
                break;
        }


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Inheritability", EditorStyles.boldLabel);
        equipment.inheritable = EditorGUILayout.Toggle("Inheritable", equipment.inheritable);

        if (equipment.inheritable)
        {
            equipment.sword = EditorGUILayout.Toggle("Sword", equipment.sword);
            equipment.axe = EditorGUILayout.Toggle("Axe", equipment.axe);
            equipment.lance = EditorGUILayout.Toggle("Lance", equipment.lance);
            equipment.dagger = EditorGUILayout.Toggle("Dagger", equipment.dagger);
            equipment.bow = EditorGUILayout.Toggle("Bow", equipment.bow);
            equipment.magic = EditorGUILayout.Toggle("Magic", equipment.magic);
            equipment.beast = EditorGUILayout.Toggle("Beast", equipment.beast);
        }

        /*
        switch (weapon.range)
        {
            case Weapon.Range.Melee:
                weapon.DistantCounter = EditorGUILayout.Toggle("Distant Counter", weapon.DistantCounter);
                break;

            case Weapon.Range.Ranged:
                weapon.CloseCounter = EditorGUILayout.Toggle("Close Counter", weapon.CloseCounter);
                break;
        }
        
        //base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
        */
        serializedObject.ApplyModifiedProperties();
    }
}
