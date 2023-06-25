// © 2023 Marcello De Bonis. All rights reserved.

using System;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Represents a Bezier spline in 3D space.
    /// </summary>
    public class BezierSpline : MonoBehaviour
    {
        /// <summary>
        /// The control points of the spline.
        /// </summary>
        public Vector3[] points;

        /// <summary>
        /// The control point modes of the spline.
        /// </summary>
        [SerializeField] private BezierControlPointMode[] modes;

        /// <summary>
        /// Determines if the spline should loop back to the start.
        /// </summary>
        [SerializeField] private bool loop;

        /// <summary>
        /// Gets or sets a value indicating whether the spline should loop back to the start.
        /// </summary>
        public bool Loop
        {
            get { return loop; }
            set
            {
                loop = value;
                if (value == true)
                {
                    modes[modes.Length - 1] = modes[0];
                    SetControlPoint(0, points[0]);
                }
            }
        }

        /// <summary>
        /// Gets the number of control points in the spline.
        /// </summary>
        public int ControlPointCount => points.Length;

        /// <summary>
        /// Gets the number of curves in the spline.
        /// </summary>
        public int CurveCount => (points.Length - 1) / 3;

        /// <summary>
        /// Initializes the spline with default control points and control point modes.
        /// </summary>
        public void Reset()
        {
            points = new Vector3[]
            {
                new Vector3(1f, 0f, 0f),
                new Vector3(2f, 0f, 0f),
                new Vector3(3f, 0f, 0f),
                new Vector3(4f, 0f, 0f)
            };
            modes = new BezierControlPointMode[]
            {
                BezierControlPointMode.Free,
                BezierControlPointMode.Free
            };
        }

        /// <summary>
        /// Gets the position on the spline at the given time.
        /// </summary>
        /// <param name="t">The time parameter (0 to 1).</param>
        /// <returns>The position on the spline.</returns>
        public Vector3 GetPoint(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = points.Length - 4;
            }
            else
            {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int)t;
                t -= i;
                i *= 3;
            }

            return transform.TransformPoint(Bezier.GetPoint(
                points[i], points[i + 1], points[i + 2], points[i + 3], t));
        }

        /// <summary>
        /// Gets the direction on the spline at the given time.
        /// </summary>
        /// <param name="t">The time parameter (0 to 1).</param>
        /// <returns>The direction on the spline.</returns>
        public Vector3 GetDirection(float t)
        {
            return GetVelocity(t).normalized;
        }

        /// <summary>
        /// Gets the velocity on the spline at the given time.
        /// </summary>
        /// <param name="t">The time parameter (0 to 1).</param>
        /// <returns>The velocity on the spline.</returns>
        public Vector3 GetVelocity(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = points.Length - 4;
            }
            else
            {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int)t;
                t -= i;
                i *= 3;
            }

            return transform.TransformPoint(Bezier.GetFirstDerivative(
                points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
        }

        /// <summary>
        /// Gets the control point at the specified index.
        /// </summary>
        /// <param name="index">The index of the control point.</param>
        /// <returns>The control point.</returns>
        public Vector3 GetControlPoint(int index)
        {
            return points[index];
        }

        /// <summary>
        /// Sets the control point at the specified index.
        /// </summary>
        /// <param name="index">The index of the control point.</param>
        /// <param name="point">The new position of the control point.</param>
        public void SetControlPoint(int index, Vector3 point)
        {
            if (index % 3 == 0)
            {
                Vector3 delta = point - points[index];
                if (loop)
                {
                    if (index == 0)
                    {
                        points[1] += delta;
                        points[points.Length - 2] += delta;
                        points[points.Length - 1] = point;
                    }
                    else if (index == points.Length - 1)
                    {
                        points[0] = point;
                        points[1] += delta;
                        points[index - 1] += delta;
                    }
                    else
                    {
                        points[index - 1] += delta;
                        points[index + 1] += delta;
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        points[index - 1] += delta;
                    }

                    if (index + 1 < points.Length)
                    {
                        points[index + 1] += delta;
                    }
                }
            }

            points[index] = point;
            EnforceMode(index);
        }

        /// <summary>
        /// Gets the control point mode at the specified index.
        /// </summary>
        /// <param name="index">The index of the control point.</param>
        /// <returns>The control point mode.</returns>
        public BezierControlPointMode GetControlPointMode(int index)
        {
            return modes[(index + 1) / 3];
        }

        /// <summary>
        /// Sets the control point mode at the specified index.
        /// </summary>
        /// <param name="index">The index of the control point.</param>
        /// <param name="mode">The new control point mode.</param>
        public void SetControlPointMode(int index, BezierControlPointMode mode)
        {
            int modeIndex = (index + 1) / 3;
            modes[modeIndex] = mode;
            if (loop)
            {
                if (modeIndex == 0)
                {
                    modes[modes.Length - 1] = mode;
                }
                else if (modeIndex == modes.Length - 1)
                {
                    modes[0] = mode;
                }
            }

            EnforceMode(index);
        }

        /// <summary>
        /// Adds a new curve to the spline.
        /// </summary>
        public void AddCurve()
        {
            Vector3 point = points[points.Length - 1];
            Array.Resize(ref points, points.Length + 3);
            point.x += 1f;
            points[points.Length - 3] = point;
            point.x += 1f;
            points[points.Length - 2] = point;
            point.x += 1f;
            points[points.Length - 1] = point;

            Array.Resize(ref modes, modes.Length + 1);
            modes[modes.Length - 1] = modes[modes.Length - 2];
            EnforceMode(points.Length - 4);

            if (loop)
            {
                points[points.Length - 1] = points[0];
                modes[modes.Length - 1] = modes[0];
                EnforceMode(0);
            }
        }

        /// <summary>
        /// Enforces the control point mode at the specified index by adjusting the tangents.
        /// </summary>
        /// <param name="index">The index of the control point.</param>
        private void EnforceMode(int index)
        {
            int modeIndex = (index + 1) / 3;
            BezierControlPointMode mode = modes[modeIndex];
            if (mode == BezierControlPointMode.Free || !loop && (modeIndex == 0 || modeIndex == modes.Length - 1))
            {
                return;
            }

            int middleIndex = modeIndex * 3;
            int fixedIndex, enforcedIndex;
            if (index <= middleIndex)
            {
                fixedIndex = middleIndex - 1;
                if (fixedIndex < 0)
                {
                    fixedIndex = points.Length - 2;
                }

                enforcedIndex = middleIndex + 1;
                if (enforcedIndex >= points.Length)
                {
                    enforcedIndex = 1;
                }
            }
            else
            {
                fixedIndex = middleIndex + 1;
                if (fixedIndex >= points.Length)
                {
                    fixedIndex = 1;
                }

                enforcedIndex = middleIndex - 1;
                if (enforcedIndex < 0)
                {
                    enforcedIndex = points.Length - 2;
                }
            }

            Vector3 middle = points[middleIndex];
            Vector3 enforcedTangent = middle - points[fixedIndex];
            if (mode == BezierControlPointMode.Aligned)
            {
                enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
            }

            points[enforcedIndex] = middle + enforcedTangent;
        }
    }
}