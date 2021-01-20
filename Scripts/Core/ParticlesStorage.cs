using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Komino.Core
{
    [CreateAssetMenu(fileName = "ParticleStorage", menuName = "Core/ParticleStorage")]
    public class ParticlesStorage : ScriptableObject
    {
        [SerializeField] GameObject commonParticle = null;
        [SerializeField] GameObject rareParticle = null;
        [SerializeField] GameObject epicParticle = null;
        [SerializeField] GameObject legendaryParticle = null;
        [SerializeField] GameObject bombParticle = null;
        [SerializeField] GameObject prizeParticle = null;

        public GameObject GetCommonParticle()
        {
            return this.commonParticle;
        }

        public GameObject GetRareParticle()
        {
            return this.rareParticle;
        }

        public GameObject GetEpicParticle()
        {
            return this.epicParticle;
        }

        public GameObject GetLegendaryParticle()
        {
            return this.legendaryParticle;
        }

        public GameObject GetBombParticle()
        {
            return this.bombParticle;
        }

        public GameObject GetPrizeParticle()
        {
            return this.prizeParticle;
        }

    }

   
}
