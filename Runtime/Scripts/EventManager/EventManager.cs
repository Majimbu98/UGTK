// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{

    public static class EventManager
    {

        // Defines variables and properties
        #region Variables & Properties

        public static Action<float, Action> OnEndTimeDoAction;
        public static Action<float, bool, Action, Action, Action> OnEndTimeDoActionElseDoOtherActionElseBoolChangeDoOtherAction;

        #endregion

        // Defines methods for the new script

        #region Methods

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Application.quitting += Shutdown;
        }

        private static void Shutdown()
        {
            
        }

        #endregion

    }

}