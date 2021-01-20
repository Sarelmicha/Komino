using System;
using System.Collections.Generic;


namespace ServerResponses.Player
{
    [System.Serializable]
    public class PlayerInfoResponse
    {
        public string _id;
        public string display_name;
        public List<CardData> cards;
        public double power;
        public DateTime last_mana_collection;
        public double fame;
        public double level;
        public CurrenciesData currencies;
        public double total_games_played;
        public CampaignBattleStatistics campaign_battle_statistics;
        public DateTime server_time;


    }
    [System.Serializable]
    public class CurrenciesData
    {
        public double gems;
        public double coins;
        public double world_boss_tokens;
        public double boss_tokens;
    }

    [System.Serializable]
    public class CampaignBattleStatistics
    {
        public int wins;
        public int defeats;
        public int total_games_played;
        public int win_streak;
        public int lose_streak;
    }





    [System.Serializable]
    public class CardData
    {
        public string _id;
        public string identifier_name;
        public string display_name;
        public string nation;
        public string tier;
        public double level;
        public double power;
        public double attack;
        public List<string> abilities;
        public double critical_hit_chance; // Change back to int
        public double critical_hit_damage;
        public double chance_to_appear_in_deck;
        public double loot;
        public double cost;
        public string cast;
        public double game_cost;
        public PlayerCard player_card;
        public List<CardLevelData> levels;

        public CardData()
        {

        }

        public CardData(string nation)
        {
            this.nation = nation;
        }
    }

    [System.Serializable]
    public class Combo
    {
        public CardData card;
        public double amount;
        public double base_damage;
    }



    [System.Serializable]
    public class AllTurnsCriticalHit
    {
        public double chance;
        public double damage;
    }



    [System.Serializable]
    public class Turn
    {
        public CardData card;
        public Choices choices;
        public double crit_chance;
        public List<Combo> combos;
        public double loot;
        public double base_damage;
        public double crit_damage;
        public double damage;
        public AllTurnsCriticalHit all_turns_critical_hit;
        public double all_turns_damage;
        public double all_turns_loot;
        public bool played;
        public string choice;
    }



    [System.Serializable]
    public class AllTurns
    {
        public string game_status;
        public int max_turns;
        public List<Turn> bot_turns;
        public List<Turn> player_turns;
        public List<LootData> loot_bars;      
        public double player_deck_power;
        public double bot_deck_power;
        public BattlePrizeData prize { get; set; }
    }

    [System.Serializable]
    public class BattlePrizeData
    {
        public BattleCurrenciesData currencies { get; set; }
    }

    [System.Serializable]
    public class BattleCurrenciesData
    {
        public int coins { get; set; }
        public int gems { get; set; }
    }


    [System.Serializable]
    public class BattleCampaignRoot
    {
        public AllTurns battle_results;
    }

    [System.Serializable]
    public class LootData
    {
        public double min;
        public string type;
        public string skill;
    }

    [System.Serializable]
    public class BattleRoot
    {
        public double max_turns;
        public List<LootData> loot_bars;
        public Turn new_turn;
        public double player_deck_power;
        public List<CardData> player_cards;
        public double bot_deck_power;
        public List<string> bot_cards;         
    }

   
    [System.Serializable]
    public class BaseCritical
    {
        public double chance { get; set; }
        public double damage { get; set; }
    }

    [System.Serializable]
    public class Choices
    {
        public double damage;
        public double loot;
        public object ability;
        public double crit;
        public bool save;

    }


    [System.Serializable]
    public class TurnRoot
    {
        public Turn turn_results;
        public Turn new_turn;
        public Turn bot_turn;
    }

    [System.Serializable]
    public class BattleCostRoot
    {
        public int cost { get; set; }
    }

    [System.Serializable]
    public class GameStatusData
    {
        public string game_status { get; set; }
    }
    
    [System.Serializable]
    public class PlayerCard
    {
        public string _id;
        public double level;
        public bool in_deck;
        public double sculptures;
        public bool owned;
    }

    [System.Serializable]
    public class PlayerCurrenciesData
    {
        public double gems { get; set; }
        public double coins { get; set; }
        public double world_boss_tokens { get; set; }
        public double boss_tokens { get; set; }

    }

    [System.Serializable]
    public class CardLevelData
    {
        public double level { get; set; }
        public double attack { get; set; }
        public double average_attack { get; set; }
        public double power { get; set; }
        public double critical_hit_chance { get; set; }
        public double critical_hit_damage { get; set; }
        public double chance_to_appear_in_deck { get; set; }
        public double luck { get; set; }
        public CardCostData cost { get; set; }
    }


    [System.Serializable]
    public class CardCostData
    {
        public double coins { get; set; }
        public double sculptures { get; set; }
    }




    [System.Serializable]
    public class ShopCardData
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<ClosePrizeData> prizes { get; set; }
        public int cost { get; set; }
        public int amount { get; set; }
        public bool buy_with_gems { get; set; }
        public bool purchased { get; set; }
    }



    [System.Serializable]
    public class ShopData
    {
        public string _id { get; set; }
        public string type { get; set; }
        public DateTime last_refresh_time { get; set; }
        public List<ShopCardData> coins { get; set; }
        public List<ShopCardData> gems { get; set; }
        public List<ShopCardData> chests { get; set; }
        public int seconds_left { get; set; }
        public int refresh_gem_price { get; set; }
        public int refresh_gem_round_in_minutes { get; set; }
    }

    [System.Serializable]
    public class ShopDataRoot
    {
        public ShopData shop { get; set; }
    }


    public class ClosePrizeData
    {
        public string type { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public string tier { get; set; }
    }

    public class OpenPrizeData
    {
        public string type { get; set; }
        public int value { get; set; }
        public List<string> card { get; set; }
    }

    public class Payload
    {
        public string type { get; set; }
        public string sensitive { get; set; }
    }

    public class AutomateBattleSettings
    {
        public Payload payload { get; set; }
    }




    //-----------------TO BE DELETE--------------------------
    [System.Serializable]
    public class LongTurnRoot
    {
        public double player_hp;
        public double bot_hp;
        public double bot_current_restart;
        public double player_current_restart;
        public string battle_id;
        public double bot_current_turn;
        public double player_current_turn;
        public Turn bot_turn { get; set; }
        public Turn player_turn { get; set; }
    }


    [System.Serializable]
    public class ManaInfo
    {
        public int min;
        public int max;

        public ManaInfo()
        {

        }

        public ManaInfo(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }


    [System.Serializable]
    public class TurnsRoot
    {
        public List<Turn> turns;
    }

    [System.Serializable]
    public class Files
    {
        public int v;
        public List<string> new_files;
        public List<object> deleted_files;
    }

    [System.Serializable]
    public class FilesRoot
    {
        public Files files;
    }
}
