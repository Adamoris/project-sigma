using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Weapon weapon = target as Weapon;

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
    }
}
