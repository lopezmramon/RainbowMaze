using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameplayViewController : MonoBehaviour
{
    public Text healthText, objectivesText;

    private void Awake()
    {
        CodeControl.Message.AddListener<PlayerObjectiveCollectedAmountChange>(OnPlayerObjectiveCollectedAmountChanged);
        CodeControl.Message.AddListener<PlayerHealthChange>(OnPlayerHealthChanged);
    }

    private void OnPlayerHealthChanged(PlayerHealthChange obj)
    {
        healthText.text = string.Format("Health: {0}/{1}", obj.currentHealth, obj.totalHealth);
    }

    private void OnPlayerObjectiveCollectedAmountChanged(PlayerObjectiveCollectedAmountChange obj)
    {
        objectivesText.text = string.Format("Objectives Collected: {0}/{1}", obj.objectivesCollected, obj.totalObjectivesToCollect);
        if (obj.objectivesCollected == obj.totalObjectivesToCollect)
        {
            objectivesText.text = "All objectives collected! Go to the Green Light!";
        }
    }
}
