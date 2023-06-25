// © 2023 Marcello De Bonis. All rights reserved.

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Runtime
{
    public abstract class S_Container_Data_Scriptable<T, T1> : S_Container_Data_Content<T1> where T: S_Container<T1> where T1: Content<T1>
    {
        
    }
}


/*
 *                      Example of Usage:
 *
 *
 *                      public class Example : Content<Example>
 *                      {
 *                          //Values
 *                      }
 *
 *                      public class S_Container_Example: S_Container<Example>
 *                      {
 *                          //Container of values
 *                      }
 *
 *                      public class S_Container_Data_Scriptable_Example : S_Container_Data_Scriptable<S_Container_Example, Example>
 *                      {
 *                          //Container of the the DATA "Container"
 *                      }
 * 
 */



