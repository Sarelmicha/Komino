using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppState.Game;
using ServerResponses.Player;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    //UI elements
    [SerializeField] private Canvas testCanvas = null;
    [SerializeField] private Dropdown PlayerPickCardDropdown = null;
    [SerializeField] private Dropdown EnemyPickCardDropdown = null;
    [SerializeField] private Dropdown PlayerPickCardLevelDropdown = null;
    [SerializeField] private Dropdown EnemyPickCardLevelDropdown = null;

    //API
    [SerializeField] TestAPI testAPI = null;

    //Game State
    [SerializeField] GameState gameState = null;

    private void Awake()
    {
        testAPI = new TestAPI();
    }

    public async Task<TurnRoot> GetFirstBattleCard()
    {      
        return await testAPI.GetFirstTurn(Tester.PlayerPickCardId);

    }

    private string GetBattleCardId(string name)
    {

        foreach (KeyValuePair<string, CardData> entry in gameState.Cards)
        {
            if (entry.Value.identifier_name == name)
            {
                return entry.Value._id;
            }
        }


        return null;
    }

    public void WriteTesterPicks()
    {
        Tester.PlayerPickCardId = GetBattleCardId(PlayerPickCardDropdown.options[PlayerPickCardDropdown.value].text);
        Tester.EnemyPickCardId = GetBattleCardId(PlayerPickCardDropdown.options[EnemyPickCardDropdown.value].text);
        Tester.PlayerPickLevelCard = PlayerPickCardDropdown.options[PlayerPickCardLevelDropdown.value].text;
        Tester.EnemyPickLevelCard = PlayerPickCardDropdown.options[EnemyPickCardLevelDropdown.value].text;

    }

    public void SetTestCanvasEnabled(bool enabled)
    {
        testCanvas.enabled = enabled;
    }

    public void SetDropdownActive(Dropdown dropdown, bool active)
    {
        dropdown.gameObject.SetActive(active);
        dropdown.gameObject.SetActive(active);
    }

    public void SetCardOptionPickerActive(bool active)
    {
        SetTestCanvasEnabled(active);
        SetDropdownActive(PlayerPickCardDropdown,active);
        SetDropdownActive(PlayerPickCardLevelDropdown, active);
        SetDropdownActive(EnemyPickCardDropdown, active);
        SetDropdownActive(EnemyPickCardLevelDropdown, active);
    }
}
