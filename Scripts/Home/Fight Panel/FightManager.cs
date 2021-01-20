using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    private Loader loader = null;
    [SerializeField] GameObject spinner = null;


    private void Awake()
    {
        loader = GameObject.FindGameObjectWithTag("loader").GetComponent<Loader>();
    }

    public void OnFightClicked()
    {
        spinner.SetActive(true);
        loader.LoadNewBattleScene();
    }

}
