//using AppState.Player;
//using BestHTTP;
//using Komino.CampaignBattle.Cards;
//using Komino.CampaignBattle.Cards.BattleCards;
//using Komino.CampaignBattle.Stats;
//using Komino.Core;
//using Komino.Enums;
//using Komino.GameEvents.Events;
//using Komino.States;
//using ServerResponses.Player;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UnityEngine;
//using System.Net;


//namespace Komino.CampaignBattle
//{
//    public class GameManager : MonoBehaviour
//    {
    
//        // Transforms
//        [SerializeField] private Transform playerImage = null;
//        [SerializeField] private Transform botImage = null;
//        [SerializeField] private Transform prizesArea = null;

//        // Events
//        [SerializeField] private StringEvent OnFightClicked = null;
//        [SerializeField] private StringEvent OnFightEnded = null;
//        [SerializeField] private StringEvent onDefuseAbilityOccured = null;
//        [SerializeField] private DamageEvent onComboOccures = null;
//        [SerializeField] private DamageEvent onCriticalHitOccures = null;
      
//        //LuckBar
//        [SerializeField] private Luckbar luckbar = null;

//        //Stats
//        [SerializeField] private GameState gameState = null;
//        [SerializeField] private CampaignBattleStats campaignBattleStats = null;

//         //Delays  
//        [SerializeField] private float delayTimeBetweenEachSummonedCardsInSeconds = 1f;
//        [SerializeField] private float delayTimeBetweenEachEnemySummonedCardsInSeconds = 0.2f;

//        // Errors Handlers
//        [SerializeField] private HttpErrorHandler httpErrorHandler = null;
//        [SerializeField] private GameErrorHandler gameErrorHandler = null;

//        //Storages
//        [SerializeField] private ParticlesStorage particlesStorage = null;
        
//        // Managers     
//        [SerializeField] private PrizeManager prizeManager = null;
//        [SerializeField] private CampaignBattleUIManager campaignBattleUIManager = null;
//        [SerializeField] private PlayerTurnManager playerTurnManager = null;
//        [SerializeField] private BotTurnManager botTurnManager = null;
//        [SerializeField] private LuckManager hudManager = null;


//        // APIs
//        private CampaignBattleAPI campaignBattleAPI = null;
//        private LongAPI longAPI = null;

//        // Helpers
//        private PrizesHelper prizesHelper = null;
//        private BattleCardHelper battleCardHelper = null;

//        private int commonPrizeLuckTarget = 120;      
//        private int epicPrizeLuckTarget = 400;
//        private int legendaryPrizeLuckTarget = 800;

//        private bool commonPrizeHasGiving = false;
//        private bool rarePrizeHasGiving = false;
//        private bool epicPrizeHasGiving = false;
//        private bool isPlayerTurn = false;
//        private bool summonNextBotCard = false;
//        private bool considerSecondPlayer = false;

//        private Turn playerLastTurn = null;
//        private Turn enemyLastTurn = null;
//        private LongTurnRoot lastTurn = null;




//        private void Awake()
//        {
//            battleCardHelper = new BattleCardHelper();
//            prizesHelper = new PrizesHelper();
//            campaignBattleAPI = new CampaignBattleAPI();
//            longAPI = new LongAPI();
//        }

//        /*private async void Start()
//        {
//            try
//            {
//                //luckbar.SetLuck(0);

//                gameState.nextBotTurn = -1;
//                campaignBattleStats.SetTotalMana(campaignBattleStats.GetTotalMana(),true);

//                BattleCampaignRoot battleCampaignRoot =  await campaignBattleAPI.GetOpenBattle();
//                SetOpenBattle(battleCampaignRoot);
//            }

//            catch (AsyncHTTPException e)
//            {
            
//                if (e.StatusCode == (int)HttpStatusCode.Gone)
//                {

//                    campaignBattleStats.ResetAllStats();
//                    GetBattleCost();
//                    return;
//                }
           
