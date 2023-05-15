// © 2023 Marcello De Bonis. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a graph data structure.
/// </summary>
/// <typeparam name="T">The type of data stored in the nodes.</typeparam>
[System.Serializable]
public class Graph<T>
{
    #region Variables and Properties

    protected List<Node<T>> nodeList = new List<Node<T>>();

    #endregion

    #region Methods

    #region Constructor

    // ... No need for a constructor comment ...

    #endregion

    #region Add Methods

    /// <summary>
    /// Adds a new node to the graph.
    /// </summary>
    /// <param name="value">The value to be stored in the new node.</param>
    public void AddNode(T value)
    {
        Node<T> node = new Node<T>(value);

        nodeList.Add(node);
    }

    /// <summary>
    /// Adds an edge to the graph using the values of the connected nodes.
    /// </summary>
    /// <param name="value1">The value of the first node.</param>
    /// <param name="value2">The value of the second node.</param>
    /// <param name="weight">The weight of the edge.</param>
    public void AddEdge(T value1, T value2, int weight)
    {
        AddEdge(GetNodeContainsValue(value1), GetNodeContainsValue(value2), weight);
    }

    /// <summary>
    /// Adds an edge to the graph using the provided nodes.
    /// </summary>
    /// <param name="node1">The first node.</param>
    /// <param name="node2">The second node.</param>
    /// <param name="weight">The weight of the edge.</param>
    public void AddEdge(Node<T> node1, Node<T> node2, int weight)
    {
        Edge<T> edge = new Edge<T>(node1, node2, weight);
        node1.AddEdge(edge);
        node2.AddEdge(edge);
    }

    #endregion

    #region Remove Methods

    /// <summary>
    /// Removes an edge between two nodes based on their values.
    /// </summary>
    /// <param name="value1">The value of the first node.</param>
    /// <param name="value2">The value of the second node.</param>
    public void RemoveEdge(T value1, T value2)
    {
        RemoveEdge(GetNodeContainsValue(value1), GetNodeContainsValue(value2));
    }

    /// <summary>
    /// Removes an edge between two nodes based on the provided nodes.
    /// </summary>
    /// <param name="node1">The first node.</param>
    /// <param name="node2">The second node.</param>
    public void RemoveEdge(Node<T> node1, Node<T> node2)
    {
        Edge<T> edgeToDelete = GetEdge(node1, node2);
        if (edgeToDelete != null)
        {
            RemoveEdge(edgeToDelete);
        }
        else
        {
            Debug.Log("Error! Edge not found!");
        }
    }

    /// <summary>
    /// Removes an edge from the graph.
    /// </summary>
    /// <param name="edge">The edge to remove.</param>
    public void RemoveEdge(Edge<T> edge)
    {
        edge.node1.RemoveEdge(edge);
        edge.node2.RemoveEdge(edge);
    }

     /// <summary>
    /// Removes a node from the graph based on its value.
    /// </summary>
    /// <param name="value">The value of the node to remove.</param>
    public void RemoveNode(T value)
    {
        RemoveNode(GetNodeContainsValue(value));
    }

    /// <summary>
    /// Removes a node from the graph.
    /// </summary>
    /// <param name="nodeToDelete">The node to remove.</param>
    public void RemoveNode(Node<T> nodeToDelete)
    {
        foreach (Edge<T> edge in nodeToDelete.edges.ToList())
        {
            RemoveEdge(edge);
        }

        nodeList.Remove(nodeToDelete);
    }

    #endregion

    #region Searching Methods

    /// <summary>
    /// Checks if there is an edge between two nodes based on their values.
    /// </summary>
    /// <param name="value1">The value of the first node.</param>
    /// <param name="value2">The value of the second node.</param>
    /// <returns>True if an edge exists, otherwise false.</returns>
    public bool ExistEdgeBetweenTwoNodes(T value1, T value2)
    {
        return GetEdge(value1, value2) != null;
    }

    /// <summary>
    /// Checks if there is an edge between two nodes based on the provided nodes.
    /// </summary>
    /// <param name="node1">The first node.</param>
    /// <param name="node2">The second node.</param>
    /// <returns>True if an edge exists, otherwise false.</returns>
    public bool ExistEdgeBetweenTwoNodes(Node<T> node1, Node<T> node2)
    {
        return GetEdge(node1, node2) != null;
    }

