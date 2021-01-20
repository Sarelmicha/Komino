using Komino.Enums;
using Komino.GameEvents.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Announcer : MonoBehaviour
{
    [SerializeField] private Text announcerText = null;
    [SerializeField] private Text playerAnnouncerDamageText = null;
    [SerializeField] private Text playerAnouncerDamageValue = null;
    [SerializeField] private Text enemyAnnouncerDamageText = null;
    [SerializeField] private Text enemyAnouncerDamageValue = null;

    [SerializeField] private VoidEvent onFinishedAnnounce = null;
    [SerializeField] private VoidEvent onFinishedAnnounceDamage = null;

    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    // Called by the listener
    public void Announce(string text)
    {
        StartCoroutine(AnnounceCoroutine(text));
    }

    public IEnumerator AnnounceCoroutine(Damage damage)
    {

        if (damage.GetDamageType() == AnnouncerConstants.DOUBLE_COMBO)
        {
            animator.ResetTrigger("fadeRegular");
            announcerText.text = damage.GetDamageType();
            animator.SetTrigger("fadeRegular");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            onFinishedAnnounce.Raise();
            yield break;
        }

        if (damage.GetDamageType() == AnnouncerConstants.TRIPLE_COMBO || damage.GetDamageType() == AnnouncerConstants.CRITICAL_HIT || damage.GetDamageType() == AnnouncerConstants.QUADRO_COMBO)
        {
            animator.ResetTrigger("fadeRegular");
            announcerText.text = damage.GetDamageType();
            animator.SetTrigger("fadeRegular");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            onFinishedAnnounce.Raise();
            yield break;
        }
    }

    public IEnumerator AnnounceCoroutine(string text)
    {
        if (text == AnnouncerConstants.WINNER || text == AnnouncerConstants.LOSER ||
           text == AnnouncerConstants.FIGHT || text == AnnouncerConstants.DEFUSE)
        {
            animator.ResetTrigger("fadeRegular");
            announcerText.text = text;
            animator.SetTrigger("fadeRegular");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            onFinishedAnnounce.Raise();
            yield break;
        }
    }

    // Called by the listener
    public void Announce(Damage damage)
    {
        StartCoroutine(AnnounceCoroutine(damage));     
    }
}
