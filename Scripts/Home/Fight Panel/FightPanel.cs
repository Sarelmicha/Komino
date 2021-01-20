using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPanel : HomePanel
{
    public override void ResetPanel()
    {
        //TODO- implement this method if needed
    }

    public override  void SetActive(bool active)
    {
        transform.gameObject.SetActive(active);
    }

    public override void EnableCanvases(bool enabled)
    {
        Canvas[] canvases = GetComponentsInChildren<Canvas>();

        foreach (Canvas canvas in canvases)
        {
            canvas.enabled = enabled;
        }
    }
}
