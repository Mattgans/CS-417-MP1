using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ClockSample
{
    public class Clock : MonoBehaviour
    {
        public Transform handHours;
        public Transform handMinutes;
        public Transform handSeconds;

        [Header("Fast Spin Settings")]
        public bool spinFast = false;
        public float spinSpeed = 720f;

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


        private void UpdateRealTime()
        {
            float handRotationHours   = DateTime.Now.Hour   * 30f; 
            float handRotationMinutes = DateTime.Now.Minute * 6f; 
            float handRotationSeconds = DateTime.Now.Second * 6f;

            if (handHours)
                handHours.localEulerAngles = new Vector3(0, 0, handRotationHours);
            if (handMinutes)
                handMinutes.localEulerAngles = new Vector3(0, 0, handRotationMinutes);
            if (handSeconds)
                handSeconds.localEulerAngles = new Vector3(0, 0, handRotationSeconds);
        }

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

        public void StartFastSpin()
        {
            spinFast = true;
            Invoke(nameof(RunOtherCode), 10f);

        }

        private void RunOtherCode()
        {
            SceneManager.LoadScene("FutureRoom");
        }
    }
}
