// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public class UiTextController : MonoBehaviour
    {

        // Defines variables and properties

        #region Variables & Properties

        [SerializeField] private List<BiClass<ScriptableObject, TextMeshProUGUI>> textList;

        #endregion

        // Defines MonoBehaviour lifecycle events

        #region MonoBehaviour

        // Called once per frame
        void Update()
        {
            UpgradeTextes();
        }

        private void OnValidate()
        {
            CheckListDuringInspectorMode();
        }

        private void CheckListDuringInspectorMode()
        {
            if (textList.Count > 0)
            {
                foreach (BiClass<ScriptableObject, TextMeshProUGUI> element in textList)
                {
                    if (element.firstValue != null)
                    {
                        ScriptableObject scriptableObject = element.firstValue;

                        if (!IsObject<bool>(scriptableObject) && !IsObject<int>(scriptableObject) &&
                            !IsObject<float>(scriptableObject) && !IsObject<string>(scriptableObject) &&
                            !IsObject<Vector2>(scriptableObject))
                        {
                            element.firstValue = null;
                            ConsoleUtility.LogColored("Error, link on " + this.gameObject.name + " only Scriptable Data containers with primitive variables", Color.blue);
                        }
                    }
                }
            }
        }

        #endregion

        // Defines methods for the new script

        #region Methods

        private void UpgradeTextes()
        {
            foreach (BiClass<ScriptableObject, TextMeshProUGUI> element in textList)
            {
                ScriptableObject scriptableObject = element.firstValue;
                TextMeshProUGUI text = element.secondValue;

                SetText<bool>(scriptableObject, text);
                SetText<int>(scriptableObject, text);
                SetText<float>(scriptableObject, text);
                SetText<string>(scriptableObject, text);
                SetText<Vector2>(scriptableObject, text);
                SetText<Vector3>(scriptableObject, text);
            }
        }

        private void SetText<T>(ScriptableObject scriptableObject, TextMeshProUGUI text)
        {
            if (IsObject<T>(scriptableObject))
            {
                text.text = ((S_Container_Data<T>)scriptableObject).content.currentValue.ToString();
            }
        }

        private bool IsObject<T>(ScriptableObject scriptableObject)
        {
            S_Container_Data<T> myNewScriptableObject = scriptableObject as S_Container_Data<T>;
            return (myNewScriptableObject != null);
        }

        #endregion

    }
}




