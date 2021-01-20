using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : HomePanel
{

    [SerializeField] private RectTransform scrollerContent = null;

    private Vector3 scrollerStartPosition;
    


    private void Awake()
    {
        scrollerStartPosition = scrollerContent.position + new Vector3(0, scrollerContent.GetComponent<RectTransform>().rect.y / 2, 0);
        print("scrollerStartPosition = " + scrollerStartPosition);
    }

    public override void ResetPanel()
    {
        //Set the scroller to the top
        scrollerContent.position = scrollerStartPosition;
    }

    public override void SetActive(bool active)
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