    /// <summary>
    /// Gets the edge between two nodes based on their values.
    /// </summary>
    /// <param name="value1">The value of the first node.</param>
    /// <param name="value2">The value of the second node.</param>
    /// <returns>The edge between the nodes, or null if not found.</returns>
    public Edge<T> GetEdge(T value1, T value2)
    {
        return GetEdge(GetNodeContainsValue(value1), GetNodeContainsValue(value2));
    }

    /// <summary>
    /// Gets the edge between two nodes based on the provided nodes.
    /// </summary>
    /// <param name="node1">The first node.</param>
    /// <param name="node2">The second node.</param>
    /// <returns>The edge between the nodes, or null if not found.</returns>
    public Edge<T> GetEdge(Node<T> node1, Node<T> node2)
    {
        foreach (Edge<T> edge in node1.edges)
        {
            if ((edge.node1 == node1 && edge.node2 == node2) || (edge.node1 == node2 && edge.node2 == node1))
            {
                return edge;
            }
        }

        return null;
    }

    /// <summary>
    /// Gets the node in the graph that contains the specified value.
    /// </summary>
    /// <param name="value">The value to search for.</param>
    /// <returns>The node with the specified value, or null if not found.</returns>
    public Node<T> GetNodeContainsValue(T value)
    {
        foreach (Node<T> node in nodeList)
        {
            if (node.value.Equals(value))
            {
                return node;
            }
        }

        Debug.Log("Error! Node not found!");

        return null;
    }

    #endregion

    #region Print Functions

    /// <summary>
    /// Performs Breadth-First Search starting from a node specified by its value.
    /// </summary>
    /// <param name="value">The value of the starting node.</param>
    public void Bfs(T value)
    {
        Bfs(GetNodeContainsValue(value));
    }

    /// <summary>
    /// Performs Breadth-First Search starting from a specified node.
    /// </summary>
    /// <param name="startNode">The starting node.</param>
    public void Bfs(Node<T> startNode)
    {
        Queue<Node<T>> queue = new Queue<Node<T>>();
        List<Node<T>> visited = new List<Node<T>>();

        queue.Enqueue(startNode);
        visited.Add(startNode);

        while (queue.Count > 0)
        {
            Node<T> current = queue.Dequeue();

            // Print the current node
            Debug.Log(current.value.ToString());

            foreach (Edge<T> edge in current.edges)
            {
                Node<T> neighbor = edge.node1 == current ? edge.node2 : edge.node1;

                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }
        }
    }

    /// <summary>
    /// Performs Depth-First Search starting from a node specified by its value.
    /// </summary>
    /// <param name="value">The value of the starting node.</param>
    public void DFS(T value)
    {
        DFS(GetNodeContainsValue(value));
    }

    /// <summary>
    /// Performs Depth-First Search starting from a specified node.
    /// </summary>
    /// <param name="startNode">The starting node.</param>
    public void DFS(Node<T> startNode)
    {
        DFS(startNode, new List<Node<T>>());
    }

    /// <summary>
    /// Performs Recursive Depth-First Search starting from a specified node.
    /// </summary>
    /// <param name="startNode">The starting node.</param>
    /// <param name="visited">The list of nodes already visited.</param>
    public void DFS(Node<T> startNode, List<Node<T>> visited)
    {
        visited.Add(startNode);

        // Print the current node
        Debug.Log(startNode.value);

        foreach (Edge<T> edge in startNode.edges)
        {
            Node<T> neighbor = edge.node1 == startNode ? edge.node2 : edge.node1;
            if (!visited.Contains(neighbor))
            {
                DFS(neighbor, visited);
            }
        }
    }

    #endregion

    #region Algorithms

