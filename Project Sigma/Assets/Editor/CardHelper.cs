using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardHelper : Editor
{
    SerializedProperty ruleset;
    SerializedProperty summonable;
    SerializedProperty unitName;
    SerializedProperty description;
    SerializedProperty race;
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
    
    SerializedProperty rarity;
    SerializedProperty portrait;
    SerializedProperty mapSprite;

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
    SerializedProperty modifierAtk;
    SerializedProperty modifierSpd;
    SerializedProperty modifierDef;
    SerializedProperty modifierRes;

    SerializedProperty levelListHP;
    SerializedProperty levelListAtk;
    SerializedProperty levelListSpd;
    SerializedProperty levelListDef;
    SerializedProperty levelListRes;

    private void OnEnable()
    {
        ruleset = serializedObject.FindProperty("ruleset");
        summonable = serializedObject.FindProperty("summonable");
        unitName = serializedObject.FindProperty("name");
        description = serializedObject.FindProperty("description");
        race = serializedObject.FindProperty("race");
        gender = serializedObject.FindProperty("gender");
        moveClass = serializedObject.FindProperty("moveClass");
        damageType = serializedObject.FindProperty("damageType");
        range = serializedObject.FindProperty("range");

        pma = serializedObject.FindProperty("pma");
        pra = serializedObject.FindProperty("pra");
        mma = serializedObject.FindProperty("mma");
        mra = serializedObject.FindProperty("mra");

        pmc = serializedObject.FindProperty("pmc");
        prc = serializedObject.FindProperty("prc");
        mmc = serializedObject.FindProperty("mmc");
        mrc = serializedObject.FindProperty("mrc");

        pmf = serializedObject.FindProperty("pmf");
        prf = serializedObject.FindProperty("prf");
        mmf = serializedObject.FindProperty("mmf");
        mrf = serializedObject.FindProperty("mrf");

        pmi = serializedObject.FindProperty("pmi");
        pri = serializedObject.FindProperty("pri");
        mmi = serializedObject.FindProperty("mmi");
        mri = serializedObject.FindProperty("mri");

        rarity = serializedObject.FindProperty("rarity");

        portrait = serializedObject.FindProperty("portrait");
        mapSprite = serializedObject.FindProperty("mapSprite");

        assist = serializedObject.FindProperty("assist");
        slotA = serializedObject.FindProperty("slotA");
        slotB = serializedObject.FindProperty("slotB");
        slotC = serializedObject.FindProperty("slotC");
        slotD = serializedObject.FindProperty("slotD");
        special = serializedObject.FindProperty("special");
        weapon = serializedObject.FindProperty("weapon");

        HP = serializedObject.FindProperty("HP");
        Atk = serializedObject.FindProperty("Atk");
        Spd = serializedObject.FindProperty("Spd");
        Def = serializedObject.FindProperty("Def");
        Res = serializedObject.FindProperty("Res");

        modifierHP = serializedObject.FindProperty("modifierHP");
        modifierAtk = serializedObject.FindProperty("modifierAtk");
        modifierSpd = serializedObject.FindProperty("modifierSpd");
        modifierDef = serializedObject.FindProperty("modifierDef");
        modifierRes = serializedObject.FindProperty("modifierRes");

        levelListHP = serializedObject.FindProperty("levelListHP");
        levelListAtk = serializedObject.FindProperty("levelListAtk");
        levelListSpd = serializedObject.FindProperty("levelListSpd");
        levelListDef = serializedObject.FindProperty("levelListDef");
        levelListRes = serializedObject.FindProperty("levelListRes");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Card card = target as Card;

        EditorGUILayout.PropertyField(ruleset);
        EditorGUILayout.PropertyField(summonable);

        if (card.summonable == true)
        {
            card.unlocked = EditorGUILayout.Toggle("Unlocked", card.unlocked);
        }
        
        EditorGUILayout.PropertyField(unitName);
        EditorGUILayout.PropertyField(race);
        EditorGUILayout.PropertyField(gender);
        EditorGUILayout.PropertyField(description);
        card.IV = EditorGUILayout.IntField("IV", card.IV);
        EditorGUILayout.PropertyField(moveClass);
        EditorGUILayout.PropertyField(damageType);
        EditorGUILayout.PropertyField(range);
        
        if (card.damageType == Card.DamageType.Physical && card.range == Card.Range.Melee && card.moveClass == Card.MoveClass.Armor)
        {
            EditorGUILayout.PropertyField(pma);
        }
        else if (card.damageType == Card.DamageType.Physical && card.range == Card.Range.Ranged && card.moveClass == Card.MoveClass.Armor)
        {
            EditorGUILayout.PropertyField(pra);
        }
        else if (card.damageType == Card.DamageType.Magical && card.range == Card.Range.Melee && card.moveClass == Card.MoveClass.Armor)
        {
            EditorGUILayout.PropertyField(mma);
        }
        else if (card.damageType == Card.DamageType.Magical && card.range == Card.Range.Ranged && card.moveClass == Card.MoveClass.Armor)
        {
            EditorGUILayout.PropertyField(mra);
        }
        else if (card.damageType == Card.DamageType.Physical && card.range == Card.Range.Melee && card.moveClass == Card.MoveClass.Cavalry)
        {
            EditorGUILayout.PropertyField(pmc);
        }
        else if (card.damageType == Card.DamageType.Physical && card.range == Card.Range.Ranged && card.moveClass == Card.MoveClass.Cavalry)
        {
            EditorGUILayout.PropertyField(prc);
        }
        else if (card.damageType == Card.DamageType.Magical && card.range == Card.Range.Melee && card.moveClass == Card.MoveClass.Cavalry)
        {
            EditorGUILayout.PropertyField(mmc);
        }
        else if (card.damageType == Card.DamageType.Magical && card.range == Card.Range.Ranged && card.moveClass == Card.MoveClass.Cavalry)
        {
            EditorGUILayout.PropertyField(mrc);
        }
        else if (card.damageType == Card.DamageType.Physical && card.range == Card.Range.Melee && card.moveClass == Card.MoveClass.Flier)
        {
            EditorGUILayout.PropertyField(pmf);
        }
        else if (card.damageType == Card.DamageType.Physical && card.range == Card.Range.Ranged && card.moveClass == Card.MoveClass.Flier)
        {
            EditorGUILayout.PropertyField(prf);
        }
        else if (card.damageType == Card.DamageType.Magical && card.range == Card.Range.Melee && card.moveClass == Card.MoveClass.Flier)
        {
            EditorGUILayout.PropertyField(mmf);
        }
        else if (card.damageType == Card.DamageType.Magical && card.range == Card.Range.Ranged && card.moveClass == Card.MoveClass.Flier)
        {
            EditorGUILayout.PropertyField(mrf);
        }
        else if (card.damageType == Card.DamageType.Physical && card.range == Card.Range.Melee && card.moveClass == Card.MoveClass.Infantry)
        {
            EditorGUILayout.PropertyField(pmi);
        }
        else if (card.damageType == Card.DamageType.Physical && card.range == Card.Range.Ranged && card.moveClass == Card.MoveClass.Infantry)
        {
            EditorGUILayout.PropertyField(pri);
        }
        else if (card.damageType == Card.DamageType.Magical && card.range == Card.Range.Melee && card.moveClass == Card.MoveClass.Infantry)
        {
            EditorGUILayout.PropertyField(mmi);
        }
        else if (card.damageType == Card.DamageType.Magical && card.range == Card.Range.Ranged && card.moveClass == Card.MoveClass.Infantry)
        {
            EditorGUILayout.PropertyField(mri);
        }

        EditorGUILayout.PropertyField(portrait);
        EditorGUILayout.PropertyField(mapSprite);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Unit's Current Power Level", EditorStyles.boldLabel);
        card.level = EditorGUILayout.IntField("Level", card.level);
        card.merge = EditorGUILayout.IntField("Merge", card.merge);
        EditorGUILayout.PropertyField(rarity);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Lvl. 1 Base Stats (Bronze)", EditorStyles.boldLabel);
        card.baseHP = EditorGUILayout.IntField("HP", card.baseHP);
        card.baseAtk = EditorGUILayout.IntField("Atk", card.baseAtk);
        card.baseSpd = EditorGUILayout.IntField("Spd", card.baseSpd);
        card.baseDef = EditorGUILayout.IntField("Def", card.baseDef);
        card.baseRes = EditorGUILayout.IntField("Res", card.baseRes);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Growth Rates (%)", EditorStyles.boldLabel);
        card.growthHP = EditorGUILayout.IntField("HP", card.growthHP);
        card.growthAtk = EditorGUILayout.IntField("Atk", card.growthAtk);
        card.growthSpd = EditorGUILayout.IntField("Spd", card.growthSpd);
        card.growthDef = EditorGUILayout.IntField("Def", card.growthDef);
        card.growthRes = EditorGUILayout.IntField("Res", card.growthRes);

        EditorGUILayout.PropertyField(weapon);
        EditorGUILayout.PropertyField(assist);
        EditorGUILayout.PropertyField(special);
        EditorGUILayout.PropertyField(slotA);
        EditorGUILayout.PropertyField(slotB);
        EditorGUILayout.PropertyField(slotC);
        EditorGUILayout.PropertyField(slotD);

        EditorGUILayout.PropertyField(HP, new GUIContent("HP"));
        EditorGUILayout.PropertyField(Atk, new GUIContent("Atk"));
        EditorGUILayout.PropertyField(Spd, new GUIContent("Spd"));
        EditorGUILayout.PropertyField(Def, new GUIContent("Def"));
        EditorGUILayout.PropertyField(Res, new GUIContent("Res"));
        EditorGUILayout.PropertyField(modifierHP, new GUIContent("HP modifier"));
        EditorGUILayout.PropertyField(modifierAtk, new GUIContent("Atk modifier"));
        EditorGUILayout.PropertyField(modifierSpd, new GUIContent("Spd modifier"));
        EditorGUILayout.PropertyField(modifierDef, new GUIContent("Def modifier"));
        EditorGUILayout.PropertyField(modifierRes, new GUIContent("Res modifier"));
        EditorGUILayout.PropertyField(levelListHP, true);
        EditorGUILayout.PropertyField(levelListAtk, true);
        EditorGUILayout.PropertyField(levelListSpd, true);
        EditorGUILayout.PropertyField(levelListDef, true);
        EditorGUILayout.PropertyField(levelListRes, true);

        card.levelingRange = EditorGUILayout.IntField("Leveling Range", card.levelingRange);

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
