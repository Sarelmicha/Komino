using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppState.Player;
using BestHTTP;
using Komino.CampaignBattle.Cards;
using Komino.CampaignBattle.Cards.BattleCards;
using Komino.CampaignBattle.Stats;
using Komino.Core;
using Komino.Enums;
using Komino.GameEvents.Events;
using ServerResponses.Player;
using System.Net;
using Komino.UI;
using System.Threading.Tasks;
using System;
using AppState.Game;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Komino.CampaignBattle
{
    public class BattleManager : MonoBehaviour
    {
        // Transforms
        [SerializeField] private Transform playerImage = null;
        [SerializeField] private Transform botImage = null;

        // Events
        [SerializeField] private StringEvent OnFightClicked = null;
        [SerializeField] private StringEvent OnFightEnded = null;
        [SerializeField] private StringEvent onDefuseAbilityOccured = null;

        [SerializeField] private VoidEvent onBattleStarted = null;
        [SerializeField] private VoidEvent onReachedMaxCards = null;

        [SerializeField] private VoidEvent onPlayerWins = null;
        [SerializeField] private VoidEvent onEnemyWins = null;
        [SerializeField] private VoidEvent onNoLootRobbed = null;


        //Stats
        [SerializeField] private CampaignBattleStats campaignBattleStats = null;


        [SerializeField] private GameState gameState = null;

        //Delays  
        [SerializeField] private float delayTimeBetweenEachSummonedCardsInSeconds = 0.4f;

        // Errors Handlers
        [SerializeField] private HttpErrorHandler httpErrorHandler = null;
        [SerializeField] private GameErrorHandler gameErrorHandler = null;

        //Storages
        [SerializeField] private ParticlesStorage particlesStorage = null;

        // Managers     
        [SerializeField] private CampaignBattleHUDManager campaignBattleHUDManager = null;
        [SerializeField] private PlayerTurnManager playerTurnManager = null;
        [SerializeField] private BotTurnManager botTurnManager = null;
        [SerializeField] private LootManager lootManager = null;
        [SerializeField] private AlertManager alertManager = null; 
        [SerializeField] private ChoiceCardManager choiceCardManager = null;
        [SerializeField] private ComboManager comboManager = null;
        [SerializeField] private TestManager testManager = null;
        [SerializeField] private PrizesManager prizesManager = null;


        private PlayerStateManager playerStateManager = null;


        //TO BE DELETE
        [SerializeField] Button manualButton = null;
        [SerializeField] Button semiButton = null;
        


        //UI
        [SerializeField] private GameObject spinner = null;

        // APIs
        private IBattleAPI battleAPI = null;
        private PlayerAPI playerAPI = null;


        // Helpers
        private BattleCardHelper battleCardHelper = null;
        private Loader loader = null;

        //Tests compenent
        private bool summonNextBotCard = false;
        private bool summonNextPlayerCard = false;
        private bool isReachedMaxCards = false;
        private bool finishedAnnounced = false;
        private bool testerChooseCard = false;
        private bool isPlayerWin = false;
        private bool isAutoBattleOn = false;

        private int currentTurn = 0;

        private TurnRoot turnRoot = null;
        private Turn playerTurn = null;
        private Turn playerLastTurn = null;
        private Turn enemyTurn = null;
        private Turn enemyLastTurn = null;
        private AllTurns battleResults = null;
        private CampaignBattleCard lastPlayerCard = null;
        private string playerLastChoice = null;


        [SerializeField] PlayerState playerState;


        private void Awake()
        {
            battleCardHelper = new BattleCardHelper();
            playerAPI = new PlayerAPI();
            loader = GameObject.FindGameObjectWithTag("loader").GetComponent<Loader>();
            playerStateManager = GameObject.FindGameObjectWithTag("playerStateManager").GetComponent<PlayerStateManager>();
            OnManualButtonClicked(manualButton);
            
        }
        public void SetBattleAPI(IBattleAPI battleAPI)
        {
            this.battleAPI = battleAPI;
        }

        public void SetActiveBattle(AllTurns allTurns)
        {
            print("root is " + allTurns);
            //campaignBattleHUDManager.SetHomeButtonsActive(false);
            campaignBattleHUDManager.IsRestarted(false);

            //Reset the gameState before set its fields.
            campaignBattleStats.ResetAllStats();

            // Set first cards
            currentTurn = allTurns.player_turns.Count - 1;
            print("currentTurn in active battle is " + currentTurn);
            playerTurn = allTurns.player_turns[allTurns.player_turns.Count - 1];
            campaignBattleStats.SetPlayerBattleCards(battleCardHelper.CreateBattleCards(allTurns.player_turns.GetRange(0, allTurns.player_turns.Count - 1)));
            campaignBattleStats.SetMaxTurns(allTurns.max_turns);
            playerTurnManager.SetCards(allTurns.player_turns.GetRange(0, allTurns.player_turns.Count - 1));
            SummonChoiceCard(allTurns.player_turns[allTurns.player_turns.Count - 1].card);

            if (allTurns.bot_turns.Count > 1)
            {
                enemyTurn = allTurns.bot_turns[allTurns.bot_turns.Count - 1];
                print("count is " + (allTurns.bot_turns.Count));
                campaignBattleStats.SetEnemyBattleCards(battleCardHelper.CreateBattleCards(allTurns.bot_turns.GetRange(0, allTurns.bot_turns.Count - 1)));
                botTurnManager.SetCards(allTurns.bot_turns.GetRange(0, allTurns.bot_turns.Count - 1));
            }

            //Set the lucks bar data
            campaignBattleStats.SetLootBarsData(allTurns.loot_bars);
            campaignBattleStats.SetCurrentMaxLootDataIndex(allTurns.loot_bars, playerTurn.all_turns_loot);

            //Set the luck bar 
            lootManager.SetLootbarData(allTurns);
            lootManager.SetMaxLoot((float)allTurns.loot_bars[campaignBattleStats.GetLootBarsData().Count - 1].min);
            lootManager.SetCheckpoints(allTurns.loot_bars);
            lootManager.SetReachedTargeStars(playerTurn.all_turns_loot, allTurns.loot_bars);
            campaignBattleStats.SetMaxLoot((float)allTurns.loot_bars[campaignBattleStats.GetLootBarsData().Count - 1].min, true);
            campaignBattleStats.SetCurrentLoot((float)playerTurn.all_turns_loot, true);

            SetPlayerChoices();
            SetBattleStats(allTurns);
            ShowChoiceButtons();

        }


        public void GetFirstBattleCard(BattleRoot firstTurn)
        {
            this.playerTurn = firstTurn.new_turn;
            //this.enemyTurn = firstTurn.bot_turn;

            campaignBattleStats.ResetAllStats();
            //campaignBattleHUDManager.SetHomeButtonsActive(false);

            campaignBattleHUDManager.IsRestarted(false);
            campaignBattleStats.SetLootBarsData(firstTurn.loot_bars);
            lootManager.SetMaxLoot((float)campaignBattleStats.GetLootBarsData()[campaignBattleStats.GetLootBarsData().Count - 1].min);
            lootManager.SetCheckpoints(firstTurn.loot_bars);
            campaignBattleStats.SetMaxLoot((float)campaignBattleStats.GetLootBarsData()[campaignBattleStats.GetLootBarsData().Count - 1].min, true);
            print("lets start playing...");
            SummonFirstChoice(playerTurn);
        }

        private void SummonChoiceCard(CardData cardData)
        {
            SetPlayerChoices();
            choiceCardManager.SummonChoiceCard(cardData);
            print("currentTurn in SummonChoiceCard is before" + currentTurn);
            currentTurn++;
            print("currentTurn in SummonChoiceCard is after" + currentTurn);

            if (((playerLastTurn == null || (battleCardHelper.GetCardComboAmount(playerLastTurn.combos, cardData) + 1) ==  1) && playerTurn.choices.crit == 0) && isAutoBattleOn)
            {
                print("inside auto choice");
               MakeAutoChoice();
            }
           
        }

     
        private void MakeAutoChoice()
        {         
            Debug.Log("inside MakeAutoChoice");

            if (playerTurn.choices.damage >= playerTurn.choices.loot)
            {
                OnDamageButtonClicked();
                return;
            }

            OnLootButtonClicked();
        }

        //Set battle stats of an open battle
        private void SetBattleStats(AllTurns allTurns)
        {
            SetPlayerScore((float)allTurns.player_turns[allTurns.player_turns.Count - 1].all_turns_damage, true);
            if (allTurns.bot_turns.Count > 0)
            {
                SetEnemyBattleStats(allTurns.bot_turns[allTurns.bot_turns.Count - 1], true);
            }
        }

        private void SetPlayerScore(double score, bool setInstantly)
        {
            campaignBattleStats.SetPlayerScore(score, setInstantly);

        }

        private void SetEnemyBattleStats(Turn root, bool setInstantly)
        {
            if (root == null)
            {
                return;
            }

            campaignBattleStats.SetEnemyScore((int)root.all_turns_damage, setInstantly);
        }

        private void SummonFirstChoice(Turn firstTurn)
        {
            //Restart luckbar from earlier rounds.
            print("onBattleStarted event has been raised!");
            onBattleStarted.Raise();
            //Set to player turn

            print("firstTurn.damage  = " + firstTurn.damage);
            //playerLastTurn = firstTurn;
            SummonChoiceCard(firstTurn.card);

        }

        public void SetPlayerChoices()
        {
            campaignBattleStats.SetAttackPerTurn(playerTurn.choices.damage, true);
            campaignBattleStats.SetAttackChancePerTurn(playerTurn.crit_chance, true);
            campaignBattleStats.SetLootPerTurn(playerTurn.choices.loot, true);
            campaignBattleStats.SetCritPerTurn(playerTurn.choices.crit, true);
            campaignBattleStats.SetAbilityPerTurn(playerTurn.choices.ability, true);

            if (playerTurn.crit_chance != 0)
            {
                print("im here and crit chance is " + playerTurn.crit_chance);
                campaignBattleHUDManager.SetPlayerCriticalAttackChanceSectionActive(true);
                return;
            }
            campaignBattleHUDManager.SetPlayerCriticalAttackChanceSectionActive(false);

        }



        public IEnumerator OnPlayerMakeChoice(string choice)
        {
            HideChoiceButtons();
            choiceCardManager.DestoryLastCampaignChoiceCard();
            //if (GameMode.isInTestMode)
            //{
            //    testManager.SetCardOptionPickerActive(true);
            //    yield return new WaitUntil(() => testerChooseCard == true);
            //    //Reset testerChooseCard for next round
            //    testerChooseCard = false;
            //}

            GetNextBattleCards(choice);
            yield return new WaitUntil(() => summonNextPlayerCard == true);
            //Reset summonNextPlayerCard for next round
            summonNextPlayerCard = false;
            playerTurnManager.HandleOneTurn(playerLastTurn, choice, campaignBattleStats.GetPlayerBattleCards());
            playerLastChoice = choice;
        }

        private bool HandleChoice(string choice)
        {
            if (choice == ChoicesConstants.DAMAGE)
            {
                // Add to score
                SetPlayerScore(playerLastTurn.all_turns_damage, false);

                if (lastPlayerCard == null)
                {
                    return false;
                }
                if (comboManager.HandleCombo(lastPlayerCard.GetAmount(), PlayerType.Player, playerLastTurn.base_damage))
                {
                    return true;
                }
                return false;
            }


            if (choice == ChoicesConstants.LOOT)
            {
                // Add to luck           
                campaignBattleStats.SetCurrentLoot(campaignBattleStats.GetCurrentLoot() + playerLastTurn.loot, false);
                StartCoroutine(lootManager.GainLoot((int)playerLastTurn.all_turns_loot, (int)campaignBattleStats.GetCurrentLootTarget().min, campaignBattleStats.GetLootBarsData()));
                return false;
            }

            if (choice == ChoicesConstants.CRIT)
            {
                //Invoke crit 
                print("damage is " + playerLastTurn.damage);
                print("luck is " + playerLastTurn.loot);
                //campaignBattleStats.SetCritPerTurn(playerLastTurn.crit_damage, true);
                if (comboManager.HandleCriticalHit(PlayerType.Player, (int)playerLastTurn.damage))
                {
                    SetPlayerScore(playerLastTurn.all_turns_damage, false);
                    return true;
                }

                else if (comboManager.HandleCriticalHit(PlayerType.Player, (int)playerLastTurn.loot))
                {
                    StartCoroutine(lootManager.GainLoot((int)playerLastTurn.all_turns_loot, (int)campaignBattleStats.GetCurrentLootTarget().min, campaignBattleStats.GetLootBarsData()));
                    return true;
                }

                return false;
            }

            if (choice == ChoicesConstants.ABILITY)
            {
                //Invoke Ability
                return false;
            }

            return false;
        }

        // Called when click on Button from UI
        public void OnPlayAgainClicked()
        {
            spinner.SetActive(true);
            print("start have been clicked!");
            loader.LoadNewBattleScene();

        }
        // Called when click on Button from UI
        public async void OnHomeButtonClicked()
        {
            spinner.SetActive(true);
            PlayerCurrenciesData playerCurrenciesData = await playerAPI.GetCurrencies();
            playerStateManager.SetCoins(new Value((int)playerCurrenciesData.coins,true));
            playerStateManager.SetGems(new Value((int)playerCurrenciesData.gems,true));
            loader.LoadHomeScene();
        }

        public void ShowChoiceButtons()
        {
            if (currentTurn == campaignBattleStats.GetMaxTurns())
            {
                return;
            }

            campaignBattleHUDManager.SetUserChoicesButtonsActive(true, playerTurn.choices.ability == null ? false : true,
                playerTurn.choices.crit == 0 ? false : true, playerTurn.choices.save);

        }

        public void HideChoiceButtons()
        {
            campaignBattleHUDManager.SetUserChoicesButtonsActive(false, true, true, true);
        }

        public void GetNextBattleCards(string choice)
        {
            try
            {
                if (currentTurn == campaignBattleStats.GetMaxTurns())
                {
                    OnPlayerReachedMaxTurns(choice);
                    return;
                }

                //Player has not reached max cards   
                GetNextTurn(choice);

            }
            catch (AsyncHTTPException e)
            {
                httpErrorHandler.HandleError(e, null);
                print(e);
            }
        }

        private async void GetNextTurn(string choice)
        {
            this.turnRoot = await battleAPI.GetNextTurn(choice);
            this.playerTurn = turnRoot.new_turn;
            this.playerLastTurn = turnRoot.turn_results;

            if (enemyTurn != null && enemyLastTurn == null)
            {
                enemyLastTurn = new Turn();
            }

            PropertiesCopy.CopyAll(enemyTurn, enemyLastTurn);
            this.enemyTurn = turnRoot.bot_turn;
            summonNextPlayerCard = true;
        }

        private async void OnPlayerReachedMaxTurns(string choice)
        {
            playerLastChoice = null;
            //Player reached max turn
            BattleCampaignRoot root = await battleAPI.GetAllTurns(choice);
            battleResults = root.battle_results;
            this.playerTurn = null;
            this.playerLastTurn = root.battle_results.player_turns[root.battle_results.player_turns.Count - 1];

            if (enemyTurn != null && enemyLastTurn == null)
            {
                enemyLastTurn = new Turn();
            }

            PropertiesCopy.CopyAll(enemyTurn, enemyLastTurn);
            summonNextPlayerCard = true;
        }

        public void OnPlayerCardFinishedSummon(CampaignBattleCard lastPlayerCard)
        {
            StartCoroutine(OnPlayerCardFinishedSummonCoroutine(lastPlayerCard));
        }

        public IEnumerator OnPlayerCardFinishedSummonCoroutine(CampaignBattleCard lastPlayerCard)
        {

            this.lastPlayerCard = lastPlayerCard;

            //After we have the turn result, we can handle the choice 
            if (HandleChoice(playerLastChoice))
            {
                print("im here in OnPlayerCardFinishedSummonCoroutine!");
                yield return new WaitUntil(() => finishedAnnounced == true);
                finishedAnnounced = false;
            }

            //Summon bot card
            StartCoroutine(SummonOneBotCard(enemyLastTurn, false));
        }


        public void OnEnemyCardFinishedSummon(CampaignBattleCard lastEnemyCard)
        {
            StartCoroutine(OnEnemyCardFinishedSummonCorotuine(lastEnemyCard));
        }

        public IEnumerator OnEnemyCardFinishedSummonCorotuine(CampaignBattleCard lastEnemyCard)
        {
            if (lastEnemyCard.GetBattleCard() != null)
            {
                SetEnemyBattleStats(enemyTurn, false);

                if (!comboManager.HandleCriticalHit(PlayerType.Enemy, (int)enemyTurn.crit_damage))
                {
                    // Raise a combo event only if crit does not occured.
                    if (comboManager.HandleCombo(lastEnemyCard.GetAmount(), PlayerType.Enemy, (int)enemyTurn.damage))
                    {
                        print("im here in OnPlayerCardFinishedSummonCoroutine");
                        yield return new WaitUntil(() => finishedAnnounced == true);
                        finishedAnnounced = false;
                    }

                    ////There is no combo or critial hit
                    //NotifySummonNextBotCard();
                }
                else
                {
                    print("im here in OnPlayerCardFinishedSummonCoroutine");
                    yield return new WaitUntil(() => finishedAnnounced == true);
                    finishedAnnounced = false;
                }
            }

            //There is no combo or critial hit
            NotifySummonNextBotCard();


            //Summon next choice
            print("currentTurn in onEnemyCardFinish is " + currentTurn);
            if (currentTurn < campaignBattleStats.GetMaxTurns())
            {
                SummonChoiceCard(playerTurn.card);
                yield break;
            }

            if (currentTurn == campaignBattleStats.GetMaxTurns() && !isReachedMaxCards)
            {
                isReachedMaxCards = true;
                RevealCloseCards();
                yield break;
            }
        }


        public IEnumerator SummonOneBotCard(Turn turn, bool lastCards)
        {
            // Dramatic pause...
            yield return new WaitForSeconds(1f);

            if (lastCards)
            {
                print("im here 3");
                botTurnManager.SetCloseCard(turn);
                yield break;
            }

            print("lets summon bot card...");
            botTurnManager.HandleOneTurn(turn, ChoicesConstants.DAMAGE, campaignBattleStats.GetEnemyBattleCards());
        }



        public void Restart()
        {
            print("start restarting...");
            playerTurnManager.RestartGame();
            botTurnManager.RestartGame();
            campaignBattleStats.ResetAllStats();
            lootManager.ResetLootbar();
            playerTurn = null;
            enemyTurn = null;
            //campaignBattleHUDManager.SetPlayerHUDActive(false);
            //campaignBattleHUDManager.SetHomeButtonsActive(true);
            campaignBattleHUDManager.IsRestarted(true);
            currentTurn = 0;
        }

        public void RevealCloseCards()
        {
            StartCoroutine(RevealCloseCards(battleResults, campaignBattleStats.GetPlayerScore()));
        }

        public IEnumerator RevealCloseCards(AllTurns allTurns, double playerScore)
        {
            // Summon the next cards to the enemy's deck.
            yield return RevealCloseCards(allTurns.bot_turns, playerScore);
            //Dramatic pause....
            yield return new WaitForSeconds(1f);
            //Check who won
            StartCoroutine(CheckWinnerCoroutine());
        }

        public IEnumerator RevealCloseCards(List<Turn> bot_turns, double playerScore)
        {

            print("im in RevealCloseCards!");
            print("num of flipped card is " + botTurnManager.GetNumOfFlipedCard());

            for (int i = bot_turns.Count - botTurnManager.GetNumOfFlipedCard(); i < bot_turns.Count; i++)
            {
                summonNextBotCard = false;
                enemyTurn = bot_turns[i];

                if (i > 0)
                {
                    if (bot_turns[i - 1].all_turns_damage > playerScore)
                    {
                        print("im here 1");
                        yield break;
                    }
                }
                if (i == 9)
                {
                    print("im here 2");
                    yield break;
                }

                StartCoroutine(SummonOneBotCard(enemyTurn, true));
                yield return new WaitUntil(() => summonNextBotCard == true);

            }
        }


        public void CheckWinner()
        {
            StartCoroutine(CheckWinnerCoroutine());
        }


        public IEnumerator CheckWinnerCoroutine()
        {

            //Some dramataic pause...
            yield return new WaitForSeconds(1f);

            if (playerTurnManager.HasWinAbility())
            {
                yield return AnnounceWinner(AnnouncerConstants.WINNER);
                isPlayerWin = true;
            }
            else if (botTurnManager.HasWinAbility())
            {
                yield return AnnounceWinner(AnnouncerConstants.LOSER);
            }
            else if (IsWinner(campaignBattleStats.GetPlayerScore(), campaignBattleStats.GetEnemyScore()))
            {
                yield return AnnounceWinner(AnnouncerConstants.WINNER);
                isPlayerWin = true;
            }
            else
            {
                yield return AnnounceWinner(AnnouncerConstants.LOSER);
            }

            yield return new WaitForSeconds(1f);

            Restart();


            if (isPlayerWin)
            {
                List<PrizeCard> prizes = battleCardHelper.CreatePrizes(battleResults.prize);
                if (prizes.Count == 0)
                {
                    print("onNoLootRobbed has been raised!");
                    onNoLootRobbed.Raise();
                    onPlayerWins.Raise();
                    yield break;
                }
                prizesManager.InstantiatePrizesCards(prizes);
                onPlayerWins.Raise();
                yield break;
            }

            onEnemyWins.Raise();

        }

        private IEnumerator AnnounceWinner(string winner)
        {
            OnFightEnded.Raise(winner);
            yield return new WaitUntil(() => finishedAnnounced == true);
            finishedAnnounced = false;
        }

        private bool IsWinner(double playerDamage, double enemyDamage)
        {
            if (playerDamage >= enemyDamage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetFinishedAnnouced(bool finishedAnnounced)
        {
            this.finishedAnnounced = finishedAnnounced;
        }

        //public void SetCriticalHit(Turn turn)
        //{
        //    //campaignBattleStats.SetPlayerCriticalHitChance(turn.card.all_turns_critical_hit.chance, false);
        //    //campaignBattleStats.SetPlayerCriticalHitDamage((int)turn.all_turns_critical_hit.damage, false);
        //}

        public void NotifySummonNextBotCard()
        {
            print("im in SetSummonNextBotCard");
            this.summonNextBotCard = true;
        }

        // Click Listeners
        public void OnDamageButtonClicked()
        {
            StartCoroutine(OnPlayerMakeChoice(ChoicesConstants.DAMAGE));
        }

        public void OnLootButtonClicked()
        {
            StartCoroutine(OnPlayerMakeChoice(ChoicesConstants.LOOT));
        }

        public void OnCritButtonClicked()
        {
            StartCoroutine(OnPlayerMakeChoice(ChoicesConstants.CRIT));
        }

        public void OnSaveButtonClicked()
        {
            StartCoroutine(OnPlayerMakeChoice(ChoicesConstants.SAVE));
        }

        public void onAbilityButtonClicked()
        {
            StartCoroutine(OnPlayerMakeChoice(ChoicesConstants.ABILITY));
        }

        public void OnAutoPlayButtonClicked(Button clickedButton)
        {
            campaignBattleHUDManager.OnAutoPlayButtonClicked(clickedButton);
            isAutoBattleOn = true;
        }

        public void OnManualButtonClicked(Button clickedButton)
        {
            campaignBattleHUDManager.OnAutoPlayButtonClicked(clickedButton);
            isAutoBattleOn = false;
        }



        //-------------------------------------------- TEST MODE FUNCTIONS -------------------------------------------

        public void InitalizeBattleTestMode()
        {
            testManager.SetCardOptionPickerActive(true);
        }

        public async void OnGoTestButtonClicked()
        {
            //Write the dropdown choices to a seperate class
            if (currentTurn == 0)
            {
                TurnRoot firstTurn = await testManager.GetFirstBattleCard();
                //GetFirstBattleCard(firstTurn);
            }
            else
            {
                testerChooseCard = true;
            }

            testManager.SetCardOptionPickerActive(false);
        }
    }
}