//                httpErrorHandler.HandleError(e, campaignBattleAPI.GetOpenBattle);
//            }
//        }

//        */

//        private async void Start()
//        {
//            try
//            {
//                campaignBattleStats.SetTotalMana(campaignBattleStats.GetTotalMana(), true);

//                BattleCampaignRoot battleCampaignRoot = await longAPI.GetFirstTurns();
//                StartCoroutine(SummonFirstCards(battleCampaignRoot));
//            }
//            catch (AsyncHTTPException e)
//            {
//                httpErrorHandler.HandleError(e, campaignBattleAPI.GetOpenBattle);
//            }
//        }

//        /*private async void GetBattleCost()
//        {
//            // There is no open battle
//            try
//            {
//                campaignBattleUIManager.SetButtonsActive(false);
//                campaignBattleUIManager.SetStartButtonActive(true);
//                CostRoot costRoot = await campaignBattleAPI.GetBattleCost();
//                print("cost of getting into the fight is " + costRoot.cost);

//                // -1 because we only get the min from costRoot
//                campaignBattleStats.SetManaPerSummon(new ManaInfo(costRoot.cost, -1));

//            }
//            catch (AsyncHTTPException e)
//            {
//                httpErrorHandler.HandleError(e, campaignBattleAPI.GetBattleCost);
//            }
//        }
//        */

//        /*
//        private void SetOpenBattle(BattleCampaignRoot root)
//        {

//            campaignBattleUIManager.SetButtonsActive(true);
//            campaignBattleUIManager.SetStartButtonActive(false);

//            //Reset the gameState before set its fields.
//            campaignBattleStats.ResetAllStats();
//            gameState.Restart();

//            //TODO - change from hard coded to the total luck
//            luckbar.SetLuck(200);

//            // Set first cards
//            gameState.SetPlayerBattleCards(battleCardHelper.CreateBattleCards(root.player_turns));
//            gameState.SetEnemyBattleCards(battleCardHelper.CreateBattleCards(root.bot_turns));

//            playerTurnManager.SetCards(root.player_turns);
//            botTurnManager.SetCards(root.bot_turns);
//            SetPrizes(root.player_turns);
         
//            SetBattleStats(root);

//            considerSecondPlayer = true;
//            playerTurnManager.SetSummonToCenter(true);
//        }
//        */

//        private void SetPrizes(List<Turn> player_turns)
//        {
//            foreach (Turn turn in player_turns)
//            {
//                //*******************************************************
//                //prizeManager.InstantiatePrizes(prizesHelper.CreatePrizes(turn.prizes),false);
//                //*******************************************************
//            }
//        }

//        private async void GetFirstBattleCards()
//        {
//            /*****************Start a campaign battle********************/
//            try
//            {
//                //BattleCampaignRoot root = await campaignBattleAPI.GetFirstTurns();

           
//                BattleCampaignRoot root =  await campaignBattleAPI.GetFirstTurns();
//                //campaignBattleStats.SetManaPerSummon(root.player_turns[root.player_turns.Count - 1].cost);

      

//                print("lets start playing...");
//                StartCoroutine(SummonFirstCards(root));
//            }
//            catch (AsyncHTTPException e)
//            {              
//                httpErrorHandler.HandleError(e, campaignBattleAPI.GetFirstTurns);
//            }
//        }

//        //Set battle stats of an open battle
//        private void SetBattleStats(BattleCampaignRoot root)
//        {
//            SetPlayerBattleStats(root.player_turns[root.player_turns.Count - 1],true);
//            //In the future change from 2 because it not neccetarly be 2 all the time
//            print("enemy score is now " + (int)root.bot_turns[2].all_turns_damage);
//            campaignBattleStats.SetEnemyScore((int)root.bot_turns[2].all_turns_damage,true);
//        }


//        private void SetPlayerBattleStats(Turn root,bool setInstantly)
//        {
//            //campaignBattleStats.SetManaPerSummon(root.cost);

