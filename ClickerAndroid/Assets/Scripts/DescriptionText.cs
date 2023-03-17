using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionText : MonoBehaviour
{
    [SerializeField] private BuyLevel _buyLevel;
    [SerializeField] private GameMechanism _gameMechanism;

    public Text[] descriptionText;

    private void FixedUpdate()
    {
        descriptionText[0].text = _buyLevel.CostBonus[0] + "/ρεκ";

        descriptionText[1].text = _gameMechanism.clickScore+"/κλ³κ";
    }
}
