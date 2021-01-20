using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PropertiesCopy : MonoBehaviour
{

    public static void CopyAll<T>(T source, T target)
    {

        if (source == null)
        {
            return;
        }

        var type = typeof(T);
        foreach (var sourceProperty in type.GetProperties())
        {
            var targetProperty = type.GetProperty(sourceProperty.Name);
            targetProperty.SetValue(target, sourceProperty.GetValue(source, null), null);
        }
        foreach (var sourceField in type.GetFields())
        {
            var targetField = type.GetField(sourceField.Name);
            targetField.SetValue(target, sourceField.GetValue(source));
        }
    }


}
