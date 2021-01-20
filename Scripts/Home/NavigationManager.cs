using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    [SerializeField, SerializeReference]
    private HomePanel[] panels = null;
    [SerializeField] HomePanel activePanel = null;


    private void Awake()
    {
        SetPanels(activePanel);
    }
    public void NavigateToPanel(HomePanel activePanel)
    {
        if (this.activePanel == activePanel)
            return;

        SetPanels(activePanel);
       
    }

    public void SetPanels(HomePanel activePanel)
    {
        ResetPanels();
        ActivatePanel(activePanel);
    }

    public void ResetPanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].ResetPanel();
            //panels[i].SetActive(false);
            panels[i].EnableCanvases(false);
        }
    }

    public void ActivatePanel(HomePanel activePanel)
    {
        this.activePanel = activePanel;
        //activePanel.SetActive(true);
        activePanel.EnableCanvases(true);
    }

}