//            //campaignBattleStats.SetPlayerScore((int)root.all_turns_damage, setInstantly);              
//            campaignBattleStats.SetBombChance(root.bomb_chance, setInstantly);
//            //campaignBattleStats.SetPlayerCriticalHitChance(root.all_turns_critical_hit.chance, setInstantly);
//            //campaignBattleStats.SetPlayerCriticalHitDamage((int)root.all_turns_critical_hit.damage, setInstantly);
            
//        }

//        private void SetEnemyBattleStats(Turn root, bool setInstantly)
//        {
//            campaignBattleStats.SetEnemyScore((int)root.all_turns_damage, setInstantly);
//        }


//        private IEnumerator SummonFirstCards(BattleCampaignRoot root)
//        {
//            /*
//            //Set to bot turn
//            //isPlayerTurn = false;

            
//            // Summon Enemys cards
//            for (int i = 0; i < root.bot_turns.Count; i++)
//            {
//                botTurnManager.HandleOneTurn(root.bot_turns[i], gameState.GetEnemyBattleCards());
//                campaignBattleStats.SetEnemyScore((int)root.bot_turns[i].all_turns_damage, false);                
//                yield return new WaitForSeconds(delayTimeBetweenEachSummonedCardsInSeconds);
//            }
            
//            // Wait to make some drama...
//            yield return new WaitForSeconds(1f);

//            */
//            //Set to player turn
//            isPlayerTurn = true;

//            print("lets summon player cards");

//            // Summon Players cards
//            for (int i = 0; i < root.player_turns.Count; i++)
//            {
//                playerLastTurn = root.player_turns[i];
//                playerTurnManager.HandleOneTurn(root.player_turns[i], gameState.GetPlayerBattleCards());
//                yield return new WaitForSeconds(delayTimeBetweenEachSummonedCardsInSeconds);
//            }

//            print("lets summon bot cards");

//            for (int i = 0; i < root.player_turns.Count; i++)
//            {
//                // Summon Bot card
//                enemyLastTurn = root.bot_turns[i];
//                StartCoroutine(SummonOneBotCard(enemyLastTurn));
//                yield return new WaitForSeconds(delayTimeBetweenEachSummonedCardsInSeconds);
//            }

//            considerSecondPlayer = true;
//            playerTurnManager.SetSummonToCenter(true);

//            campaignBattleUIManager.SetButtonsActive(true);
//        }

//        // Called when click on Button from UI
        
//        public void OnSmallClicked()
//        {
//            print("small have been clicked!");
//            Summon();
//        }
        

//        // Called when click on Button from UI
//        /*public void OnBigClicked()
//        {
//            print("big have been clicked!");
//            Summon(campaignBattleStats.GetManaPerSummon().max);
//        }
//        */
        

//        // Called when click on Button from UI
//        /*public void OnStartClicked()
//        {
//            print("start have been clicked!");
//            Summon(campaignBattleStats.GetManaPerSummon().min);
//        }
//        */

     
//        /*
//        public void Summon(int value)
//        {

//            if (campaignBattleStats.GetTotalMana() - value < 0)
//            {
//                gameErrorHandler.HandleError(GameError.NOT_ENOUGH_MANA,null);
//                return;
//            }

//            campaignBattleUIManager.SetButtonsActive(false);
//            campaignBattleUIManager.SetStartButtonActive(false);


//            isPlayerTurn = true;

//            if (isGameStarted)
//            {               
//                GetNextBattleCard(value);              
//            }
//            else
//            {
//                GetFirstBattleCards();  
//            }

//            // Reduce turn mana
//            campaignBattleStats.SetTotalMana(campaignBattleStats.GetTotalMana() - value,false);

//        }
//        */

//        public void Summon()
//        {

//            campaignBattleUIManager.SetButtonsActive(false);
//            campaignBattleUIManager.SetStartButtonActive(false);

//            isPlayerTurn = true;

//            GetNextBattleCard();
   

//        }

