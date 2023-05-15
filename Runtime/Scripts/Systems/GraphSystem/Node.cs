// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;

[System.Serializable]
public class Node<T>
{
    #region Variables and properties

    public T value; // The value stored in the node
    public List<Edge<T>> edges = new List<Edge<T>>(); // List of edges connected to the node
    private bool traversable; // Flag indicating if the node is traversable or not
    
    #endregion

    #region Methods

    /// <summary>
    /// Constructor for the Node class.
    /// </summary>
    /// <param name="_value">The value to be stored in the node.</param>
    public Node(T _value)
    {
        value = _value;
        BecomeTraversable(); // By default, a newly created node is set as traversable
    }

    /// <summary>
    /// Adds an edge to the node.
    /// </summary>
    /// <param name="edge">The edge to be added.</param>
    public void AddEdge(Edge<T> edge)
    {
        edges.Add(edge);
    }

    /// <summary>
    /// Removes an edge from the node.
    /// </summary>
    /// <param name="edge">The edge to be removed.</param>
    public void RemoveEdge(Edge<T> edge)
    {
        edges.Remove(edge);
    }

    /// <summary>
    /// Makes the node traversable.
    /// </summary>
    public void BecomeTraversable()
    {
        traversable = true;
    }
    
    /// <summary>
    /// Makes the node untraversable.
    /// </summary>
    public void BecomeUntraversable()
    {
        traversable = false;
    }
    
    /// <summary>
    /// Checks if the node is traversable.
    /// </summary>
    /// <returns>True if the node is traversable, false otherwise.</returns>
    public bool IsTraversable()
    {
        return traversable;
    }
    
    #endregion
    
}