using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPrize : MonoBehaviour
{
    [SerializeField] private GameMechanism _gameMechanism;

    public int[] costPrize;
    public string[] taskText;

    public Text[] achivmentCostText;
    public Text[] achivmentTaskText;

    public AudioSource getAchievementSource;

    public void ReloadText()
    {
        for (int i = 0; i < costPrize.Length; i++)
        {
            achivmentCostText[i].text = costPrize[i] + "";
            achivmentTaskText[i].text = taskText[i+i+1] + _gameMechanism.CostMaxAchievement[i] + taskText[i+i + 2];
        }
    }

    public void GetPrize(int i)
    {
        _gameMechanism.scoreCoins += costPrize[i];
        _gameMechanism.ValueAchievement(i);

        costPrize[i] += costPrize[i];

        if (i == 1)
        {
            _gameMechanism.lastSpentScoreAchievement += _gameMechanism.scoreCoins;
        }

        ReloadText();

        getAchievementSource.Play();
    }
}