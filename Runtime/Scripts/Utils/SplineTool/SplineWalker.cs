// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Collections;
using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Runtime
{
    public class SplineWalker : MonoBehaviour
    {
        public BezierSpline spline; // Reference to the spline

        [Header("Movement Info")] public S_SplineMovement splinePoints;
        public bool lookForward; // Flag to indicate if the object should look forward
        public E_SplineWalkerMode mode; // Mode of the spline walker
        
        private float positionLastFrame=0f;

        [Range(0f, 1f)] [SerializeField] private float position;

        private bool isMovementEnabled = true; // Flag to indicate if movement is enabled
        private int currentIndex = 0; // Index of the current control point


        private void OnValidate()
        {
            CheckTValue();
        }

        private void Update()
        {
            CheckTValue();
        }

        private void CheckTValue()
        {
            if (position != positionLastFrame)
            {
                transform.position = spline.GetPoint(position);
            }
            
            if(lookForward)

            positionLastFrame = position;
        }

        private void Start()
        {
            MoveToNextPoint();
        }

        private void MoveToNextPoint()
        {
            StartCoroutine(Move(splinePoints));
        }

        private void MoveToPreviousPoint()
        {
            
        }

        private IEnumerator Move(S_SplineMovement _splinePoints)
        {
            float myPos = _splinePoints.firstPoint.value;
            float endPos = _splinePoints.secondPoint.value;
            float duration = _splinePoints.duration;
            float elapsedTime = 0f;
            
            while (elapsedTime<duration)
            {
                float currentValue = Mathf.Lerp(myPos, endPos, elapsedTime / duration);
                
                elapsedTime += Time.deltaTime;

                position = currentValue;

                yield return null;
            }

            position = endPos;
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
                // Check if the progress has reached the end of the spline
                if (progress > 1f)
                {
                    if (mode == E_SplineWalkerMode.AllPoints)
                    {
                        progress = 0f; // Reset the progress to the beginning of the spline if the mode is AllPoints
                        currentIndex = 0; // Reset the current control point index
                    }
                    else if (mode == E_SplineWalkerMode.Loop)
                    {
                        progress -= 1f; // Wrap the progress back to the beginning if the mode is Loop
                        currentIndex = 0; // Reset the current control point index
                    }
                    else if (mode == E_SplineWalkerMode.LoopPingPong)
                    {
                        progress = 2f - progress; // Reverse the progress if the mode is LoopPingPong
                        goingForward = false; // Set the direction to backward
                        currentIndex = spline.ControlPointCount - 1; // Set the current control point index to the last point
                    }
                }
                else if (progress < 0f)
                {
                    progress = -progress; // Reverse the progress
                    goingForward = true; // Set the direction to forward
                    currentIndex = 0; // Reset the current control point index
                }

                // Get the position on the spline based on the current progress
                Vector3 position;
                if (goingForward)
                {
                    position = spline.GetPoint(progress); // Move towards the next point on the curve
                }
                else
                {
                    position = spline.GetPoint(1f - progress); // Move towards the previous point on the curve
                }

                transform.localPosition = position; // Set the object's local position to the calculated position

                // Make the object look at the next point on the spline if lookForward is enabled
                if (lookForward)
                {
                    Vector3 lookAtPosition;
                    if (goingForward)
                    {
                        lookAtPosition = spline.GetPoint(progress + 0.01f); // Look towards the next point on the curve
                    }
                    else
                    {
                        lookAtPosition =spline.GetPoint(1f - (progress + 0.01f)); // Look towards the previous point on the curve
                    }
                    transform.LookAt(lookAtPosition); // Make the object look at the calculated position
                }

                yield return null; // Wait for the next frame
            }
        }

        // Stop the movement of the object along the spline
        public void StopMovement()
        {
            isMovementEnabled = false;
        }
    }
}
