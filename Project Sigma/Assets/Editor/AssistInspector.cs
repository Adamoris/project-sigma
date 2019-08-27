using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Assist))]
public class AssistInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        Assist assist = target as Assist;

        switch (assist.assistType)
        {
            case Assist.AssistType.Heal:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Heal", EditorStyles.boldLabel);
                assist.healAmount = EditorGUILayout.IntField("HP to be healed", assist.healAmount);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Effect Details", EditorStyles.boldLabel);
                assist.assistDuration = EditorGUILayout.IntField("Effect Duration", assist.assistDuration);
                assist.target = (Assist.Target)EditorGUILayout.EnumPopup("Target", assist.target);
                break;
            case Assist.AssistType.Positional:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Effect Details", EditorStyles.boldLabel);
                assist.target = (Assist.Target)EditorGUILayout.EnumPopup("Target", assist.target);
                break;
            case Assist.AssistType.Stats:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
                assist.buffAtk = EditorGUILayout.IntField("Atk", assist.buffAtk);
                assist.buffSpd = EditorGUILayout.IntField("Spd", assist.buffSpd);
                assist.buffDef = EditorGUILayout.IntField("Def", assist.buffDef);
                assist.buffRes = EditorGUILayout.IntField("Res", assist.buffRes);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Effect Details", EditorStyles.boldLabel);
                assist.assistDuration = EditorGUILayout.IntField("Effect Duration", assist.assistDuration);
                assist.target = (Assist.Target)EditorGUILayout.EnumPopup("Target", assist.target);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Additional Effects", EditorStyles.boldLabel);
                assist.negatePenalties = EditorGUILayout.Toggle("Negate Penalties", assist.negatePenalties);
                break;
        }
    }
}
