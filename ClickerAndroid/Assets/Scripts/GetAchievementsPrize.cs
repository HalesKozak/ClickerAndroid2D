using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAchievementsPrize : MonoBehaviour
{
    [SerializeField] private GameMechanism _gameMechanism;

    public int[] costPrize;
    public string[] taskText;

    public Text[] achivmentCostText;
    public Text[] achivmentTaskText;

    public void GetPrize(int i)
    {
        _gameMechanism.scoreCoins += costPrize[i];
        _gameMechanism.ValueAchievement(i);

        costPrize[i] += costPrize[i];
        achivmentCostText[i].text = costPrize[i] + "";
        achivmentTaskText[i].text = taskText[i] + costPrize[i] + taskText[i+1];

    }
}
