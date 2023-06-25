// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public static class AssetLoader
    {
        /// <summary>
        /// Load a ScriptableObject of the specified type from the given file path.
        /// </summary>
        /// <typeparam name="T">The type of ScriptableObject to load.</typeparam>
        /// <param name="filePath">The file path of the ScriptableObject.</param>
        /// <returns>The loaded ScriptableObject instance.</returns>
        public static T LoadScriptableObject<T>(string filePath) where T : ScriptableObject
        {
            // Convert the file path to a resource path
            var resourcePath = PathToResourcePath(filePath);

            // Load the ScriptableObject using Resources.Load
            var asset = Resources.Load<T>(resourcePath);
            return asset;
        }

        /// <summary>
        /// Load a GameObject from the given file path.
        /// </summary>
        /// <param name="filePath">The file path of the GameObject.</param>
        /// <returns>The loaded GameObject instance.</returns>
        public static GameObject LoadGameObject(string filePath)
        {
            // Convert the file path to a resource path
            var resourcePath = PathToResourcePath(filePath);

            // Load the GameObject using Resources.Load
            var prefab = Resources.Load<GameObject>(resourcePath);
            return prefab;
        }

        /// <summary>
        /// Convert a file path to a resource path.
        /// </summary>
        /// <param name="filePath">The file path to convert.</param>
        /// <returns>The converted resource path.</returns>
        private static string PathToResourcePath(string filePath)
        {
            // Find the "Assets" index in the file path
            var assetsIndex = filePath.IndexOf("Assets");

            // Extract the resource path starting from the "Assets" index and remove the file extension.
            var resourcePath = filePath.Substring(assetsIndex);

            // Find the last occurrence of the file extension and remove it
            var extensionIndex = resourcePath.LastIndexOf(".");
            resourcePath = resourcePath.Substring(0, extensionIndex);

            return resourcePath;
        }
    }
}