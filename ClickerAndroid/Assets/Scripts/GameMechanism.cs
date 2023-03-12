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

    private SaveProgress saveProgress = new SaveProgress();

    public Text scoreText;

    public int clickScore=1;
    private int TotalScoreCoinsBonus;
    private int firstCostClick = 100;
    private int firstCostBonus = 500;

    public AudioSource openClosePanelSource;
    public AudioSource clickCoinSource;

    public void SearchSavedKey()
    {
        if (PlayerPrefs.HasKey("Progress"))
        {
            saveProgress = JsonUtility.FromJson<SaveProgress>(PlayerPrefs.GetString("Progress"));

            scoreCoins = saveProgress.scoreCoins;
            clickScore = saveProgress.clickScore;

            for (int i = 0; i < 1; i++)
            {
                _buyLevel.CostBonus[i] = saveProgress.CostBonus[i];
                TotalScoreCoinsBonus += saveProgress.CostBonus[i];
            }

            for (int i = 0; i < 2; i++)
            {
                _buyLevel.CostIntLevel[i] = saveProgress.CostIntLevel[i];
                _buyLevel.CostText[i].text = saveProgress.CostIntLevel[i] + " ";
            }

            TotalScoreBonus();
        }
    }

    private void TotalScoreBonus()
    {
        DateTime dateTime = new DateTime(saveProgress.Date[0], saveProgress.Date[1], saveProgress.Date[2], saveProgress.Date[3], saveProgress.Date[4], saveProgress.Date[5]);
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

    public void SavingProgressGame()
    {
        saveProgress.scoreCoins = scoreCoins;
        saveProgress.clickScore = clickScore;
        saveProgress.CostBonus = new int[1];
        saveProgress.CostIntLevel = new int[2];

        for (int i = 0; i < 1; i++)
        {
            saveProgress.CostBonus[i] = _buyLevel.CostBonus[i];
        }

        for (int i = 0; i < 2; i++)
        {
            saveProgress.CostIntLevel[i] = _buyLevel.CostIntLevel[i];
        }

        saveProgress.Date[0] = DateTime.Now.Year; saveProgress.Date[1] = DateTime.Now.Month; saveProgress.Date[2] = DateTime.Now.Day;
        saveProgress.Date[3] = DateTime.Now.Hour; saveProgress.Date[4] = DateTime.Now.Minute; saveProgress.Date[5] = DateTime.Now.Second;

        PlayerPrefs.SetString("Progress", JsonUtility.ToJson(saveProgress));
    }

    public void ResetResult()
    {
        scoreCoins = 0;
        clickScore = 1;

        for (int i = 0; i < 1; i++)
        {
            _buyLevel.CostBonus[i] = 0;
            TotalScoreCoinsBonus += saveProgress.CostBonus[i];
        }

        _buyLevel.CostIntLevel[0] = firstCostClick;
        _buyLevel.CostIntLevel[1] = firstCostBonus;
        for (int i = 0; i < 2; i++)
        {
            _buyLevel.CostText[i].text = _buyLevel.CostIntLevel[i] + " ";
        }

        PlayerPrefs.DeleteKey("Progress");

        openClosePanelSource.Play();
    }

    private void OnApplicationPause(bool pause)
    {
        SavingProgressGame();
    }

    private void OnApplicationQuit()
    {
        SavingProgressGame();
    }
}

[Serializable]
public class SaveProgress
{
    public int scoreCoins;
    public int clickScore;
    public int [] CostIntLevel;
    public int [] CostBonus;
    public int[] Date = new int[6];
}
