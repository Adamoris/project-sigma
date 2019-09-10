using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardHelper : Editor
{
    SerializedProperty ruleset;
    SerializedProperty summonable;
    SerializedProperty unlocked;
    SerializedProperty unitName;
    SerializedProperty iv;
    SerializedProperty gender;
    SerializedProperty moveClass;
    SerializedProperty damageType;
    SerializedProperty range;

    SerializedProperty pma;
    SerializedProperty pra;
    SerializedProperty mma;
    SerializedProperty mra;

    SerializedProperty pmc;
    SerializedProperty prc;
    SerializedProperty mmc;
    SerializedProperty mrc;

    SerializedProperty pmf;
    SerializedProperty prf;
    SerializedProperty mmf;
    SerializedProperty mrf;

    SerializedProperty pmi;
    SerializedProperty pri;
    SerializedProperty mmi;
    SerializedProperty mri;

    SerializedProperty race;
    SerializedProperty level;
    SerializedProperty merge;
    SerializedProperty rarity;
    SerializedProperty portrait;
    SerializedProperty mapSprite;

    SerializedProperty baseHP;
    SerializedProperty baseAtk;
    SerializedProperty baseSpd;
    SerializedProperty baseDef;
    SerializedProperty baseRes;

    SerializedProperty growthHP;
    SerializedProperty growthAtk;
    SerializedProperty growthSpd;
    SerializedProperty growthDef;
    SerializedProperty growthRes;

    SerializedProperty assist;
    SerializedProperty slotA;
    SerializedProperty slotB;
    SerializedProperty slotC;
    SerializedProperty slotD;
    SerializedProperty special;
    SerializedProperty weapon;

    SerializedProperty HP;
    SerializedProperty Atk;
    SerializedProperty Spd;
    SerializedProperty Def;
    SerializedProperty Res;

    SerializedProperty modifierHP;

    private void OnEnable()
    {
        pma = serializedObject.FindProperty("physicalMeleeArmor");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Card card = target as Card;

        //EditorGUILayout.Space();
        //EditorGUILayout.LabelField("Unit General Information", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(iv);

        if (card.moveClass == Card.MoveClass.Armor && card.damageType == Card.DamageType.Physical && card.range == Card.Range.Melee)
        {
            EditorGUILayout.PropertyField(pma);
        }

        if (GUILayout.Button("Generate EXP Spread"))
        {
            card.GenerateEXPSpread();

            if (card.levelListHP.Length == 0)
            {
                Debug.Log("Error: rarity has not been set.");
            }
            else
            {
                Debug.Log("EXP Spread for " + card.name + " generated.");
            }
        }
        if (GUILayout.Button("Level-Up"))
        {
            card.LevelUp();
        }
        if (GUILayout.Button("Level-Down"))
        {
            card.LevelDown();
        }
        if (GUILayout.Button("Initialize Stats"))
        {
            card.InitializeStats();
        }
        if (GUILayout.Button("Reset Stats"))
        {
            card.ResetStats();
            card.GenerateEXPSpread();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
