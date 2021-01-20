using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value 
{
    private int value;
    private bool setInstantly;
    private object value1;

    public Value(int value, bool setInstantly)
    {
        this.value = value;
        this.setInstantly = setInstantly;
    }

    public Value(int value)
    {
        this.value = value;
        this.setInstantly = false;
    }

    public int GetValue()
    {
        return this.value;
    }

    public void SetValue(int value)
    {
        this.value = value;
    }

    public bool SetInstantly()
    {
        return this.setInstantly;
    }

}
