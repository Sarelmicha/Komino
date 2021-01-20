using AppState.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignBattleHUDManager : HUDManager
{

    [SerializeField] private Image enemyImage = null;
    // Game Section
    [SerializeField] private Text playerScoreText = null;
    [SerializeField] private Text enemyScoreText = null;

    //Battle Section
    [SerializeField] private Text playerAttackChoiceText = null;
    [SerializeField] private Text playerLootChoiceText = null;
    [SerializeField] private Text playerAbilityText = null;
    [SerializeField] private Text playerAbilityDescription = null;
    [SerializeField] private Text playerCritText = null;

    [SerializeField] private Text currentLootText = null;
    [SerializeField] private Text maxLootText = null;

    [SerializeField] private GameObject playerCriticalAttackChanceSection = null;
    [SerializeField] private GameObject playerCriticalLootChanceSection = null;
    [SerializeField] private GameObject automaticButtonsHolder = null;


    [SerializeField] private float fastDelayBetweenIncrement = 0.0005f;
    [SerializeField] private float slowDelayBetweenIncrement = 0.05f;

    //----------------TO BE DELETE -------------------
    [SerializeField] private Text playerId = null;
    [SerializeField] private PlayerState playerState = null;
    //----------------------------------------------------------

    // Buttons 
    //[SerializeField] private Button playAgainButton = null;
    //[SerializeField] private Button homeButton = null;
    [SerializeField] private Button attackButton = null;
    [SerializeField] private Button lootButton = null;
    [SerializeField] private Button abilityButton = null;
    [SerializeField] private Button critButton = null;
    [SerializeField] private Button saveButton = null;


    [SerializeField] private InteractiveBar scorebar = null;

    private Value playerScore = null;
    private Value enemyScore = null;
    private Value currentLoot = null;


    private void Awake()
    {
        playerScore = new Value(0);
        enemyScore = new Value(0);
        currentLoot = new Value(0);

        // to be delete ----------------
        playerId.text = playerState.Id;
        //------------------------------
     
    }

    private void Start()
    {
        if (scorebar != null)
        {
            scorebar.SetValue(scorebar.GetMaxValue() / 2);
        }
    }

    public void UpdatePlayerAttackChoiceText(Value attack)
    {
        print("attack choice is " + attack.GetValue());
        this.playerAttackChoiceText.text = abbrevationUtility.AbbreviateNumber(attack.GetValue());
    }

    public void UpdatePlayerCritChoiceText(Value attack)
    {
        this.playerCritText.text = abbrevationUtility.AbbreviateNumber(attack.GetValue()) + "%";
    }

    public void UpdateMaxLootText(Value maxLoot)
    {
        this.maxLootText.text = "/ " + abbrevationUtility.AbbreviateNumber(maxLoot.GetValue());
    }
    public void UpdatePlayerCriticalAttackChanceText(Value criticalAttackChance)
    {
        this.playerCriticalAttackChanceSection.GetComponentInChildren<Text>().text = abbrevationUtility.AbbreviateNumber(criticalAttackChance.GetValue()) + "%";
    }

    public void UpdatePlayerLootChoiceText(Value loot)
    {
        print("loot choice is " + loot.GetValue());

        this.playerLootChoiceText.text = abbrevationUtility.AbbreviateNumber(loot.GetValue());
    }


    public void UpdatePlayerAbilityText(Value ability)
    {
        this.playerAbilityText.text = abbrevationUtility.AbbreviateNumber(ability.GetValue());
    }

    public void UpdatePlayerAbilityDescriptionText(string playerAbilityDescription)
    {
        this.playerAbilityDescription.text = playerAbilityDescription;
    }

    // Called by listener
    public void UpdateEnemyScoreText(Value targetScore)
    {

        int oldScore = enemyScore.GetValue();

        if (targetScore.SetInstantly())
        {

            SetTextInstantly(enemyScoreText, enemyScore, targetScore.GetValue(), "");
            return;
        }

        StartCoroutine(IncrementToTarget(enemyScoreText, enemyScore, targetScore.GetValue(), "", fastDelayBetweenIncrement, 1));

        if (!scorebar.IsRestarted())
        {
            scorebar.Reduce(targetScore.GetValue() - oldScore);
        }
    }


    // Called by listener
    public void UpdatePlayerScoreText(Value targetScore)
    {

        int oldScore = playerScore.GetValue();

        if (targetScore.SetInstantly())
        {

            SetTextInstantly(playerScoreText, playerScore, targetScore.GetValue(), "");
            scorebar.SetValue(scorebar.GetMaxValue() / 2);

            return;
        }

        StartCoroutine(IncrementToTarget(playerScoreText, playerScore, targetScore.GetValue(), "", fastDelayBetweenIncrement, 1));

        if (!scorebar.IsRestarted())
        {
            scorebar.Gain(targetScore.GetValue() - oldScore);
        }

    }


    // Called by listener
    public void UpdateCurrentLootText(Value targetScore)
    {

        if (targetScore.SetInstantly())
        {
            SetTextInstantly(currentLootText, currentLoot, targetScore.GetValue(), "");
         
            return;
        }
        StartCoroutine(IncrementToTarget(currentLootText, currentLoot, targetScore.GetValue(), "", fastDelayBetweenIncrement, 1));

     
    }


    public void SetUserChoicesButtonsActive(bool active,bool isAbility,bool isCrit, bool isSave)
    {
        SetAttackButtonActive(active);
        SetLootButtonActive(active);
       
        if (isAbility)
        {
            SetAbilityButtonActive(active);            
        }
        if (isCrit)
        {
            SetCritButtonActive(active);
        }

        if (isSave)
        {
            SetSaveButtonActive(active);
        }
    }


    public void SetPlayerCriticalAttackChanceSectionActive(bool active)
    {
        this.playerCriticalAttackChanceSection.SetActive(active);
    }

    public void SetPlayerCriticalLootChanceSectionActive(bool active)
    {
        this.playerCriticalLootChanceSection.SetActive(active);
    }


    public void SetAbilityButtonActive(bool active)
    {
        abilityButton.gameObject.SetActive(active);
    }

    public void SetCritButtonActive(bool active)
    {
        critButton.gameObject.SetActive(active);
    }

    public void SetSaveButtonActive(bool active)
    {
        saveButton.gameObject.SetActive(active);
    }



    public void SetAttackButtonActive(bool active)
    {
        attackButton.gameObject.SetActive(active);
    }

    public void OnAutoPlayButtonClicked(Button pickedButton)
    {
        print("inside OnAutoPlayButtonClicked");

        foreach (Transform child in automaticButtonsHolder.transform)
        {
            Button button = child.GetComponent<Button>();

            button.interactable = true;

            if (button == pickedButton)
            {
                button.interactable = false;
            }

        }
    }



    public void SetLootButtonActive(bool active)
    {
        lootButton.gameObject.SetActive(active);
    }

    public void SetPlayerHUDActive(bool active)
    {
        scorebar.gameObject.SetActive(active);
        enemyImage.gameObject.SetActive(active);
    }
}