//        /*public async void GetNextBattleCard(int manaValue)
//        {
//            try
//            {
//                TurnRoot turnRoot = await campaignBattleAPI.GetNextTurn(manaValue);
//                playerLastTurn = turnRoot.turn;
//                //HandleLuckBar();
//                playerTurnManager.HandleOneTurn(playerLastTurn, gameState.GetPlayerBattleCards());
//            }

//            catch (AsyncHTTPException e)
//            {
//                httpErrorHandler.HandleError(e, null);
//                print(e);
//                //errorHandler.HandleError(e, campaignBattleAPI.GetNextTurn);
//            }
//        }
//        */

//        public async void GetNextBattleCard()
//        {
//            try
//            {
//                LongTurnRoot turn = await longAPI.GetNextTurn(); ;
//                lastTurn = turn;
//                playerLastTurn = lastTurn.player_turn;
//                enemyLastTurn = lastTurn.bot_turn;

//                //----------------
//                print("player hp from json is meir " + lastTurn.player_hp);
//                print("bot hp  from json is meir" + lastTurn.bot_hp);


          


//                //---------------

//                playerTurnManager.HandleOneTurn(playerLastTurn, gameState.GetPlayerBattleCards());
//            }

//            catch (AsyncHTTPException e)
//            {
//                httpErrorHandler.HandleError(e, null);
//                print(e);
//                //errorHandler.HandleError(e, campaignBattleAPI.GetNextTurn);
//            }
//        }

//        /*private void HandleLuckBar()
//        {
//           // luckbar.GainLuck(playerLastTurn.luck,GetNextChestLuckTarget((int)luckbar.luck));

//            if (playerLastTurn.card == null)
//            {
//                return;
//            }

//            if (playerLastTurn.card.luck != 0)
//            {
//                //TODO - Uncomment and see how to relocate prizes on bar according to resize of bar
//                //luckSlider.SetMaxHealth(luckSlider.GetMaxHealth() + playerLastTurn.card.luck);
//            }
//        }
//        */

//        public void AwardChest(int currentLuck)
//        {
            
//            //prizeManager.InstantiateChest(GetChestType(currentLuck));

//        }

//        public void BombCardSummoned()
//        {
//            if (isPlayerTurn)
//            {
//                StartCoroutine(BombCardSummonCoroutine(playerTurnManager, "player",gameState.GetPlayerBattleCards(),playerLastTurn));

//            }

//            else
//            {
//                StartCoroutine(BombCardSummonCoroutine(botTurnManager, "bot",gameState.GetEnemyBattleCards(),enemyLastTurn));

//            }
//        }

//        /*public IEnumerator BombCardSummonCoroutine()
//        {
//            if (isPlayerTurn)
//            {
//                if (playerTurnManager.HasDefuseAbility())
//                {
//                    GameObject defuseParticle = Instantiate(particlesStorage.GetCommonParticle());
//                    print("onDefuseAbilityOccured has been raised!");
//                    onDefuseAbilityOccured.Raise(AnnouncerConstants.DEFUSE);
//                    playerTurnManager.DestoryBombCard();
//                    campaignBattleUIManager.SetButtonsActive(true);
//                    playerTurnManager.HasDefuseAbility(false);

//                    yield break;
//                }

//                // Bomb was not defused
//                print("***********end of game.*****************");
//                // Instnatiate a bomb
//                GameObject bombParticle = Instantiate(particlesStorage.GetBombParticle());


//                //**********************************************************
//                //StartCoroutine(prizeManager.RewardPrizes(botImage));
//                //*****************************************************


//                OnFightEnded.Raise(AnnouncerConstants.LOSER);
//                print("here sarel");
//                Restart();
//                yield break;
//            }

//            else
//            {
//                if (botTurnManager.HasDefuseAbility())
//                {
//                    GameObject defuseParticle = Instantiate(particlesStorage.GetCommonParticle());
//                    onDefuseAbilityOccured.Raise(AnnouncerConstants.DEFUSE);
//                    botTurnManager.DestoryBombCard();
//                    campaignBattleUIManager.SetButtonsActive(true);
//                    botTurnManager.HasDefuseAbility(false);
//                    yield break;
//                }

