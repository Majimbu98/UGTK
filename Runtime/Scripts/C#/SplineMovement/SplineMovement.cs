using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SplineMovement 
{

// Defines variables and properties
#region Variables & Properties

[Range(0f,1f)]
[SerializeField] public float firstPoint;
[Range(0f,1f)]
[SerializeField] public float secondPoint;
[SerializeField] public float duration;

#endregion


// Defines methods for the new script
#region Methods

public SplineMovement(float _firstPoint, float _secondPoint, float _duration)
{
    _firstPoint = Mathf.Clamp(_firstPoint, 0, 1);
    _secondPoint = Mathf.Clamp(_firstPoint, 0, 1);
    
    firstPoint = _firstPoint;
    secondPoint = _secondPoint;
    duration = _duration;
}

#endregion

}
