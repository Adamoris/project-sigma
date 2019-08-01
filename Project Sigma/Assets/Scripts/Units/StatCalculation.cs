using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class StatCalculation : MonoBehaviour
{
    private readonly float rarityFactor = 0.07f;
    private readonly int levelingRange = 39;
    int randomizer;
    int[] levelList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RarityAdjustment(int rarity)
    {

    }

    public int GetGrowthValue(int growthRate, int rarity)
    {
        //Debug.Log((int)Mathf.Floor(levelingRange * 0.01f * Mathf.Floor(growthRate * (1f - rarityFactor + (rarityFactor * rarity)))));
        return (int)Mathf.Floor(levelingRange * 0.01f * Mathf.Floor(growthRate * (1f - rarityFactor + (rarityFactor * rarity))));
    }

    public int[] GenerateLevelList(int growthValue)
    {
        levelList = new int[growthValue];
        for (int j = 0; j < growthValue; j++)
        {
            randomizer = UnityEngine.Random.Range(2, levelingRange + 2);
            
            while (levelList.Contains(randomizer))
            {
                randomizer = UnityEngine.Random.Range(2, levelingRange + 2);
            }
            levelList[j] = randomizer;
        }
        Array.Sort(levelList);
        return levelList;
    }



}
