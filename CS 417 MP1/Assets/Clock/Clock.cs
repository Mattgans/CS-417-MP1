using System;
using UnityEngine;

namespace ClockSample
{
    public class Clock : MonoBehaviour
    {
        public Transform handHours;
        public Transform handMinutes;
        public Transform handSeconds;

        [Header("Fast Spin Settings")]
        public bool spinFast = false;      // Default: real time
        public float spinSpeed = 720f;     // Degrees per second for fast spin

        private void Update()
        {
            if (spinFast)
            {
                SpinFast();
            }
            else
            {
                UpdateRealTime();
            }
        }

        // Normal real-time clock
        private void UpdateRealTime()
        {
            float handRotationHours   = DateTime.Now.Hour   * 30f; // 360 / 12
            float handRotationMinutes = DateTime.Now.Minute * 6f;  // 360 / 60
            float handRotationSeconds = DateTime.Now.Second * 6f;

            if (handHours)
                handHours.localEulerAngles = new Vector3(0, 0, handRotationHours);
            if (handMinutes)
                handMinutes.localEulerAngles = new Vector3(0, 0, handRotationMinutes);
            if (handSeconds)
                handSeconds.localEulerAngles = new Vector3(0, 0, handRotationSeconds);
        }

        // Fast spinning mode
        private void SpinFast()
        {
            float rotation = spinSpeed * Time.deltaTime;

            if (handHours)
                handHours.Rotate(0, 0, rotation);
            if (handMinutes)
                handMinutes.Rotate(0, 0, rotation * 2f);
            if (handSeconds)
                handSeconds.Rotate(0, 0, rotation * 4f);
        }

        // Call this when key is inserted
        public void StartFastSpin()
        {
            spinFast = true;
        }

        // Call this when key is removed (optional)
        public void StopFastSpin()
        {
            spinFast = false;
        }
    }
}