    /// <summary>
    /// Finds the shortest path between two nodes using Dijkstra's algorithm.
    /// </summary>
    /// <param name="startValue">The value of the starting node.</param>
    /// <param name="endValue">The value of the target node.</param>
    /// <returns>The list of nodes representing the shortest path from the start node to the end node.</returns>
    public List<Node<T>> FindShortestPathByDijkstra(T startValue, T endValue)
    {
        // Get the start and end nodes from the graph's nodeList using the startValue and endValue parameters
        Node<T> startNode = GetNodeContainsValue(startValue);
        Node<T> endNode = GetNodeContainsValue(endValue);

        return FindShortestPathByDijkstra(startNode, endNode);
    }

    /// <summary>
    /// Finds the shortest path between two nodes using Dijkstra's algorithm.
    /// </summary>
    /// <param name="startNode">The starting node.</param>
    /// <param name="endNode">The target node.</param>
    /// <returns>The list of nodes representing the shortest path from the start node to the end node.</returns>
    public List<Node<T>> FindShortestPathByDijkstra(Node<T> startNode, Node<T> endNode)
    {
        // Create a dictionary called distances that maps each node in the graph to an integer representing the distance from the start node
        Dictionary<Node<T>, int> distances = new Dictionary<Node<T>, int>();

        // Create a dictionary called previousNodes that maps each node in the graph to the previous node in the shortest path from the start node
        Dictionary<Node<T>, Node<T>> previousNodes = new Dictionary<Node<T>, Node<T>>();

        // Create a list called unvisitedNodes that contains all the nodes in the graph
        List<Node<T>> unvisitedNodes = new List<Node<T>>(nodeList);

        // Initialize the distances and previousNodes dictionaries
        foreach (Node<T> node in unvisitedNodes)
        {
            distances[node] = int.MaxValue;
            previousNodes[node] = null;
        }

        // Set the distance of the start node to 0
        distances[startNode] = 0;

        // Main loop

        // Keep looping until all nodes have been visited
        while (unvisitedNodes.Count > 0)
        {
            // Find the node with the smallest distance
            Node<T> currentNode = unvisitedNodes.OrderBy(node => distances[node]).First();

            // Remove the currentNode from the unvisitedNodes list
            unvisitedNodes.Remove(currentNode);

            // Exit the loop if we have reached the end node
            if (currentNode == endNode)
            {
                break;
            }

            // Update the distances of each neighbor
            foreach (Edge<T> edge in currentNode.edges)
            {
                // Find the neighbor of the currentNode using the edge that connects them
                Node<T> neighbor = edge.node1 == currentNode ? edge.node2 : edge.node1;

                // Calculate the distance to the neighbor through the currentNode
                int distance = distances[currentNode] + edge.weight;

                // If the new distance is smaller than the current distance to the neighbor, update the distance and previousNode dictionaries
                if (distance < distances[neighbor])
                {
                    distances[neighbor] = distance;
                    previousNodes[neighbor] = currentNode;
                }
            }
        }

        // Build the path by starting with the end node and following the previousNode dictionary back to the start node
        List<Node<T>> path = new List<Node<T>>();
        Node<T> currentNodeInPath = endNode;

        while (currentNodeInPath != null)
        {
            // Add the current node to the beginning of the path list
            path.Insert(0, currentNodeInPath);

            // Move to the previous node in the path
            currentNodeInPath = previousNodes[currentNodeInPath];
        }

        // Return the shortest path from the start to the end node
        return path;
    }

    // ... No need for a comment before the A* algorithm ...

    /// <summary>
    /// Finds the shortest path between two nodes using the A* algorithm.
    /// </summary>
    /// <param name="startValue">The value of the starting node.</param>
    /// <param name="endValue">The value of the target node.</param>
    /// <returns>The list of nodes representing the shortest path from the start node to the end node.</returns>
    public List<Node<T>> AStar(T startValue, T endValue)
    {
        // Get the start and end nodes from the graph's nodeList using the startValue and endValue parameters
        Node<T> startNode = GetNodeContainsValue(startValue);
        Node<T> endNode = GetNodeContainsValue(endValue);

        return AStar(startNode, endNode);
    }

