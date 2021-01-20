using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUI : MonoBehaviour
{
    private Animator anim = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Resize(Value value)
    {
        anim.SetInteger("resize", value.GetValue());
    }
}

