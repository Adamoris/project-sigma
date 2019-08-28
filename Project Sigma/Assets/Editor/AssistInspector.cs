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
            case Assist.AssistType.Action:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Effect Details", EditorStyles.boldLabel);
                assist.rangeCast = EditorGUILayout.IntField("Casting Range", assist.rangeCast);
                assist.rangeEffect = EditorGUILayout.IntField("Effect Range", assist.rangeEffect);

                DefaultAdditionalDetails();

                break;
            case Assist.AssistType.Heal:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Heal", EditorStyles.boldLabel);
                assist.healAmount = EditorGUILayout.IntField("HP to be healed", assist.healAmount);

                DefaultEffectDetails();

                break;
            case Assist.AssistType.Positional:
                //EditorGUILayout.Space();
                //EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Effect Details", EditorStyles.boldLabel);
                //assist.target = (Assist.Target)EditorGUILayout.EnumPopup("Target", assist.target);
                assist.rangeCast = EditorGUILayout.IntField("Casting Range", assist.rangeCast);
                assist.positionTypes = (Assist.PositionTypes)EditorGUILayout.EnumPopup("Skill Type", assist.positionTypes);
                //assist.orientation = (Assist.Orientation)EditorGUILayout.EnumPopup("Orientation", assist.orientation);
                break;
            case Assist.AssistType.Stats:
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
                assist.buffAtk = EditorGUILayout.IntField("Atk", assist.buffAtk);
                assist.buffSpd = EditorGUILayout.IntField("Spd", assist.buffSpd);
                assist.buffDef = EditorGUILayout.IntField("Def", assist.buffDef);
                assist.buffRes = EditorGUILayout.IntField("Res", assist.buffRes);

                DefaultEffectDetails();

                DefaultAdditionalDetails();
                break;
        }
        void DefaultEffectDetails()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Effect Details", EditorStyles.boldLabel);
            assist.rangeCast = EditorGUILayout.IntField("Casting Range", assist.rangeCast);
            assist.rangeEffect = EditorGUILayout.IntField("Effect Range", assist.rangeEffect);
            assist.assistDuration = EditorGUILayout.IntField("Effect Duration", assist.assistDuration);
            assist.target = (Assist.Target)EditorGUILayout.EnumPopup("Target", assist.target);
        }
        void DefaultAdditionalDetails()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Additional Effects", EditorStyles.boldLabel);
            assist.negatePenalties = EditorGUILayout.Toggle("Negate Penalties", assist.negatePenalties);
            assist.movementModifier = EditorGUILayout.IntField("Movement Modifier", assist.movementModifier);
            assist.countdownAcceleration = EditorGUILayout.IntField("CD Acceleration Modifier", assist.countdownAcceleration);
        }
    }
}
