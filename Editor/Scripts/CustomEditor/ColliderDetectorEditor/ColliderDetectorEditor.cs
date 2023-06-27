// © 2023 Marcello De Bonis. All rights reserved.

using UnityEditor;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Editor
{

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ColliderDetector), true)]
    public class ColliderDetectorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            SerializedProperty typeCollision = serializedObject.FindProperty("typeCollision");
            EditorGUILayout.PropertyField(typeCollision);
            
            E_VerifyCollision type = (E_VerifyCollision)typeCollision.enumValueIndex;

            SerializedProperty gameObjectReference= serializedObject.FindProperty("gameObjectReference");

            bool drawEvents=false;
            
            switch (type)
            {
                case E_VerifyCollision.ObjectComponents:
                    
                    EditorGUILayout.PropertyField(gameObjectReference);

                    if (gameObjectReference.objectReferenceValue != null)
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        SerializedProperty componentList= serializedObject.FindProperty("componentList");
                        EditorGUILayout.PropertyField(componentList);
                        EditorGUI.EndDisabledGroup();
                    }

                    break;
                case E_VerifyCollision.DirectObject:
                    
                    EditorGUILayout.PropertyField(gameObjectReference);
                    
                    break;
                case E_VerifyCollision.Tag:

                    SerializedProperty tag= serializedObject.FindProperty("tag");
                    EditorGUILayout.PropertyField(tag);

                    if (!string.IsNullOrEmpty(tag.stringValue))
                    {
                        drawEvents = true;
                    }

                    break;
                case E_VerifyCollision.EveryObject:

                    //DON'T DRAW NOTHING
                    
                    break;
            }

            if (gameObjectReference.objectReferenceValue != null)
            {
                drawEvents = true;
            }

            if (drawEvents)
            {
                SerializedProperty onCollisionEnter = serializedObject.FindProperty("onCollisionEnter");
                EditorGUILayout.PropertyField(onCollisionEnter);
            
                SerializedProperty onColliding = serializedObject.FindProperty("onCollisionStay");
                EditorGUILayout.PropertyField(onColliding);
            
                SerializedProperty onCollisionExit = serializedObject.FindProperty("onCollisionExit");
                EditorGUILayout.PropertyField(onCollisionExit);
            }

            serializedObject.ApplyModifiedProperties();
            
        }
    }

#endif
    
}