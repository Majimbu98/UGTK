// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{

    public interface IPoolable
    {

        // Defines variables and properties
        #region Variables & Properties

        GameObject self { get; set; }
        GameObject parentWhenDeactivated { get; set; }
        GameObject parentWhenActivated { get; set; }

        #endregion

        // Defines methods for the script
        #region Methods

        public void AttachToActivatedParent()
        {
            self.transform.parent = parentWhenActivated.transform;
        }

        public void AttachToDeactivatedParent()
        {
            self.transform.parent = parentWhenDeactivated.transform;
        }

        public bool IsActive()
        {
            return self.activeInHierarchy;
        }

        public void Deactive()
        {
            AttachToDeactivatedParent();
            self.SetActive(false);
        }

        #endregion

    }
}
