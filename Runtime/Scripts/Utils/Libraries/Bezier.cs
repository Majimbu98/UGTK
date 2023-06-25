// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
// Provides static methods for calculating points and derivatives on a Bezier curve
    public static class Bezier
    {
// Calculates a point on the Bezier curve based on the control points and the parameter t
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t); // Ensure t is clamped between 0 and 1
            float oneMinusT = 1f - t; // Calculate (1 - t)
            // Calculate the point on the curve using the Bezier formula
            return
                oneMinusT * oneMinusT * oneMinusT * p0 +
                3f * oneMinusT * oneMinusT * t * p1 +
                3f * oneMinusT * t * t * p2 +
                t * t * t * p3;
        }

        // Calculates the first derivative of the Bezier curve based on the control points and the parameter t
        public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t); // Ensure t is clamped between 0 and 1
            float oneMinusT = 1f - t; // Calculate (1 - t)

            // Calculate the first derivative of the curve using the Bezier formula
            return
                3f * oneMinusT * oneMinusT * (p1 - p0) +
                6f * oneMinusT * t * (p2 - p1) +
                3f * t * t * (p3 - p2);
        }
    }
}
