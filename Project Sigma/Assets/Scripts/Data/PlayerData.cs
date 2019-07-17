using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerLevel;
    public int premiumCurrencyCount;

    public PlayerData (PlayerProgress progress)
    {
        playerLevel = progress.playerLevel;
        premiumCurrencyCount = progress.premiumCurrencyCount;
    }
}
