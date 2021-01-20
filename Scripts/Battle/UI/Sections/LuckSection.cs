using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckSection : MonoBehaviour
{
    private SimpleTooltip tooltip = null;

    private void Awake()
    {
        tooltip = GetComponent<SimpleTooltip>();
    }

    public void HandleTooltip()
    {
        TooltipHelper.HandleTooltip(tooltip);
    }
}
