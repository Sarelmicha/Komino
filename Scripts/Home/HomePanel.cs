using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HomePanel : MonoBehaviour
{
    public abstract void ResetPanel();
    public abstract void SetActive(bool active);
    public abstract void EnableCanvases(bool enabled);
}
