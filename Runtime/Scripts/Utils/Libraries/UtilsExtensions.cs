using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilExtensions
{
    public static bool CheckPercentage(this float percentage)
    {
        float randomValue = Random.Range(0f, 100f); // Genera un valore casuale tra 0 e 100
        return randomValue <= percentage; // Restituisce true se il valore casuale è inferiore o uguale alla percentuale, false altrimenti
    }
    
    public static List<GameObject> GetAllChildren(GameObject gameObject) 
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in gameObject.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }
}
