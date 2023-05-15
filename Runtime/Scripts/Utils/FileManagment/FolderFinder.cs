// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using System;
using System.IO;
using System.Linq;

namespace UnityGamesToolkit.Runtime
{

    public static class FolderFinder
    {
        /// <summary>
        /// Get the script of the specified type from the given folder path.
        /// </summary>
        /// <typeparam name="T">The type of script to search for.</typeparam>
        /// <param name="folderPath">The path of the folder to search in.</param>
        /// <param name="scriptName">The name of the script to retrieve.</param>
        /// <returns>The script of the specified type from the given folder path.</returns>
        public static T GetScriptOfTypeInFolder<T>(string folderPath, string scriptName) where T : UnityEngine.Object
        {
            string[] scriptFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);
            string scriptFileName = $"{scriptName}.cs";
            string scriptFilePath = scriptFiles.FirstOrDefault(file =>
                Path.GetFileName(file).Equals(scriptFileName, StringComparison.OrdinalIgnoreCase));

            if (scriptFilePath == null)
            {
                throw new FileNotFoundException($"Script '{scriptName}' not found in folder '{folderPath}'.");
            }

            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(scriptFilePath);
        }

        /// <summary>
        /// Get all scripts of the specified type from the given folder path.
        /// </summary>
        /// <typeparam name="T">The type of scripts to search for.</typeparam>
        /// <param name="folderPath">The path of the folder to search in.</param>
        /// <returns>An array of scripts of the specified type found in the folder.</returns>
        public static T[] GetAllScriptsOfTypeInFolder<T>(string folderPath) where T : UnityEngine.Object
        {
            string[] scriptFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);

            T[] scripts = scriptFiles
                .Select(file => UnityEditor.AssetDatabase.LoadAssetAtPath<T>(file))
                .Where(script => script != null)
                .ToArray();

            return scripts;
        }

        /// <summary>
        /// Get ScriptableObjects of the specified type from the given folder path.
        /// </summary>
        /// <typeparam name="T">The type of ScriptableObject to search for.</typeparam>
        /// <param name="folderPath">The path of the folder to search in.</param>
        /// <returns>An array of ScriptableObject instances found in the folder.</returns>
        public static T[] GetScriptableObjectsOfType<T>(string folderPath) where T : ScriptableObject
        {
            // Get all .asset files in the specified folder and its subdirectories
            var scriptableObjectFiles = Directory.GetFiles(folderPath, "*.asset", SearchOption.AllDirectories);

            // Load the ScriptableObjects using Resources.Load for each file path
            var scriptableObjects = scriptableObjectFiles
                .Select(filePath => AssetLoader.LoadScriptableObject<T>(filePath))
                .Where(obj => obj != null)
                .ToArray();

            return scriptableObjects;
        }

        /// <summary>
        /// Get GameObjects from the given folder path.
        /// </summary>
        /// <param name="folderPath">The path of the folder to search in.</param>
        /// <returns>An array of GameObject instances found in the folder.</returns>
        public static GameObject[] GetGameObjectsInFolder(string folderPath)
        {
            // Get all .prefab files in the specified folder and its subdirectories
            var gameObjectFiles = Directory.GetFiles(folderPath, "*.prefab", SearchOption.AllDirectories);

            // Load the GameObjects using Resources.Load for each file path
            var gameObjects = gameObjectFiles
                .Select(filePath => AssetLoader.LoadGameObject(filePath))
                .Where(obj => obj != null)
                .ToArray();

            return gameObjects;
        }
    }
}