//                // Bomb was not defused
//                print("***********end of game.*****************");

//                //Some dramatic pause...
//                yield return new WaitForSeconds(1f);
//                // Instnatiate a bomb
//                GameObject bombParticle = Instantiate(particlesStorage.GetBombParticle());
//                //**************************************************************
//                //StartCoroutine(prizeManager.RewardPrizes(playerImage));
//                //**********************************************************
//                print("OnFightEnded has been raised!");
//                OnFightEnded.Raise(AnnouncerConstants.WINNER);
//                print("here mor");
//                Restart();
//                yield break;
//            }
//        }
//        */

//        public IEnumerator BombCardSummonCoroutine(TurnManager turnManager,string playerType, List<BattleCard> battleCards,Turn lastTurn)
//        {
//            print("there is bomb and playerType is " + playerType);
//            if (turnManager.HasDefuseAbility())
//            {
//                HandleDefuse(turnManager);

//                yield break;
//            }

//            // Bomb was not defused
//            // Instnatiate a bomb
//            GameObject bombParticle = Instantiate(particlesStorage.GetBombParticle());
//            //Need to change not from hard coded number 50
//            if (playerType == "player")
//            {
//                campaignBattleStats.ResetStats(playerType);
//                hudManager.ReduceLife(PlayerType.Enemy, 50);
//                isPlayerTurn = false;
//                StartCoroutine(SummonOneBotCard(enemyLastTurn));
//            }

//            else
//            {
//                hudManager.ReduceLife(PlayerType.Player, 50);
//            }

//            GetNewCards(turnManager,playerType,battleCards,lastTurn);
//            campaignBattleUIManager.SetButtonsActive(true);
//            yield break;

//        }

//        private void HandleDefuse(TurnManager turnManager)
//        {
//            GameObject defuseParticle = Instantiate(particlesStorage.GetCommonParticle());
//            print("onDefuseAbilityOccured has been raised!");
//            onDefuseAbilityOccured.Raise(AnnouncerConstants.DEFUSE);
//            turnManager.DestoryBombCard();
//            campaignBattleUIManager.SetButtonsActive(true);
//            turnManager.HasDefuseAbility(false);
//        }

//        //Called from listener
//        public void GetPlayerNewCards()
//        {
//            GetNewCards(playerTurnManager, "player", gameState.GetPlayerBattleCards(),playerLastTurn);
//            //StartCoroutine(SummonOneBotCard());
//        }

//        public async void GetNewCards(TurnManager turnManager, string playerType,List<BattleCard> battleCards,Turn lastTurn)
//        {
//            try
//            {
//                print("im here and player type is " + playerType);
//                turnManager.RestartBoard();
//                gameState.ResetCards(playerType);
//                campaignBattleStats.ResetStats(playerType);
//                campaignBattleUIManager.SetButtonsActive(false);

//                TurnsRoot root = await longAPI.Restart(playerType);

               
//                RaffleNewCards(turnManager, root.turns, battleCards, lastTurn);

//            }
//            catch (AsyncHTTPException e)
//            {
//                httpErrorHandler.HandleError(e, longAPI.GetNextTurn);
//            }
//        }

//        private void RaffleNewCards(TurnManager turnManager,List<Turn> turns,List<BattleCard> battleCards,Turn lastTurn)
//        {
//            int numOfFirstRaffledCards = 1;
//            considerSecondPlayer = false;
//            playerTurnManager.SetSummonToCenter(false);

//            for (int i = 0; i < numOfFirstRaffledCards; i++)
//            {
//                lastTurn = turns[i];
//                turnManager.HandleOneTurn(lastTurn, battleCards);                       
//            }


//            considerSecondPlayer = true;
//            playerTurnManager.SetSummonToCenter(true);
//            campaignBattleUIManager.SetButtonsActive(true);

//        }

