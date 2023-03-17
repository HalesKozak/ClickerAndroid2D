using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyLevel : MonoBehaviour
{
    [SerializeField] private GameMechanism _gameMechanism;

    public int[] CostIntLevel;
    public int[] CostBonus;
    public Text [] CostText;

    public AudioSource noBuySource;
    public AudioSource buyCompleteSource;


    private void Start()
    {
        StartCoroutine(Bonus());
    }
    public void OnClickBuyLevelShop()
    {
        if (_gameMechanism.scoreCoins >= CostIntLevel[0])
        {
            _gameMechanism.currentCostAhievement[1] += CostIntLevel[0];
            _gameMechanism.scoreCoins -= CostIntLevel[0];

            _gameMechanism.clickScore *= 2;
            CostIntLevel[0] *= 2;
            CostText[0].text = CostIntLevel[0] + " ";

            buyCompleteSource.Play();
        }
        else noBuySource.Play();
    }
    public void OnClickBuyLevelBonus()
    {
        if (_gameMechanism.scoreCoins >= CostIntLevel[1])
        {
            _gameMechanism.currentCostAhievement[1] += CostIntLevel[1];

            _gameMechanism.scoreCoins -= CostIntLevel[1];

            CostIntLevel[1] *= 2;
            CostBonus[0] += 2*2;
            CostText[1].text = CostIntLevel[1] + " ";

            buyCompleteSource.Play();
        }
        else noBuySource.Play();
    }

    IEnumerator Bonus()
    {
        while (true)
        {
            _gameMechanism.scoreCoins += CostBonus[0];
            yield return new WaitForSeconds(1f);
        }
    }
}