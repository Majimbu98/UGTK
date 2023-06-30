// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Runtime
{
    public class SplineWalker : MonoBehaviour
    {
        public BezierSpline spline;
        [Header("Movement Info")] 
        [Range(0f, 1f)] [SerializeField] private float position;
        [SerializeField] public List<SplineMovement> splinePoints;
        [HideInInspector] public SplineMovement currentMovement;
        public bool lookForward;
        private float positionLastFrame=0f;
        private bool isMovementEnabled = true;
        [HideInInspector]
        public bool myBoolean = false;
        [HideInInspector] public int index = -1;

        [DrawIf("myBoolean", true, E_DisablingType.ReadOnly)]
        public bool moving=false;

        private void OnValidate()
        {
            CheckTValue();
        }
        
        private void Awake()
        {
            index = -1;
        }
        
        private void Start()
        {
            
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
            
            if (lookForward)
            {
                Vector3 lookAtPosition;
                if (currentMovement.firstPoint > currentMovement.secondPoint)
                {
                    lookAtPosition = spline.GetPoint(position + 0.01f);
                }
                else
                {
                    lookAtPosition = spline.GetPoint(position + 0.01f);
                }
                
                transform.LookAt(lookAtPosition);
            }

            if (splinePoints.Count == 0)
            {
                splinePoints.Add(new SplineMovement(0, 1, 10));
                currentMovement = splinePoints[0];
                index = -1;
            }

            positionLastFrame = position;

#if UNITY_EDITOR
            UnityEditor.EditorWindow view = UnityEditor.EditorWindow.GetWindow(typeof(UnityEditor.EditorWindow));
            view.Repaint();
#endif
        }

        public bool OthersPoints()
        {
            return (currentMovement != splinePoints.Last());
        }
        
        public void MoveOnCurrent()
        {
            StartCoroutine(MoveOnCurrentMovement());
        }
        
        public void MoveOnNext()
        {
            StartCoroutine(MoveOnNextMovement());
        }
        
        public void MoveOnPrevious()
        {
            StartCoroutine(MoveOnPreviousMovement());
        }
        
        private IEnumerator MoveOnNextMovement()
        {
            index++;
            yield return (Move(splinePoints[index]));
        }

        private IEnumerator MoveOnPreviousMovement()
        {
            index--;
            yield return (Move(splinePoints[index]));
        }

        private IEnumerator MoveOnCurrentMovement()
        {
            yield return (Move(splinePoints[index]));
        }

        private IEnumerator Move(SplineMovement _splinePoints)
        {
            moving = true;
            
            currentMovement = _splinePoints;
            
            float myPos = _splinePoints.firstPoint;
            float endPos = _splinePoints.secondPoint;
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

            moving = false;
        }
    }
}
