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
            /*
            Debug.Log(card.HP_rate);
            Debug.Log("Bronze: " + card.GetGrowthValue(card.HP_rate, Card.Rarity.Bronze));
            Debug.Log("Silver: " + card.GetGrowthValue(60, Card.Rarity.Silver));
            Debug.Log("Gold: " + card.GetGrowthValue(60, Card.Rarity.Gold));
            int[] test = card.RandomEXP(60, Card.Rarity.Gold);
            foreach (int value in test)
            {
                Debug.Log(value);
            }
            */
            card.levelListHP = card.RandomEXP(card.HP_rate, card.rarity);
            card.levelListAtk = card.RandomEXP(card.Atk_rate, card.rarity);
            card.levelListSpd = card.RandomEXP(card.Spd_rate, card.rarity);
            card.levelListDef = card.RandomEXP(card.Def_rate, card.rarity);
            card.levelListRes = card.RandomEXP(card.Res_rate, card.rarity);

            if (card.levelListHP.Length == 0)
            {
                Debug.Log("Error: rarity has not been set.");
            }
            else
            {
                Debug.Log("EXP Spread for " + card.name + " generated.");
            }
        }

        //card.levelListHP = EditorGUILayout.PropertyField()
    }
}
