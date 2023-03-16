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
        for (int i = 0; i < taskText.Length; i++)
        {
            achivmentCostText[i].text = costPrize[i] + "";
            achivmentTaskText[i].text = taskText[i] + costPrize[i] + taskText[i + 1];
        }
    }

    public void GetPrize(int i)
    {
        _gameMechanism.scoreCoins += costPrize[i];
        _gameMechanism.ValueAchievement(i);

        costPrize[i] += costPrize[i];

        achivmentCostText[i].text = costPrize[i] + "";
        achivmentTaskText[i].text = taskText[i] + costPrize[i] + taskText[i+1];

        getAchievementSource.Play();
    }
}