//        public void OnPlayerCardFinishedSummon(BattleCardController summonedBattleCardController)
//        {

//            print("im on OnPlayerCardFinishedSummon");

//            SetPlayerBattleStats(playerLastTurn,false);

//            if (!considerSecondPlayer)
//            {
//                return;
//            }

//            if (!HandleCriticalHit(PlayerType.Player,(int)playerLastTurn.crit_damage))
//            {
//                // Raise a combo event only if crit does not occured.
//                HandleCombo(summonedBattleCardController.GetAmount(),PlayerType.Player,(int)playerLastTurn.damage);
//            }

//            //Player turn finished
//            isPlayerTurn = false;
           
//            StartCoroutine(SummonOneBotCard(enemyLastTurn));
//        }

//        public void OnEnemyCardFinishedSummon(BattleCardController summonedBattleCardController)
//        {

//            SetEnemyBattleStats(enemyLastTurn, false);

//            if (!HandleCriticalHit(PlayerType.Enemy, (int)enemyLastTurn.crit_damage))
//            {

//                // Raise a combo event only if crit does not occured.
//                if (!HandleCombo(summonedBattleCardController.GetAmount(), PlayerType.Enemy, (int)enemyLastTurn.damage))
//                {
//                    //There is no combo or critial hit
//                    NotifySummonNextBotCard();
//                }
//            }

//            campaignBattleUIManager.SetButtonsActive(true);
//        }

//        public IEnumerator SummonOneBotCard(Turn turn)
//        {
//            // Dramatic pause...
//            yield return new WaitForSeconds(0.5f);

//            int randomNum = UnityEngine.Random.Range(0, 5);
//            if (randomNum == 3 && considerSecondPlayer && !enemyLastTurn.cancel_random)
//            {
//                GetNewCards(botTurnManager,"bot",gameState.GetEnemyBattleCards(),enemyLastTurn);
//                yield break;
//            }

//            if (considerSecondPlayer && enemyLastTurn.restart)
//            {
//                GetNewCards(botTurnManager, "bot", gameState.GetEnemyBattleCards(), enemyLastTurn);
//                yield break;
//            }

//            enemyLastTurn = turn;
//            botTurnManager.HandleOneTurn(turn, gameState.GetEnemyBattleCards());

//        }

//        public bool HandleCombo(int amount,PlayerType playerType,int damage)
//        {
//            print("im here playerType is " + playerType + "and amount is sss" + amount);

//            if (!considerSecondPlayer)
//            {
//                return false;
//            }

//            if (amount <= 1)
//            {
//                return false;
//            }

//            switch (amount)
//            {
//                case 2:
//                    // Raise an event that combo has accourd
//                    print("onComboOccures has been raised!");
//                    onComboOccures.Raise(new Damage(playerType, AnnouncerConstants.DOUBLE_COMBO,damage));
//                    hudManager.ReduceLife(playerType, damage);
//                    return true;
//                case 3:
//                    print("onComboOccures has been raised!");
//                    onComboOccures.Raise(new Damage(playerType, AnnouncerConstants.TRIPLE_COMBO, damage));
//                    hudManager.ReduceLife(playerType, damage);
//                    return true;
//                case 4:
//                    print("onComboOccures has been raised!");
//                    onComboOccures.Raise(new Damage(playerType, AnnouncerConstants.QUADRO_COMBO, damage));
//                    hudManager.ReduceLife(playerType, damage);
//                    return true;                 
//            }

//            return false;

//        }

//        private bool HandleCriticalHit(PlayerType playerType, int damage)
//        {
//            print("im here playerType is " + playerType + "and damage is " + damage);

//            if (!considerSecondPlayer)
//            {
//                return false;
//            }

//            if (damage > 0)
//            {
//                print("onCriticalHitOccures has been raised sss!");
//                onCriticalHitOccures.Raise(new Damage(playerType, AnnouncerConstants.CRITICAL_HIT,damage));
//                hudManager.ReduceLife(playerType, damage);
                
