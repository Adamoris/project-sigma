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
    SerializedProperty combatOrder;

    private void OnEnable()
    {
        equipmentName = serializedObject.FindProperty("name");
        slot = serializedObject.FindProperty("slot");
        description = serializedObject.FindProperty("description");
        cost = serializedObject.FindProperty("cost");
        prerequisite = serializedObject.FindProperty("prerequisite");
        icon = serializedObject.FindProperty("icon");
        combatOrder = serializedObject.FindProperty("combatOrder");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(equipmentName, new GUIContent("Name"));
        EditorGUILayout.PropertyField(slot);
        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(cost);
        EditorGUILayout.PropertyField(prerequisite);
        EditorGUILayout.PropertyField(combatOrder);

        EditorGUILayout.PropertyField(icon);

        Equipment equipment = target as Equipment;
        if (equipment.slot != Equipment.Slot.D)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Inheritability", EditorStyles.boldLabel);
            equipment.inheritable = EditorGUILayout.Toggle("Inheritable", equipment.inheritable);

            if (equipment.inheritable)
            {
                equipment.meleeInherit = EditorGUILayout.Toggle("Melee", equipment.meleeInherit);
                equipment.rangeInherit = EditorGUILayout.Toggle("Range", equipment.rangeInherit);
                equipment.armorInherit = EditorGUILayout.Toggle("Armor", equipment.armorInherit);
                equipment.cavalryInherit = EditorGUILayout.Toggle("Cavalry", equipment.cavalryInherit);
                equipment.flierInherit = EditorGUILayout.Toggle("Flier", equipment.flierInherit);
                equipment.infantryInherit = EditorGUILayout.Toggle("Infantry", equipment.infantryInherit);
            }
        }
        

        switch (equipment.slot)
        {
            case Equipment.Slot.A:
                StatModifiers();
                NullifyWeaponEffectiveness();
                break;
            case Equipment.Slot.B:
                StatModifiers();
                NullifyWeaponEffectiveness();
                break;
            case Equipment.Slot.C:

                break;
            case Equipment.Slot.D:

                break;
        }

        void StatModifiers()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Stat Modifiers", EditorStyles.boldLabel);
            equipment.healthModifier = EditorGUILayout.IntField("HP modifier", equipment.healthModifier);
            equipment.atkModifier = EditorGUILayout.IntField("Atk modifier", equipment.atkModifier);
            equipment.spdModifier = EditorGUILayout.IntField("Spd modifier", equipment.spdModifier);
            equipment.defModifier = EditorGUILayout.IntField("Def modifier", equipment.defModifier);
            equipment.resModifier = EditorGUILayout.IntField("Res modifier", equipment.resModifier);
        }

        void NullifyWeaponEffectiveness()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Nullify Weapon Effectiveness", EditorStyles.boldLabel);
            equipment.armorShield = EditorGUILayout.Toggle("Armor", equipment.armorShield);
            equipment.cavalryShield = EditorGUILayout.Toggle("Cavalry", equipment.cavalryShield);
            equipment.flierShield = EditorGUILayout.Toggle("Flier", equipment.flierShield);
            equipment.infantryShield = EditorGUILayout.Toggle("Infantry", equipment.infantryShield);
        }

        void Threshold()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Effect Requirements", EditorStyles.boldLabel);
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
