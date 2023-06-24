// © 2023 Marcello De Bonis. All rights reserved.

using UnityEditor;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    [CreateAssetMenu(menuName = "New Custom Scriptable/Spline/New Movement")]
    public class S_SplineMovement : ScriptableObject
    {
        // Defines variables and properties
        #region Variables & Properties

        [SerializeField] public S_SplinePoint firstPoint;
        [SerializeField] public S_SplinePoint secondPoint;
        [SerializeField] public float duration;
        [SerializeField] public S_Spline splineReference;

        #endregion

        // Defines methods for the new script
        #region Methods

            public S_SplineMovement(S_SplinePoint firstPoint, S_SplinePoint secondPoint, S_Spline splineReference)
            {
                this.firstPoint = firstPoint;
                this.secondPoint = secondPoint;
                this.splineReference = splineReference;
            }

        #endregion
        
        
        
    }
    
    public class MyScriptableObjectWindow : EditorWindow
    {
        private S_SplineMovement myScriptableObject;
        private S_SplineMovement otherScriptableObject;

        [MenuItem("Window/Custom ScriptableObject Window")]
        public static void ShowWindow()
        {
            MyScriptableObjectWindow window = GetWindow<MyScriptableObjectWindow>("Custom Window");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Create New ScriptableObject", EditorStyles.boldLabel);

            myScriptableObject = EditorGUILayout.ObjectField("S_SplineMovement", myScriptableObject, typeof(S_SplineMovement), false) as S_SplineMovement;

            otherScriptableObject = EditorGUILayout.ObjectField("S_SplineMovement", otherScriptableObject, typeof(S_SplineMovement), false) as S_SplineMovement;

            if (GUILayout.Button("Create"))
            {
                // Esegui l'azione desiderata quando si fa clic sul pulsante "Create"
                if (myScriptableObject != null && otherScriptableObject != null)
                {
                    // Esegui qui la logica per creare un nuovo ScriptableObject con i collegamenti desiderati
                }
                else
                {
                    Debug.LogWarning("Devi collegare entrambi gli ScriptableObject prima di creare.");
                }
            }
        }
    }
}