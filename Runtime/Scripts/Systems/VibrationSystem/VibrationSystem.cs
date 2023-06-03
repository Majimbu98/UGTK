// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// A singleton class that handles the vibration system.
    /// </summary>
    public class VibrationSystem : Singleton<VibrationSystem>
    {
        #region Variables & Properties

        private Dictionary<S_Vibration, bool> vibrationIsPlaying = new Dictionary<S_Vibration, bool>();

        #endregion

        #region MonoBehaviour

        #region Activation/Deactivation

        /// <summary>
        /// Called when the object is enabled.
        /// Subscribes to necessary events.
        /// </summary>
        private void OnEnable()
        {
            EventManager.OnActiveVibration += ActiveVibration;
            EventManager.OnDeactiveVibration += DeactiveVibration;
        }

        /// <summary>
        /// Called when the object is disabled.
        /// Unsubscribes from events.
        /// </summary>
        private void OnDisable()
        {
            EventManager.OnActiveVibration -= ActiveVibration;
            EventManager.OnDeactiveVibration -= DeactiveVibration;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Activates a vibration based on the provided <paramref name="vibration"/> data.
        /// </summary>
        /// <param name="vibration">The vibration data to activate.</param>
        private void ActiveVibration(S_Vibration vibration)
        {
#if UNITY_ANDROID || UNITY_IOS
            if (!vibrationIsPlaying.ContainsKey(vibration))
            {
                bool activation = true;
                vibrationIsPlaying.Add(vibration, activation);
                StartCoroutine(VibrationTimeline(vibration, activation));
            }
#endif
        }

        /// <summary>
        /// Deactivates a vibration based on the provided <paramref name="vibration"/> data.
        /// </summary>
        /// <param name="vibration">The vibration data to deactivate.</param>
        private void DeactiveVibration(S_Vibration vibration)
        {
            vibrationIsPlaying[vibration] = false;
        }

        /// <summary>
        /// Executes the timeline of a vibration.
        /// </summary>
        /// <param name="vibration">The vibration data.</param>
        /// <param name="activation">The activation state of the vibration.</param>
        private IEnumerator VibrationTimeline(S_Vibration vibration, bool activation)
        {
#if UNITY_ANDROID || UNITY_IOS
            int index = 0;
            while (activation && index < vibration.list.Count)
            {
                switch (vibration.list[index].firstValue)
                {
                    case E_TimeType.Deelay:
                        yield return TimerDelay(vibration.list[index].secondValue, activation);
                        break;
                    case E_TimeType.Vibration:
                        yield return TimerVibration(vibration.list[index].secondValue, activation);
                        break;
                }

                index++;

            }

            if (vibrationIsPlaying.ContainsKey(vibration))
            {
                vibrationIsPlaying.Remove(vibration);
            }
#endif
            yield return null;
        }

        /// <summary>
        /// Executes a delay for the specified <paramref name="time"/>.
        /// </summary>
        /// <param name="time">The duration of the delay in seconds.</param>
        /// <param name="activation">The activation state of the vibration.</param>
        private IEnumerator TimerDelay(float time, bool activation)
        {
            float elapsedTime = 0f;

            while (elapsedTime < time && activation)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        /// <summary>
        /// Executes a vibration for the specified <paramref name="time"/>.
        /// </summary>
        /// <param name="time">The duration of the vibration in seconds.</param>
        /// <param name="activation">The activation state of the vibration.</param>
        private IEnumerator TimerVibration(float time, bool activation)
        {
#if UNITY_ANDROID || UNITY_IOS
            float elapsedTime = 0f;

            Handheld.Vibrate();
            while (elapsedTime < time && activation)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Handheld.Vibrate();
#endif
            yield return null;
        }

        #endregion

    }
}