    /// <summary>
    /// Finds the shortest path between two nodes using the A* algorithm.
    /// </summary>
    /// <param name="startNode">The starting node.</param>
    /// <param name="endNode">The target node.</param>
    /// <returns>The list of nodes representing the shortest path from the start node to the end node.</returns>
    public List<Node<T>> AStar(Node<T> startNode, Node<T> endNode)
    {
        // Create dictionaries to store gScores and fScores for each node
        Dictionary<Node<T>, int> gScores = new Dictionary<Node<T>, int>();
        Dictionary<Node<T>, int> fScores = new Dictionary<Node<T>, int>();

        // Create a dictionary to store the previous node in the optimal path
        Dictionary<Node<T>, Node<T>> cameFrom = new Dictionary<Node<T>, Node<T>>();

        // Create a list to store the open set of nodes to be evaluated
        List<Node<T>> openSet = new List<Node<T>> { startNode };

        // Initialize gScores and fScores dictionaries for all nodes
        foreach (Node<T> node in nodeList)
        {
            gScores[node] = int.MaxValue;
            fScores[node] = int.MaxValue;
        }

        // Set the gScore of the start node to 0
        gScores[startNode] = 0;

        // Set the fScore of the start node to the estimated cost from start to end node (heuristic cost estimate)
        fScores[startNode] = HeuristicCostEstimate(startNode, endNode);

        // Main loop

        // Keep looping until there are no nodes left to consider
        while (openSet.Count > 0)
        {
            // Find the node in the openSet with the lowest fScore
            Node<T> currentNode = openSet.OrderBy(node => fScores[node]).First();

            // Remove the currentNode from the openSet
            openSet.Remove(currentNode);

            // If the current node is the end node, we have found the optimal path
            if (currentNode == endNode)
            {
                return ReconstructPath(cameFrom, currentNode);
            }

            // Loop through each neighbor of the current node
            foreach (Edge<T> edge in currentNode.edges)
            {
                Node<T> neighbor = edge.node1 == currentNode ? edge.node2 : edge.node1;

                // Skip the neighbor if it is not traversable
                if (!neighbor.IsTraversable())
                {
                    continue;
                }

                // Calculate the tentative gScore for the neighbor
                int tentativeGScore = gScores[currentNode] + edge.weight;

                // If the tentative gScore is lower than the current gScore for the neighbor, update the scores
                if (tentativeGScore < gScores[neighbor])
                {
                    // Update the cameFrom dictionary to record the current node as the previous node for the neighbor
                    cameFrom[neighbor] = currentNode;

                    // Update the gScore for the neighbor
                    gScores[neighbor] = tentativeGScore;

                    // Update the fScore for the neighbor
                    fScores[neighbor] = gScores[neighbor] + HeuristicCostEstimate(neighbor, endNode);

                    // Add the neighbor to the openSet if it's not already in it
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        // If we have looped through all nodes and haven't found the end node, there is no path from the start to the end
        return null;
    }

    /// <summary>
    /// Estimates the heuristic cost between two nodes (in this case, the straight-line distance).
    /// </summary>
    /// <param name="node1">The first node.</param>
    /// <param name="node2">The second node.</param>
    /// <returns>The heuristic cost estimate between the nodes.</returns>
    protected virtual int HeuristicCostEstimate(Node<T> node1, Node<T> node2)
    {
        // Implement your heuristic cost estimation logic here
        return 0;
    }

    /// <summary>
    /// Reconstructs the path from the start node to the given node using the cameFrom dictionary.
    /// </summary>
    /// <param name="cameFrom">The dictionary that maps each node to its previous node in the path.</param>
    /// <param name="currentNode">The current node.</param>
    /// <returns>The list of nodes representing the reconstructed path.</returns>
    private List<Node<T>> ReconstructPath(Dictionary<Node<T>, Node<T>> cameFrom, Node<T> currentNode)
    {
        List<Node<T>> path = new List<Node<T>>();
        path.Add(currentNode);

        while (cameFrom.ContainsKey(currentNode))
        {
            currentNode = cameFrom[currentNode];
            path.Insert(0, currentNode);
        }

        return path;
    }

    // ... No need for a comment before the Utils section ...

    #endregion

    #region Utils

    /// <summary>
    /// Retrieves the farthest nodes from a list of nodes in the graph.
    /// </summary>
    /// <param name="nodes">The list of nodes.</param>
    /// <returns>The list of farthest nodes.</returns>
    public List<Node<T>> GetFarthestNodes(List<Node<T>> nodes)
    {
        // Initialize variables
        List<Node<T>> farthestNodes = new List<Node<T>>();
        Dictionary<Node<T>, int> nodeDistances = new Dictionary<Node<T>, int>();
        Queue<Node<T>> queue = new Queue<Node<T>>();

        // Initialize distances of all nodes to infinity
        foreach (Node<T> node in nodeList)
        {
            nodeDistances[node] = int.MaxValue;
        }

        // Enqueue starting nodes and set their distance to zero
        foreach (Node<T> node in nodes)
        {
            nodeDistances[node] = 0;
            queue.Enqueue(node);
        }

        // Traverse the graph using breadth-first search
        while (queue.Count > 0)
        {
            Node<T> current = queue.Dequeue();

            // Iterate over neighboring nodes
            foreach (Edge<T> edge in current.edges)
            {
                Node<T> neighbor = edge.node1 == current ? edge.node2 : edge.node1;

                // Calculate new distance from starting node to neighboring node
                int newDistance = nodeDistances[current] + edge.weight;

                // Update distance if the new distance is less than the current distance
                if (newDistance < nodeDistances[neighbor] && neighbor.IsTraversable())
                {
                    nodeDistances[neighbor] = newDistance;
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Find nodes with the maximum distance
        int maxDistance = nodeDistances.Values.Max();
        foreach (Node<T> node in nodeList)
        {
            if (nodeDistances[node] == maxDistance && node.IsTraversable())
            {
                farthestNodes.Add(node);
            }
        }

        // Return the list of farthest nodes
        return farthestNodes;
    }

    /// <summary>
    /// Converts a list of nodes into a queue.
    /// </summary>
    /// <param name="list">The list of nodes.</param>
    /// <returns>A queue containing the nodes in the same order as the list.</returns>
    public Queue<Node<T>> ConvertListToQueue(List<Node<T>> list)
    {
        Queue<Node<T>> queue = new Queue<Node<T>>(list);
        return queue;
    }

    /// <summary>
    /// Retrieves the reachable nodes from a starting node within a specified movable cost.
    /// </summary>
    /// <param name="startNode">The starting node.</param>
    /// <param name="movableCost">The maximum cost to move.</param>
    /// <returns>A list of reachable nodes within the movable cost.</returns>
    public List<Node<T>> GetReachableNodesWithinCost(Node<T> startNode, int movableCost)
    {
        List<Node<T>> reachableNodes = new List<Node<T>>();
        Queue<Node<T>> queue = new Queue<Node<T>>();

        // Initialize the queue with the starting node
        queue.Enqueue(startNode);

        // Initialize a dictionary to keep track of the distance to each node from the starting node
        Dictionary<Node<T>, int> distances = new Dictionary<Node<T>, int>();
        distances[startNode] = 0;

        // Keep looping until the queue is empty
        while (queue.Count > 0)
        {
            // Dequeue the next node from the queue
            Node<T> currentNode = queue.Dequeue();

            // Loop through each neighbor of the current node
            foreach (Edge<T> edge in currentNode.edges)
            {
                Node<T> neighbor = edge.node1 == currentNode ? edge.node2 : edge.node1;

                // Calculate the distance to the neighbor from the starting node
                int distanceToNeighbor = distances[currentNode] + edge.weight;

                // If the distance to the neighbor is less than or equal to the movable cost, add it to the reachableNodes list and enqueue it
                if (distanceToNeighbor <= movableCost && !reachableNodes.Contains(neighbor))
                {
                    reachableNodes.Add(neighbor);
                    queue.Enqueue(neighbor);

                    // Update the distance to the neighbor in the distances dictionary
                    distances[neighbor] = distanceToNeighbor;
                }
            }
        }

        // Return the list of reachable nodes
        return reachableNodes;
    }

    /// <summary>
    /// Retrieves the list of nodes in the graph.
    /// </summary>
    /// <returns>A list of nodes in the graph.</returns>
    public List<Node<T>> GetNodeList()
    {
        return nodeList;
    }

    #endregion
    
    #endregion
}



