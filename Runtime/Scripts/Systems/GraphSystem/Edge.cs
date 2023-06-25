// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an edge between two nodes in a graph.
/// </summary>
/// <typeparam name="T">The type of data stored in the nodes.</typeparam>
[System.Serializable]
public class Edge<T>
{
    #region Variables and Properties

    /// <summary>
    /// The first node connected by the edge.
    /// </summary>
    public Node<T> node1;

    /// <summary>
    /// The second node connected by the edge.
    /// </summary>
    public Node<T> node2;

    /// <summary>
    /// The weight of the edge.
    /// </summary>
    public int weight;

    #endregion

    #region Methods

    /// <summary>
    /// Initializes a new instance of the Edge class.
    /// </summary>
    /// <param name="_node1">The first node connected by the edge.</param>
    /// <param name="_node2">The second node connected by the edge.</param>
    /// <param name="_weight">The weight of the edge.</param>
    public Edge(Node<T> _node1, Node<T> _node2, int _weight)
    {
        weight = _weight;
        node1 = _node1;
        node2 = _node2;
    }

    #endregion
}