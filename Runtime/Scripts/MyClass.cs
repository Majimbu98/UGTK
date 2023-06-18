using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass : MonoBehaviour
{

// Defines variables and properties
#region Variables & Properties

[SerializeField] public bool boolean1;

[SerializeField] public bool boolean2;

[DrawIf("boolean1", false, E_DisablingType.DontDraw)] [SerializeField] public string ciao;

#endregion

}
