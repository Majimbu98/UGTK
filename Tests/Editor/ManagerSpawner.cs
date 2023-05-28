using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PrefabSceneUpdater : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn;

    public void UpdateScenes()
    {
        string currentScenePath = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;

        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");
        foreach (string sceneGuid in sceneGuids)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);

            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath, UnityEditor.SceneManagement.OpenSceneMode.Single);

            bool isCurrentScene = scenePath == currentScenePath;

            foreach (GameObject prefab in prefabsToSpawn)
            {
                GameObject[] prefabInstances = GameObject.FindGameObjectsWithTag(prefab.tag);
                foreach (GameObject prefabInstance in prefabInstances)
                {
                    if (prefabInstance != prefab)
                        DestroyImmediate(prefabInstance);
                }
            }

            if (!isCurrentScene)
            {
                UnityEditor.SceneManagement.EditorSceneManager.SaveScene(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
            }
        }

        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(currentScenePath, UnityEditor.SceneManagement.OpenSceneMode.Single);
    }
}

[CustomEditor(typeof(PrefabSceneUpdater))]
public class PrefabSceneUpdaterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PrefabSceneUpdater prefabSceneUpdater = (PrefabSceneUpdater)target;

        if (GUILayout.Button("Esegui"))
        {
            prefabSceneUpdater.UpdateScenes();
        }
    }
}
