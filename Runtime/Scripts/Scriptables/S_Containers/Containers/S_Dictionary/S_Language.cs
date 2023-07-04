// © 2023 Marcello De Bonis. All rights reserved.
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{

    [CreateAssetMenu(menuName = "New Custom Scriptable/Language/New Language")]
    public class S_Language : S_Dictionary<S_Container_Data_String, string>
    {

        // Defines variables and properties
        #region Variables & Properties

        [SerializeField] private S_Container_Data_String name;

        #endregion

        // Defines methods for the new script
        #region Methods

        public void SetMainLanguage()
        {
            foreach (var _string in list)
            {
                _string.firstValue.content.defaultValue = _string.secondValue;
                _string.firstValue.content.CloneDefaultIntoCurrent();
            }
        }

        #endregion

    }
}
