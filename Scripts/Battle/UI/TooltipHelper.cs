using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipHelper
{

    public static void HandleTooltip(SimpleTooltip tooltip)
    {
        if (tooltip == null)
        {
            return;
        }

        if (tooltip.IsShowing())
        {
            tooltip.HideTooltip();

        }   
        else
        {

            tooltip.ShowTooltip();
        }       
    }
}
