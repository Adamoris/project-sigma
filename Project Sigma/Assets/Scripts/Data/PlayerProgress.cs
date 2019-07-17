using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerProgress : MonoBehaviour
{
    public int playerLevel = 1;
    public int premiumCurrencyCount;

    

    public void SaveProgress()
    {
        SaveSystem.SaveProgress(this);
    }

    public void LoadProgress()
    {
        PlayerData data = SaveSystem.LoadProgress();

        playerLevel = data.playerLevel;
        premiumCurrencyCount = data.premiumCurrencyCount;
    }

    #region testing

    public TextMeshProUGUI levelCounter;
    public TextMeshProUGUI premiumCurrencyCounter;

    public void Update()
    {
        levelCounter.text = "" + playerLevel;
        premiumCurrencyCounter.text = "" + premiumCurrencyCount;

    }

    public void ChangeLevel(int amount)
    {
        playerLevel += amount;
    }

    public void ChangeCurrency(int amount)
    {
        premiumCurrencyCount += amount;
    }

    #endregion

}
