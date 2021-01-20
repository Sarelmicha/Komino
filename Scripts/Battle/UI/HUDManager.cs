using Komino.GameEvents.Events;
using ServerResponses.Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{

    protected AbbrevationUtility abbrevationUtility = new AbbrevationUtility();
    private bool isRestarted = false;

    protected IEnumerator IncrementToTarget(Text valueText, Value currentValue, int targetValue, string prefix,float delayBetweenScoreIncrement,int incerementValue)
    {
     
        while (currentValue.GetValue() < targetValue)
        {
            if(isRestarted)
            {
                yield break;
            }
            currentValue.SetValue(currentValue.GetValue() + incerementValue);
            valueText.text = prefix + abbrevationUtility.AbbreviateNumber(currentValue.GetValue());
            yield return new WaitForSeconds(delayBetweenScoreIncrement);
        }


        if (currentValue.GetValue() != targetValue)
        {
            currentValue.SetValue(targetValue);
            valueText.text = prefix + abbrevationUtility.AbbreviateNumber(currentValue.GetValue());
        }

    }

    protected IEnumerator DecrementToTarget(Text valueText, Value currentValue, int targetValue, string prefix, float delayBetweenScoreIncrement,int decrementValue)
    {
      
        while (currentValue.GetValue() > targetValue)
        {
           
            if (isRestarted)
            {
                print("im in restarted!!");
                yield break;
            }
            currentValue.SetValue(currentValue.GetValue() - decrementValue);
            valueText.text = prefix + abbrevationUtility.AbbreviateNumber(currentValue.GetValue());
            yield return new WaitForSeconds(delayBetweenScoreIncrement);
        }

        if (currentValue.GetValue() != targetValue)
        {
            currentValue.SetValue(targetValue);
            valueText.text = prefix + abbrevationUtility.AbbreviateNumber(currentValue.GetValue());
        }

    }

   

    protected void SetTextInstantly(Text text,Value currentValue, int newValue, string prefix)
    {
        currentValue.SetValue(newValue);

        text.text = prefix + abbrevationUtility.AbbreviateNumber(currentValue.GetValue());
    }

    public void SetRestarted()
    {
        this.isRestarted = false;
    }


   
    public void IsRestarted(bool isRestarted)
    {
        this.isRestarted = isRestarted;
    }


}
