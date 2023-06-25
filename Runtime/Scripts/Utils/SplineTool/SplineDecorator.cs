// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
// Decorates a spline with items along its path
    public class SplineDecorator : MonoBehaviour
    {
        public BezierSpline spline; // Reference to the spline
        public int frequency; // Frequency of items along the spline

        public bool lookForward; // Flag to indicate if items should look forward
        public Transform[] items; // Array of items to be placed along the spline

        private void Awake()
        {
            // Check if the frequency, items, or items length is invalid
            if (frequency <= 0 || items == null || items.Length == 0)
            {
                return; // Exit early if any of the conditions are true
            }

            // Calculate the step size based on the frequency and number of items
            float stepSize = frequency * items.Length;

            // Adjust the step size if the spline is a loop or has only one step
            if (spline.Loop || stepSize == 1)
            {
                stepSize = 1f / stepSize;
            }
            else
            {
                stepSize = 1f / (stepSize - 1);
            }

            // Iterate over the frequency and items arrays to instantiate and position the items along the spline
            for (int f = 0, p = 0; f < frequency; f++)
            {
                for (int i = 0; i < items.Length; i++, p++)
                {
                    Transform item = Instantiate(items[i]) as Transform; // Instantiate the item
                    Vector3 position = spline.GetPoint(p * stepSize); // Get the position on the spline
                    item.transform.localPosition = position; // Set the item's local position
                    if (lookForward)
                    {
                        // If lookForward is enabled, make the item look at the next point on the spline
                        item.transform.LookAt(position + spline.GetDirection(p * stepSize));
                    }

                    item.transform.parent = transform; // Set the item's parent to this decorator
                }
            }
        }
    }
}