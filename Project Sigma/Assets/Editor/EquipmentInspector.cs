using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Equipment))]
public class EquipmentInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Equipment equipment = target as Equipment;

        //GUILayout.Label("Inheritability");
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
        */
        //base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
