using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Komino.CampaignBattle.Cards
{
    public class CardHolder : MonoBehaviour
    {
        void OnDrawGizmosSelected()
        {
            // Draw a semitransparent blue cube at the transforms position
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawWireCube(transform.position, new Vector3(0.6f, 0.8f, 1));
        }
    }
}
