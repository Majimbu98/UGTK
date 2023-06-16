using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public class MyClass : MonoBehaviour
    {

        // Defines variables and properties

        #region Variables & Properties

        [SerializeField] public bool press = true;

        [SerializeField] private int hello;

      //  [ShowIf(nameof(Funct), E_DisablingType.DONTSHOW)]
        public string ciao;


        #endregion

        // Defines methods for the new script

        #region Methods

        public bool Funct()
        {
            return (hello>7);
        }

        #endregion

    }
}