//                //Critical hit has occured
//                return true;
//            }
//            // Critical hit has not occured
//            return false;
//        }

//        public void Restart()
//        {
//            print("start restarting...");

//            campaignBattleStats.ResetAllStats();
//            playerTurnManager.RestartGame();
//            botTurnManager.RestartGame();
//            gameState.Restart();           
//            //luckbar.Restart();          
//            //GetBattleCost();
//            //prizeManager.Restart();

//            playerLastTurn = null;
//            enemyLastTurn = null;

//            //Check why this line is here
//            //luckbar.SetMaxLuck(luckbar.maximumLuck);

//            considerSecondPlayer = false;

          
//            print("finsihed restarting...");
//        }

        

//        public async void Fight()
//        {
//            try
//            {
//                print("fight have been clicked!");
//                //Disable buttons
//                campaignBattleUIManager.SetButtonsActive(false);

//                FightRoot root = await campaignBattleAPI.GetAllTurns();
//                print("OnFightClicked has been raised!");
//                OnFightClicked.Raise(AnnouncerConstants.FIGHT);

//                isPlayerTurn = false;

//                StartCoroutine(Fight(root,campaignBattleStats.GetPlayerScore()));

//            }
//            catch (AsyncHTTPException e)
//            {
//                print("error occured here");
//                httpErrorHandler.HandleError(e, campaignBattleAPI.GetAllTurns);
//            }
//        }

//        public IEnumerator Fight(FightRoot root, int playerScore)
//        {
//            //Dramatic pause before start the fight
//            yield return new WaitForSeconds(0.5f);

//            // Summon the next cards to the enemy's deck.
//            yield return SummonEnemyRemainsCards(root.bot_turns, playerScore);

//            if (root.bot_turns[root.bot_turns.Count - 1].is_bomb && !botTurnManager.HasDefuseAbility())
//            {
//                yield break;
//            }

//            print("finish the summon!");

//            //Dramatic pause....
//            yield return new WaitForSeconds(1f);

//            //Check who won
//            StartCoroutine(CheckWinner());
//        }


//        public IEnumerator SummonEnemyRemainsCards(List<Turn> bot_turns, int playerScore)
//        {
//            for (int i = botTurnManager.GetNextEmptyCardPlaceHolderIndex(); i < bot_turns.Count; i++)
//            {
//                summonNextBotCard = false;

//                print("inside SummonEnemyRemainsCards and i is " + i);

//                if (i > 0)
//                {
//                    if (bot_turns[i - 1].all_turns_damage > playerScore)
//                    {
//                        //Dramatic pause....
//                        yield return new WaitForSeconds(1f);
//                        // Check who won
//                        StartCoroutine(CheckWinner());
//                        yield break;
//                    }
//                }

//                if(i == 9)
//                {
//                    //Dramatic pause....
//                    yield return new WaitForSeconds(1f);
//                    // Check who won
//                    StartCoroutine(CheckWinner());
//                    yield break;
//                }

//                enemyLastTurn = bot_turns[i];
//                StartCoroutine(SummonOneBotCard(enemyLastTurn));

         
//                yield return new WaitUntil(() => summonNextBotCard == true);

//            }
//        }

//        public void HandleEndOfFight(Turn lastBotTurn)
//        {
//            print("HandleEndOfFight and all turns damage is " + lastBotTurn.all_turns_damage);
//            campaignBattleStats.SetEnemyScore((int)lastBotTurn.all_turns_damage,false);
//            StartCoroutine(CheckWinner());
//        }



//        public IEnumerator CheckWinner()
//        {

//            //Some dramataic pause...
//            yield return new WaitForSeconds(1f);

//            if (playerTurnManager.HasWinAbility())
//            {
//                OnFightEnded.Raise(AnnouncerConstants.WINNER);
//                //*******************************************************
//               // StartCoroutine(prizeManager.RewardPrizes(playerImage));
//                //*******************************************************
//            }
//            else if (botTurnManager.HasWinAbility())
//            {
//                OnFightEnded.Raise(AnnouncerConstants.LOSER);
//                //*******************************************************
//               // StartCoroutine(prizeManager.RewardPrizes(botImage));
//                //*******************************************************
//            }

