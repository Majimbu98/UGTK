// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections;
using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    // Controls the movement of an object along a Bezier spline
    public class SplineWalker : MonoBehaviour
    {
        public BezierSpline spline; // Reference to the spline
        public float duration; // Duration of the movement along the spline
        public bool lookForward; // Flag to indicate if the object should look forward

        public E_SplineWalkerMode mode; // Mode of the spline walker
        private bool isMovementEnabled = true; // Flag to indicate if movement is enabled

        private void Start()
        {
            StartCoroutine(StartMovementCoroutine());
        }

        private IEnumerator StartMovementCoroutine()
        {
            while (isMovementEnabled)
            {
                yield return StartCoroutine(MoveAlongSplineCoroutine());
            }
        }

        private IEnumerator MoveAlongSplineCoroutine()
        {
            float progress = 0f; // Current progress along the spline
            bool goingForward = true; // Flag to indicate the direction of movement

            while (progress >= 0f && progress <= 1f)
            {
                // Update the progress based on the direction and time
                float deltaTime = Time.deltaTime;
                if (!goingForward)
                {
                    deltaTime = -deltaTime;
                }
                progress += deltaTime / duration;

                // Check if the progress has reached the end of the spline
                if (progress > 1f)
                {
                    if (mode == E_SplineWalkerMode.AllPoints)
                    {
                        progress = 0f; // Reset the progress to the beginning of the spline if the mode is AllPoints
                    }
                    else if (mode == E_SplineWalkerMode.Loop)
                    {
                        progress -= 1f; // Wrap the progress back to the beginning if the mode is Loop
                    }
                    else if (mode == E_SplineWalkerMode.LoopPingPong)
                    {
                        progress = 2f - progress; // Reverse the progress if the mode is LoopPingPong
                        goingForward = false; // Set the direction to backward
                    }
                }
                else if (progress < 0f)
                {
                    progress = -progress; // Reverse the progress
                    goingForward = true; // Set the direction to forward
                }

                // Get the position on the spline based on the current progress
                Vector3 position;
                if (mode == E_SplineWalkerMode.NextCurvePoint)
                {
                    float nextProgress = Mathf.Clamp01(progress + 0.01f);
                    position = spline.GetPoint(nextProgress); // Move towards the next point on the curve
                }
                else if (mode == E_SplineWalkerMode.PreviousCurvePoint)
                {
                    float previousProgress = Mathf.Clamp01(progress - 0.01f);
                    position = spline.GetPoint(previousProgress); // Move towards the previous point on the curve
                }
                else
                {
                    position = spline.GetPoint(progress);
                }

                transform.localPosition = position; // Set the object's local position to the calculated position

                // Make the object look at the next point on the spline if lookForward is enabled
                if (lookForward)
                {
                    Vector3 lookAtPosition;
                    if (mode == E_SplineWalkerMode.NextCurvePoint)
                    {
                        float nextProgress = Mathf.Clamp01(progress + 0.01f);
                        lookAtPosition = position + spline.GetDirection(nextProgress); // Look towards the next point on the curve
                    }
                    else if (mode == E_SplineWalkerMode.PreviousCurvePoint)
                    {
                        float previousProgress = Mathf.Clamp01(progress - 0.01f);
                        lookAtPosition = position + spline.GetDirection(previousProgress); // Look towards the previous point on the curve
                    }
                    else
                    {
                        lookAtPosition = position + spline.GetDirection(progress);
                    }

                    transform.LookAt(lookAtPosition);
                }

                yield return null;
            }
        }

        public void EnableMovement()
        {
            isMovementEnabled = true;
            StartCoroutine(StartMovementCoroutine());
        }

        public void DisableMovement()
        {
            isMovementEnabled = false;
        }
    }
}
