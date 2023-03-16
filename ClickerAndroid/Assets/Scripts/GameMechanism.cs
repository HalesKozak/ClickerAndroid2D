using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameMechanism : MonoBehaviour
{
    [SerializeField] public int scoreCoins;

    [SerializeField] private BuyLevel _buyLevel;
    [SerializeField] private AchievementsPrize _achievementsPrize;

    public GameObject shopPanel;
    public GameObject bonusPanel;
    public GameObject achievementPanel;

    private SaveProgress saveProgress = new SaveProgress();

    public Text scoreText;

    public int clickScore=1;
    private int TotalScoreCoinsBonus;
    private int firstCostClick = 100;
    private int firstCostBonus = 500;

    [Header("Досягнення")]
    public int[] CostMaxAchievement;
    public int[] currentCostAhievement = { 0, 0, 0 };

    public Slider[] achievementSlider;

    public GameObject[] achievementImage;

    public AudioSource openClosePanelSource;
    public AudioSource clickCoinSource;

    private void Update()
    {
        scoreText.text = scoreCoins + " ";
    }

    public void SearchSavedKey()
    {
        if (PlayerPrefs.HasKey("Progress"))
        {
            saveProgress = JsonUtility.FromJson<SaveProgress>(PlayerPrefs.GetString("Progress"));

            scoreCoins = saveProgress.scoreCoinsSave;
            clickScore = saveProgress.clickScoreSave;

            for (int i = 0; i < 1; i++)
            {
                _buyLevel.CostBonus[i] = saveProgress.CostBonusSave[i];
                TotalScoreCoinsBonus += saveProgress.CostBonusSave[i];
            }

            for (int i = 0; i < 2; i++)
            {
                _buyLevel.CostIntLevel[i] = saveProgress.CostIntLevelSave[i];
                _buyLevel.CostText[i].text = saveProgress.CostIntLevelSave[i] + " ";
            }

            for (int i = 0; i < 3; i++)
            {
                CostMaxAchievement[i] = saveProgress.CostMaxAchievementSave[i];
                currentCostAhievement[i] = saveProgress.currentCostAhievementSave[i];
            }

            LoadingAchievement();

            TotalScoreBonus();
        }
    }

    private void TotalScoreBonus()
    {
        DateTime dateTime = new DateTime(saveProgress.DateSave[0], saveProgress.DateSave[1], saveProgress.DateSave[2], saveProgress.DateSave[3], saveProgress.DateSave[4], saveProgress.DateSave[5]);
        TimeSpan timeSpan = DateTime.Now - dateTime;

        scoreCoins += (int)timeSpan.TotalSeconds * TotalScoreCoinsBonus;
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

    public void ShowAndHideAchivmentPanel()
    {
        achievementPanel.SetActive(!achievementPanel.activeSelf);
        openClosePanelSource.Play();
    }

    public void OnclickButton()
    {
        scoreCoins += clickScore;

        for (int i = 0; i < CostMaxAchievement.Length; i++)
        {
            if (currentCostAhievement[i] < CostMaxAchievement[i])
            {
                currentCostAhievement[i]++;
                achievementSlider[i].value = currentCostAhievement[i];
            }
            if (currentCostAhievement[i] == CostMaxAchievement[i])
            {
                achievementImage[i].SetActive(true);
            }
        }
        
        clickCoinSource.Play();
    }

    public void ValueAchievement(int i)
    {
        achievementImage[i].SetActive(false);

        achievementSlider[i].maxValue += achievementSlider[i].maxValue;
        achievementSlider[i].value = 0;

        currentCostAhievement[i] = 0;
        CostMaxAchievement[i] += CostMaxAchievement[i];
    }

    public void SavingProgressGame()
    {
        saveProgress.scoreCoinsSave = scoreCoins;
        saveProgress.clickScoreSave = clickScore;

        saveProgress.CostBonusSave = new int[1];
        saveProgress.CostIntLevelSave = new int[2];

        saveProgress.CostMaxAchievementSave = new int[3];
        saveProgress.currentCostAhievementSave = new int[3];

        for (int i = 0; i < 1; i++)
        {
            saveProgress.CostBonusSave[i] = _buyLevel.CostBonus[i];
        }

        for (int i = 0; i < 2; i++)
        {
            saveProgress.CostIntLevelSave[i] = _buyLevel.CostIntLevel[i];
        }

        for (int i = 0; i < 3; i++)
        {
            saveProgress.CostMaxAchievementSave[i] = CostMaxAchievement[i];
            saveProgress.currentCostAhievementSave[i] = currentCostAhievement[i];
        }

        saveProgress.DateSave[0] = DateTime.Now.Year; saveProgress.DateSave[1] = DateTime.Now.Month; saveProgress.DateSave[2] = DateTime.Now.Day;
        saveProgress.DateSave[3] = DateTime.Now.Hour; saveProgress.DateSave[4] = DateTime.Now.Minute; saveProgress.DateSave[5] = DateTime.Now.Second;

        PlayerPrefs.SetString("Progress", JsonUtility.ToJson(saveProgress));
    }

    public void ResetResult()
    {
        scoreCoins = 0;
        clickScore = 1;
        CostMaxAchievement[0] = 100;

        for (int i = 0; i < CostMaxAchievement.Length; i++)
        {
            currentCostAhievement[i] = 0;
        }

        _buyLevel.CostBonus[0] = 0;
        _buyLevel.CostIntLevel[0] = firstCostClick;
        _buyLevel.CostIntLevel[1] = firstCostBonus;

        for (int i = 0; i < _buyLevel.CostIntLevel.Length; i++)
        {
            _buyLevel.CostText[i].text = _buyLevel.CostIntLevel[i] + " ";
        }

        _achievementsPrize.costPrize[0] = 100;
        _achievementsPrize.ReloadText();

        PlayerPrefs.DeleteKey("Progress");

        openClosePanelSource.Play();
    }

    private void LoadingAchievement()
    {
        for (int i = 0; i < CostMaxAchievement.Length; i++)
        {
            achievementSlider[i].maxValue = CostMaxAchievement[i];
            achievementSlider[i].value = currentCostAhievement[i];

            if (currentCostAhievement[i] == CostMaxAchievement[i])
            {
                achievementImage[i].SetActive(true);
            }
        }

        _achievementsPrize.ReloadText();
    }

    //private void OnApplicationQuit()
    //{
    //    saveProgress.scoreCoinsSave = scoreCoinsSave;
    //    saveProgress.clickScoreSave = clickScoreSave;
    //    saveProgress.CostBonusSave = new int[1];
    //    saveProgress.CostIntLevelSave = new int[2];

    //    for (int i = 0; i < 1; i++)
    //    {
    //        saveProgress.CostBonusSave[i] = _buyLevel.CostBonusSave[i];
    //    }

    //    for (int i = 0; i < 2; i++)
    //    {
    //        saveProgress.CostIntLevelSave[i] = _buyLevel.CostIntLevelSave[i];
    //    }

    //    saveProgress.DateSave[0] = DateTime.Now.Year; saveProgress.DateSave[1] = DateTime.Now.Month; saveProgress.DateSave[2] = DateTime.Now.Day;
    //    saveProgress.DateSave[3] = DateTime.Now.Hour; saveProgress.DateSave[4] = DateTime.Now.Minute; saveProgress.DateSave[5] = DateTime.Now.Second;

    //    PlayerPrefs.SetString("Progress", JsonUtility.ToJson(saveProgress));
    //}
}

[Serializable]
public class SaveProgress
{
    public int scoreCoinsSave;
    public int clickScoreSave;
    public int [] CostIntLevelSave;
    public int [] CostBonusSave;
    public int[] DateSave = new int[6];

    public int[] CostMaxAchievementSave;
    public int[] currentCostAhievementSave;
}
