using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameMechanism : MonoBehaviour
{
    [SerializeField] public int scoreCoins;
    [SerializeField] private BuyLevel _buyLevel;

    public GameObject shopPanel;
    public GameObject bonusPanel;

    private Save sv = new Save();

    public Text scoreText;

    public int clickScore=1;
    private int TotalScoreCoinsBonus;

    public AudioSource openClosePanelSource;
    public AudioSource clickCoinSource;

    public void SearchSavedKey()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));

            scoreCoins = sv.scoreCoins;
            clickScore = sv.clickScore;

            for (int i = 0; i < 1; i++)
            {
                _buyLevel.CostBonus[i] = sv.CostBonus[i];
                TotalScoreCoinsBonus += sv.CostBonus[i];
            }

            for (int i = 0; i < 2; i++)
            {
                _buyLevel.CostIntLevel[i] = sv.CostIntLevel[i];
                _buyLevel.CostText[i].text = sv.CostIntLevel[i] + " ";
            }

            TotalScoreBonus();
        }
    }

    private void TotalScoreBonus()
    {
        DateTime dateTime = new DateTime(sv.Date[0], sv.Date[1], sv.Date[2], sv.Date[3], sv.Date[4], sv.Date[5]);
        TimeSpan timeSpan = DateTime.Now - dateTime;

        scoreCoins += (int)timeSpan.TotalSeconds * TotalScoreCoinsBonus;
    } 

    void Update()
    {
        scoreText.text = scoreCoins +" ";
    }

    public void ShowAndHideShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        openClosePanelSource.Play();
    }
    public void ShowAndHideBonusPanel()
    {
        bonusPanel.SetActive(!bonusPanel.activeSelf);
        openClosePanelSource.Play();
    }

    public void OnclickButton()
    {
        scoreCoins += clickScore;
        clickCoinSource.Play();
    }

    public void SavingGame()
    {
        //OnApplicationQuit();
        OnApplicationPause(true);
    }

    private void OnApplicationPause(bool pause)
    {
        sv.scoreCoins = scoreCoins;
        sv.clickScore = clickScore;
        sv.CostBonus = new int[1];
        sv.CostIntLevel = new int[2];

        for (int i = 0; i < 1; i++)
        {
            sv.CostBonus[i] = _buyLevel.CostBonus[i];
        }

        for (int i = 0; i < 2; i++)
        {
            sv.CostIntLevel[i] = _buyLevel.CostIntLevel[i];
        }

        sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day;
        sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;

        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }

    //private void OnApplicationQuit()
    //{
    //    sv.scoreCoins = scoreCoins;
    //    sv.clickScore = clickScore;
    //    sv.CostBonus = new int[1];
    //    sv.CostIntLevel = new int[2];

    //    for (int i = 0; i < 1; i++)
    //    {
    //        sv.CostBonus[i] = _buyLevel.CostBonus[i];
    //    }

    //    for (int i = 0; i < 2; i++)
    //    {
    //        sv.CostIntLevel[i] = _buyLevel.CostIntLevel[i];
    //    }

    //    sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day;
    //    sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;

    //    PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    //}
}

[Serializable]
public class Save
{
    public int scoreCoins;
    public int clickScore;
    public int [] CostIntLevel;
    public int [] CostBonus;
    public int[] Date = new int[6];
}
