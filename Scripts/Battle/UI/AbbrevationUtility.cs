using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reverse Comperator
public sealed class ReverseComparer<T> : IComparer<T>
{
    private readonly IComparer<T> inner;
    public ReverseComparer() : this(null) { }
    public ReverseComparer(IComparer<T> inner)
    {
        this.inner = inner ?? Comparer<T>.Default;
    }
    int IComparer<T>.Compare(T x, T y) { return inner.Compare(y, x); }
}

public class AbbrevationUtility
{
    private readonly SortedDictionary<int, string> abbrevations = new SortedDictionary<int, string>(new ReverseComparer<int>())
     {
        {1000,"K"},
        {1000000, "M" },
        {1000000000, "B" },    
     };

    public string AbbreviateNumber(int number)
    {
        foreach (KeyValuePair<int, string> pair in abbrevations)
        {

            if (number >= pair.Key)
            {
                int roundedNumber = Mathf.FloorToInt(number / pair.Key);
            
                return roundedNumber.ToString() + pair.Value;
            }
        }

        return number.ToString();
      
    }
}
