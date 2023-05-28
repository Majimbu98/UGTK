// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Editor
{

    public class AutoScripter : MonoBehaviour
    {
        [SerializeField]
        private Type objectType;
        
        private string scriptsCategoryFolderPath;
        private string name = "";
        private string parent = "";
        private int intValue = 0;
        private float floatValue = 0.0f;
        private bool boolValue = false;

        [MenuItem("CodeWizard/OpenWindow %w")]
        public static void ShowWindow()
        {
        //    AutoScripter window = EditorWindow.GetWindow<AutoScripter>("AutoScripter");
           // window.Show();
        }


        /*
        private void OnGUI()
        {
            GUILayout.Label("CustomScript", EditorStyles.boldLabel);
            GUILayout.Space(10);
    
            // Visualizza i controlli in base allo stato corrente
            switch (state)
            {
                case EnumClassType.Custom:
                    GUILayout.Label("Name:", EditorStyles.label);
                    name = EditorGUILayout.TextField(name);
    
                    GUILayout.Label("Parent:", EditorStyles.label);
                    parent = EditorGUILayout.TextField(parent);
                    break;
    
                case EnumClassType.Advanced:
                    GUILayout.Label("Int Value:", EditorStyles.label);
                    intValue = EditorGUILayout.IntField(intValue);
    
                    GUILayout.Label("Float Value:", EditorStyles.label);
                    floatValue = EditorGUILayout.FloatField(floatValue);
                    break;
    
                case EnumClassType.Expert:
                    GUILayout.Label("Bool Value:", EditorStyles.label);
                    boolValue = EditorGUILayout.Toggle(boolValue);
                    break;
    
                case EnumClassType.Default:
                default:
                    GUILayout.Label("Select a state to customize the script:", EditorStyles.label);
                    break;
            }
    
            GUILayout.Space(10);
    
            // Visualizza i pulsanti per cambiare lo stato corrente
            if (GUILayout.Button("Default", GUILayout.Width(150), GUILayout.Height(50)))
            {
                state = EnumClassType.Default;
            }
    
            if (GUILayout.Button("Custom"))
            {
                state = EnumClassType.Custom;
            }
    
            if (GUILayout.Button("Advanced"))
            {
                state = EnumClassType.Advanced;
            }
    
            if (GUILayout.Button("Expert"))
            {
                state = EnumClassType.Expert;
            }
    
            GUILayout.Space(10);
    
            // Visualizza il pulsante per creare lo script
            if (GUILayout.Button("Create Script"))
            {
                CreateCustomScript();
            }
        }
    
        private void CreateCustomScript()
        {
            // Costruisce il percorso completo della cartella di destinazione
            string folderPath = Path.Combine(Application.dataPath, "Scripts");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
    
            // Costruisce il percorso completo del file di script
            string scriptPath = Path.Combine(folderPath, name + ".cs");
    
            // Verifica se il file di script esiste già
            if (File.Exists(scriptPath))
            {
                Debug.LogWarning("Il file di script esiste già! Impossibile creare il nuovo script.");
                return;
            }
    
            // Scrive il codice del nuovo script nel file di testo
            using (StreamWriter writer = new StreamWriter(scriptPath))
            {
                writer.WriteLine("using UnityEngine;");
                writer.WriteLine("");
                writer.WriteLine("public class " + name + " : " + parent);
                writer.WriteLine("{");
    
                // Aggiunge le variabili in base allo stato corrente
                switch (state)
                {
                    case EnumClassType.Custom:
                        writer.WriteLine("    public string stringValue;");
                        break;
    
                    case EnumClassType.Advanced:
                        writer.WriteLine("    public int intValue;");
                        writer.WriteLine("    public float floatValue;");
                        break;
    
                    case EnumClassType.Expert:
                        writer.WriteLine("    public bool boolValue;");
                        break;
    
                    case EnumClassType.Default:
                    default:
                        break;
                }
    
                writer.WriteLine("");
                writer.WriteLine("    // Il codice della nuova classe");
                writer.WriteLine("}");
            }
    
            // Importa il file di script nella cartella dei Assets di Unity
            AssetDatabase.ImportAsset(Path.Combine("Scripts", name + ".cs"));
    
            Debug.Log("Il nuovo script è stato creato con successo in Scripts!");
    
            AssetDatabase.Refresh();
        }
    }
    
            */
        
        
        
        
        
        
    }
}



public enum UnityObjectType
{
    AnimationClip,
    AudioClip,
    Material,
    PhysicMaterial,
    Shader,
    Texture2D,
    RenderTexture,
    Mesh,
}

public static class UnityObjectFactory
{
    public static UnityEngine.Object CreateObject(UnityObjectType objectType)
    {
        switch (objectType)
        {
            case UnityObjectType.AnimationClip:
                return new AnimationClip();

            case UnityObjectType.AudioClip:
                return AudioClip.Create("New AudioClip", 44100, 1, 44100, false);

            case UnityObjectType.Material:
                return new Material(Shader.Find("Standard"));

            case UnityObjectType.PhysicMaterial:
                return new PhysicMaterial();

            case UnityObjectType.Shader:
                return Shader.Find("Standard");

            case UnityObjectType.Texture2D:
                return new Texture2D(128, 128);

            case UnityObjectType.RenderTexture:
                return new RenderTexture(128, 128, 24);

            case UnityObjectType.Mesh:
                return new Mesh();
        }

        return null;
    }
}



public class ObjectCreator : MonoBehaviour
{
    public UnityObjectType objectType;

    public void CreateObject()
    {
        // Crea un'istanza dell'oggetto specificato dall'enumerazione "UnityObjectType"
        UnityEngine.Object obj = UnityObjectFactory.CreateObject(objectType);

        // Crea una nuova directory nella cartella "Assets"
        string directoryPath = "Assets/NewObjects";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Crea un nuovo asset dall'oggetto creato e lo salva nella directory appena creata
        string assetPath = $"{directoryPath}/{objectType.ToString()}.asset";
        AssetDatabase.CreateAsset(obj, assetPath);

        // Aggiorna il database degli assets per riflettere le modifiche appena apportate
        AssetDatabase.Refresh();
    }
}