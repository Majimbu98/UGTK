// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public static class ComponentUtility
    {
        public static bool CompareComponentLists(GameObject gameObject1, GameObject gameObject2)
        {
            List<Component> components1 = GetComponentsInGameObject(gameObject1);
            List<Component> components2 = GetComponentsInGameObject(gameObject2);

            if (components1.Count != components2.Count)
            {
                return false;
            }

            HashSet<System.Type> componentTypes1 = new HashSet<System.Type>();

            foreach (Component component in components1)
            {
                componentTypes1.Add(component.GetType());
            }

            foreach (Component component in components2)
            {
                if (!componentTypes1.Contains(component.GetType()))
                {
                    return false;
                }
            }

            return true;
        }

        private static List<Component> GetComponentsInGameObject(GameObject gameObject)
        {
            List<Component> components = new List<Component>();

            if (gameObject != null)
            {
                components.AddRange(gameObject.GetComponents<Component>());
            }

            return components;
        }
    }
}