//            else if (IsWinner(campaignBattleStats.GetPlayerScore(), campaignBattleStats.GetEnemyScore()))
//            {
//                OnFightEnded.Raise(AnnouncerConstants.WINNER);
//                //*******************************************************
//                //StartCoroutine(prizeManager.RewardPrizes(playerImage));
//                //*******************************************************
//            }

//            else
//            {
//                OnFightEnded.Raise(AnnouncerConstants.LOSER);
//                //*******************************************************
//                //StartCoroutine(prizeManager.RewardPrizes(botImage));
//                //*******************************************************
//            }
          
//        }

//        public void GameOver()
//        {
//            if (lastTurn.bot_hp <= 0)
//            {
//                OnFightEnded.Raise(AnnouncerConstants.WINNER);
//            }

//            else if (lastTurn.player_hp <= 0)
//            {
//                OnFightEnded.Raise(AnnouncerConstants.LOSER);
//            }

//            Restart();

//        }

//        private int GetNextChestLuckTarget(int currentLuck)
//        {
//            print("current luck is " + currentLuck);

//            if (currentLuck < commonPrizeLuckTarget)
//            {
//                return commonPrizeLuckTarget;
//            }

//            if (currentLuck >= commonPrizeLuckTarget && luckbar.luck < epicPrizeLuckTarget)
//            {
//                return epicPrizeLuckTarget;
//            }

//            if (currentLuck >= epicPrizeLuckTarget && luckbar.luck < legendaryPrizeLuckTarget)
//            {
//                return legendaryPrizeLuckTarget;
//            }

//            return legendaryPrizeLuckTarget;
//        }

       


//        private bool IsWinner(int playerDamage, int enemyDamage)
//        {

//            if (playerDamage >= enemyDamage)            
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        private string GetChestType(int luck)
//        {
//            if (luck >= commonPrizeLuckTarget && luck < epicPrizeLuckTarget)
//            {
//                return ChestsConstants.COMMON;
//            }

//            if (luck >= epicPrizeLuckTarget && luckbar.luck < legendaryPrizeLuckTarget)
//            {
//                return ChestsConstants.EPIC;
//            }

//            if (luck >= legendaryPrizeLuckTarget)
//            {
//                return ChestsConstants.LEGENDARY;
//            }

//            return ChestsConstants.LEGENDARY;

//        }


//        // Abilities Functions ---------------------------------------------------
//        public void SetDefuseAbility()
//        {
//            if (isPlayerTurn)
//            {
//                this.playerTurnManager.HasDefuseAbility(true);
//            }
//            else
//            {
//                this.botTurnManager.HasDefuseAbility(true);
//            }
            
//        }

//        public void SetWinAbility()
//        {
//            if (isPlayerTurn)
//            {
//                this.playerTurnManager.HasWinAbility(true);
//            }
//            else
//            {
//                this.botTurnManager.HasWinAbility(true);
//            }
//        }

     

//        public void SetBombChance(int bombChance)
//        {
//            campaignBattleStats.SetBombChance(bombChance,false);
//        }

//        public void SummonEnemyRemainsCard(Turn turn)
//        {
//            isPlayerTurn = false;
//            StartCoroutine(SummonEnemyRemainsCards(turn.bot_cards,campaignBattleStats.GetPlayerScore()));
//        }

//        public void SetCriticalHit(Turn turn)
//        { 
//            campaignBattleStats.SetPlayerCriticalHitChance(turn.all_turns_critical_hit.chance,false);
//            campaignBattleStats.SetPlayerCriticalHitDamage((int)turn.all_turns_critical_hit.damage,false);
//        }

//        public void NotifySummonNextBotCard()
//        {
//            print("im in SetSummonNextBotCard");
//            this.summonNextBotCard = true;
//        }

//    }
//}
