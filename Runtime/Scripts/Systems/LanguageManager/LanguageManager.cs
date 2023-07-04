using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Runtime
{

    public class LanguageManager : Singleton<LanguageManager>
    {

        // Defines variables and properties
        #region Variables & Properties

            [SerializeField] public S_Language currentLanguage;

        #endregion

        // Defines MonoBehaviour lifecycle events
        #region MonoBehaviour


        #region Activation/Deactivation

            // Called when the object is enabled
            void OnEnable()
            {
                EventManager.OnSetNewLanguage += UpdateCurrentLanguage;
            }

            // Called when the object is disabled
            void OnDisable()
            {
                EventManager.OnSetNewLanguage -= UpdateCurrentLanguage;
            }

            #endregion

            protected override void Awake()
            {
                base.Awake();
                UpdateCurrentLanguage(currentLanguage);
            }

            #endregion

        // Defines methods for the new script
        #region Methods

            private void UpdateCurrentLanguage(S_Language language)
            { 
                currentLanguage = language;
            }

        #endregion

    }
}
