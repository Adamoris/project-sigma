using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardStatRandomization : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Card card = target as Card;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Additional Tools", EditorStyles.boldLabel);

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
        if (GUILayout.Button("Level-Up") && card.level <= card.levelingRange)
        {
            card.LevelUp();
        }
        if (GUILayout.Button("Level-Down") && card.level > 1)
        {
            card.LevelDown();
        }
        if (GUILayout.Button("Reset Stats"))
        {
            Debug.Log("Recommended: Generate a new EXP Spread!");
            card.ResetStats();
        }
    }
}
