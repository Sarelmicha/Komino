using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : HomePanel
{
    [SerializeField] Canvas cardInfoCanvas = null;
    [SerializeField] RectTransform scrollerContent = null;

    private Vector3 scrollerStartPosition;


    private void Awake()
    {
        scrollerStartPosition = scrollerContent.position;
        print(scrollerStartPosition);
    }

    public override void ResetPanel()
    {
        cardInfoCanvas.enabled = false;
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
            if (canvas != cardInfoCanvas)
            {
                canvas.enabled = enabled;
            }
        }
    }
}
