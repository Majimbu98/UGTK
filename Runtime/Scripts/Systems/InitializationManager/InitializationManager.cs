using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Runtime
{
    public class InitializationManager : Singleton<InitializationManager>
    {

        // Defines variables and properties
        #region Variables & Properties

        [SerializeField] private List<IExecutableOnPlay> executableList;

        #endregion

        // Defines MonoBehaviour lifecycle events
        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();
            InitExecutableList();
        }

        #endregion

        // Defines methods for the new script
        #region Methods

        private void NewExecutableElement(IExecutableOnPlay element)
        {
            executableList.Add(element);
        }
        
        private void InitExecutableList()
        {
            
            
            foreach (var executableElement in executableList)
            {
                if (executableElement != null)
                {
                    executableElement.OnPlay();
                }
            }
        }

        #endregion

    }
}
