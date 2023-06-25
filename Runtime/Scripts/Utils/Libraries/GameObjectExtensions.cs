// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Editor
{
    // Summary:
    // Extension class for GameObject providing additional functionality.
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Checks if the GameObject is already present in the scene with the same list of components.
        /// </summary>
        /// <param name="gameObject">The GameObject to check.</param>
        /// <returns>True if a similar GameObject is found in the scene, otherwise false.</returns>
        public static bool IsInScene(this GameObject gameObject)
        {
            // Get the list of components on the given GameObject
            Component[] gameObjectComponents = gameObject.GetComponents<Component>();

            // Find all GameObjects in the scene
            GameObject[] sceneObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject sceneObject in sceneObjects)
            {
                if (sceneObject == gameObject)
                    continue;

                // Get the list of components on the current scene GameObject
                Component[] sceneComponents = sceneObject.GetComponents<Component>();

                // If the number of components is different, continue to the next scene GameObject
                if (gameObjectComponents.Length != sceneComponents.Length)
                    continue;

                // Check if all components match between the two GameObjects
                bool allComponentsMatch = true;
                for (int i = 0; i < gameObjectComponents.Length; i++)
                {
                    if (gameObjectComponents[i].GetType() != sceneComponents[i].GetType())
                    {
                        allComponentsMatch = false;
                        break;
                    }
                }

                // If all components match, a similar GameObject is found in the scene
                if (allComponentsMatch)
                    return true;
            }

            // No similar GameObject is found in the scene
            return false;
        }
    }
}