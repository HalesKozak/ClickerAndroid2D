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

    public Text scoreText;

    private SaveProgress saveProgress = new SaveProgress();

    public int clickScore=1;
    private int firstCostClick = 100;
    private int firstCostBonus = 500;

    public int lastSpentScoreAchievement;

    [Header("Досягнення")]
    public int[] CostMaxAchievement;
    public int[] currentCostAhievement = { 0, 0, 0 };

    public Slider[] achievementSlider;

    public GameObject[] achievementImage;

    public AudioSource openClosePanelSource;
    public AudioSource clickCoinSource;

    private void FixedUpdate()
    {
        scoreText.text = scoreCoins + " ";

        if(achievementImage[2].activeSelf == false)
        {
            currentCostAhievement[2] = scoreCoins;
        }

        for (int i = 0; i < currentCostAhievement.Length; i++)
        {
            if (currentCostAhievement[i] >= CostMaxAchievement[i])
            {
                achievementImage[i].SetActive(true);
            }
            achievementSlider[i].value = currentCostAhievement[i];
        }
    }

    public void SearchSavedKey()
    {
        if (PlayerPrefs.HasKey("Progress"))
        {
            saveProgress = JsonUtility.FromJson<SaveProgress>(PlayerPrefs.GetString("Progress"));

            scoreCoins = saveProgress.scoreCoinsSave;
            clickScore = saveProgress.clickScoreSave;

            lastSpentScoreAchievement = saveProgress.lastSpentScoreAchievementSave;

            for (int i = 0; i < _buyLevel.CostBonus.Length; i++)
            {
                _buyLevel.CostBonus[i] = saveProgress.CostBonusSave[i];
            }

            for (int i = 0; i < _buyLevel.CostIntLevel.Length; i++)
            {
                _buyLevel.CostIntLevel[i] = saveProgress.CostIntLevelSave[i];
                _buyLevel.CostText[i].text = saveProgress.CostIntLevelSave[i] + " ";
            }

            for (int i = 0; i < CostMaxAchievement.Length; i++)
            {
                CostMaxAchievement[i] = saveProgress.CostMaxAchievementSave[i];
                currentCostAhievement[i] = saveProgress.currentCostAhievementSave[i];
            }
        }
        LoadingAchievement();
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

        if (currentCostAhievement[0] < CostMaxAchievement[0])
        {
            currentCostAhievement[0]++;
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

        saveProgress.lastSpentScoreAchievementSave = lastSpentScoreAchievement;

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

        PlayerPrefs.SetString("Progress", JsonUtility.ToJson(saveProgress));
    }

    public void ResetResult()
    {
        scoreCoins = 0;
        clickScore = 1;
        lastSpentScoreAchievement = 0;

        CostMaxAchievement[0] = 100;
        CostMaxAchievement[1] = 1000;
        CostMaxAchievement[2] = 700;

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

        for (int i = 0; i < _achievementsPrize.costPrize.Length; i=i+2)
        {
            _achievementsPrize.costPrize[i] = 100;
        }
       
        _achievementsPrize.ReloadText();

        for (int i = 0; i < CostMaxAchievement.Length; i++)
        {
            achievementImage[i].SetActive(false);
        }

        PlayerPrefs.DeleteKey("Progress");

        LoadingAchievement();

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
}

[Serializable]
public class SaveProgress
{
    public int scoreCoinsSave;
    public int clickScoreSave;
    public int lastSpentScoreAchievementSave;

    public int [] CostIntLevelSave;
    public int [] CostBonusSave;

    public int[] CostMaxAchievementSave;
    public int[] currentCostAhievementSave;
}