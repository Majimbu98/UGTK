// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using System;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Utility class for managing time-related operations in Unity.
    /// </summary>
    public static class Timer
    {
        #region Methods

        #region Initialization

        /// <summary>
        /// Initializes the Timer class by subscribing to the application quitting event.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Application.quitting += Shutdown;
        }

        /// <summary>
        /// Cleans up any resources used by the Timer class.
        /// </summary>
        private static void Shutdown()
        {
            // No cleanup required for now
        }

        #endregion

        #region CallToCourutines
        
        /// <summary>
        /// Executes an action after a specified delay.
        /// </summary>
        /// <param name="time">The delay in seconds before executing the action.</param>
        /// <param name="action">The action to be executed.</param>
        public static void DoAfterTime(float time, Action action)
        {
            Coroutine.Instance.StartCoroutine(DoAfterTimeCoroutine(time, action));
        }

        /// <summary>
        /// Executes an action for a specified duration of time if a boolean value changes during that time.
        /// </summary>
        /// <param name="time">The duration of time to check for the boolean value change.</param>
        /// <param name="boolValue">The boolean value to monitor for changes.</param>
        /// <param name="action">The action to be executed if the boolean value changes.</param>
        public static void DuringTimeIfBoolChangesDo(float time, bool boolValue, Action action)
        {
            Coroutine.Instance.StartCoroutine(DuringTimeIfBoolChangesDoCoroutine(time, boolValue, action));
        }

        /// <summary>
        /// Executes an action after a specified delay if a boolean value changes during that time.
        /// </summary>
        /// <param name="time">The delay in seconds before executing the action.</param>
        /// <param name="boolValue">The boolean value to monitor for changes.</param>
        /// <param name="action">The action to be executed if the boolean value changes.</param>
        public static void DoAfterTimeIfBoolChanges(float time, bool boolValue, Action action)
        {
            Coroutine.Instance.StartCoroutine(DoAfterTimeIfBoolChangesCoroutine(time, boolValue, action));
        }
        
        #endregion
        
        #region Courutines
        
        private static System.Collections.IEnumerator DoAfterTimeCoroutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }

        private static System.Collections.IEnumerator DuringTimeIfBoolChangesDoCoroutine(float time, bool boolValue, Action action)
        {
            float timer = 0f;
            bool previousBoolValue = boolValue;

            while (timer < time)
            {
                yield return null;

                if (boolValue != previousBoolValue)
                {
                    action?.Invoke();
                    yield break;
                }

                timer += Time.deltaTime;
            }
        }

        private static System.Collections.IEnumerator DoAfterTimeIfBoolChangesCoroutine(float time, bool boolValue, Action action)
        {
            float timer = 0f;
            bool previousBoolValue = boolValue;

            while (timer < time)
            {
                yield return null;

                if (boolValue != previousBoolValue)
                {
                    previousBoolValue = boolValue;
                }

                timer += Time.deltaTime;
            }

            if (boolValue != previousBoolValue)
            {
                action?.Invoke();
            }
        }
        
        #endregion
        
        #endregion
    }
}