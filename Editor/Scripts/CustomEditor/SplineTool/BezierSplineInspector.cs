// © 2023 Marcello De Bonis. All rights reserved.

using UnityEditor;
using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Editor
{
    // Custom editor for the BezierSpline component
    [CustomEditor(typeof(BezierSpline))]
    public class BezierSplineInspector : UnityEditor.Editor
    {
        private const int lineSteps = 10;
        private const float directionScale = 0.5f;
        private const int stepsPerCurve = 10;
        private const float handleSize = 0.04f;
        private const float pickSize = 0.06f;

        private int selectedIndex = -1;

        private BezierSpline spline;
        private Transform handleTransform;
        private Quaternion handleRotation;

        // Handles the GUI in the Scene view
        private void OnSceneGUI()
        {
            spline = target as BezierSpline;
            handleTransform = spline.transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local
                ? handleTransform.rotation
                : Quaternion.identity;

            Vector3 p0 = ShowPoint(0);
            for (int i = 1; i < spline.ControlPointCount; i += 3)
            {
                Vector3 p1 = ShowPoint(i);
                Vector3 p2 = ShowPoint(i + 1);
                Vector3 p3 = ShowPoint(i + 2);

                Handles.color = Color.red;
                Handles.DrawLine(p0, p1);
                Handles.DrawLine(p2, p3);

                Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
                p0 = p3;
            }

            ShowDirections();
        }

        // Displays a control point and handles interaction
        private Vector3 ShowPoint(int index)
        {
            Vector3 point = handleTransform.TransformPoint(spline.GetControlPoint(index));
            float size = HandleUtility.GetHandleSize(point);
            if (index == 0)
            {
                size *= 2f;
            }

            Handles.color = modeColors[(int)spline.GetControlPointMode(index)];
            if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
            {
                selectedIndex = index;
                Repaint();
            }

            if (selectedIndex == index)
            {
                EditorGUI.BeginChangeCheck();
                point = Handles.DoPositionHandle(point, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(spline, "Move Point");
                    EditorUtility.SetDirty(spline);
                    spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
                }
            }

            // Display label
            string label;
            float fontSize = 12f;
            Color colorFont = Color.white;
            
            if (index % 3 == 0)
            {
                if (index == 0)
                {
                    label = "Start Node Point\n" + point;
                }
                else if (index == spline.ControlPointCount - 1)
                {
                    label = "End Node Point\n" + point;
                }
                else
                {
                    label = "Node Point\n" + point;
                }
                fontSize = 14f;
                colorFont = Color.black;
            }
            else
            {
                if (index == 1)
                {
                    label = "First Control Point\n" + point;
                }
                else
                {
                    label = "Second Control Point\n" + point;
                }
                fontSize = 12f;
                colorFont = Color.white;
            }

            Handles.Label(point, label, new GUIStyle { fontSize = Mathf.RoundToInt(fontSize), normal = {textColor = colorFont}});

            return point;
        }



        // Shows the direction lines along the spline
        private void ShowDirections()
        {
            Handles.color = Color.green;
            Vector3 point = spline.GetPoint(0f);
            Handles.DrawLine(point, point + spline.GetDirection(0f) * directionScale);
            int steps = stepsPerCurve * spline.CurveCount;
            for (int i = 1; i <= steps; i++)
            {
                point = spline.GetPoint(i / (float)steps);
                Handles.DrawLine(point, point + spline.GetDirection(i / (float)steps) * directionScale);
            }
        }

        // Handles the inspector GUI
        public override void OnInspectorGUI()
        {
            spline = target as BezierSpline;
            EditorGUI.BeginChangeCheck();
            bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Toggle Loop");
                EditorUtility.SetDirty(spline);
                spline.Loop = loop;
            }

            if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount)
            {
                DrawSelectedPointInspector();
            }

            if (GUILayout.Button("Add Curve"))
            {
                Undo.RecordObject(spline, "Add Curve");
                spline.AddCurve();
                EditorUtility.SetDirty(spline);
            }
        }

        private static Color[] modeColors =
        {
            Color.white,
            Color.yellow,
            Color.blue,
        };

        // Draws the inspector GUI for the selected control point
        private void DrawSelectedPointInspector()
        {
            GUILayout.Label("Selected Point");
            EditorGUI.BeginChangeCheck();
            Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Move Point");
                EditorUtility.SetDirty(spline);
                spline.SetControlPoint(selectedIndex, point);
            }

            EditorGUI.BeginChangeCheck();
            BezierControlPointMode mode =
                (BezierControlPointMode)EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Change Point Mode");
                spline.SetControlPointMode(selectedIndex, mode);
                EditorUtility.SetDirty(spline);
            }
        }
    }
}
