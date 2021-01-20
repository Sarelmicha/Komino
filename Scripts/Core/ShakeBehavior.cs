using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Komino.Core
{
    public class ShakeBehavior : MonoBehaviour
    {
        // Desired duration of the shake effect
        float shakeDuration = 0f;

        // A measure of magnitude for the shake. Tweak based on your preference
        [SerializeField] float shakeMagnitude = 1f;

        // A measure of how quickly the shake effect should evaporate
        [SerializeField] float dampingSpeed = 2f;

        // The initial position of the GameObject
        Vector3 initialPosition;

        private void OnEnable()
        {
            initialPosition = transform.localPosition;
        }

        private void Update()
        {
            if (shakeDuration == 0)
            {
                return;
            }

            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }

        public void TriggerShake()
        {
            shakeDuration = 1.0f;
        }
    }
}
