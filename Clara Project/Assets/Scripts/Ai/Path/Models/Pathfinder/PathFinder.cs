using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PathFinder 
{
    Node m_startNode;
    Node m_goalNode;

    Graph m_graph;
    GraphView m_graphView;

    PriorityQueue<Node> m_frontierNodes;
    List<Node> m_exploredNodes;
    List<Node> m_pathNodes;

    public Color startColor = Color.green;
    public Color goalColor = Color.blue;
    public Color frontierColor = Color.magenta;
    public Color exploderColor = Color.grey;
    public Color pathColor = Color.cyan;
    public Color arrowColor = new Color(0.85f, 0.85f, 0.85f, 1.0f);
    public Color highlightColor = new Color(1.0f, 1.0f, 0.5f, 1.0f);

    public bool showIterrations = false;
    public bool showCollors = false;
    public bool showArrows = false;
    public bool exitOnGoal = true;

    public bool isComplete = false;
    private int m_iterations = 0;

    public Mode mode = Mode.BreadthFirstSearch;

    public bool Init(Graph graph, GraphView graphView, Node startNode, Node goalNode)
    {

        if (graph == null || startNode == null || goalNode == null) {
            Debug.LogWarning("PathFinder Init error: missing componet(s)!");
            return false;
        }

        if (startNode.nodeType == NodeType.Blocked || goalNode.nodeType == NodeType.Blocked) {
            Debug.LogWarning("PathFinder Init error: start node and goal node must me unblocked!");
            return false;
        }      

        this.m_graph = graph;
        this.m_graphView = graphView;
        this.m_startNode = startNode;
        this.m_goalNode = goalNode;

        //ShowColors();

        this.m_frontierNodes = new PriorityQueue<Node>();
        m_frontierNodes.Enqueue(startNode);

        m_exploredNodes = new List<Node>();
        m_pathNodes = new List<Node>();

        for (int x = 0; x < m_graph.Width; x++) {
            for (int y = 0; y < m_graph.Height; y++) {
                this.m_graph.nodes[x, y].Reset();
            }
        }

        isComplete = false;
        m_iterations = 0;
        m_startNode.distanceTraveled = 0;

        return true;
    }

    private void ShowColors(GraphView graphView, Node startNode, Node goalNode)
    {
        if(graphView == null || startNode == null || goalNode == null) {
            return;
        }

        if(m_frontierNodes != null) {
            graphView.ColorNodes(m_frontierNodes.ToList(), frontierColor);
        }

        if(m_exploredNodes != null) {
            graphView.ColorNodes(m_exploredNodes, exploderColor);
        }

        if(m_pathNodes != null && m_pathNodes.Count > 0) {
            graphView.ColorNodes(m_pathNodes, pathColor);
        }

        NodeView startNodeView = graphView.nodeViews[startNode.xIndex, startNode.yIndex];

        if (startNodeView != null) {
            startNodeView.ColorNode(startColor);
        }

        NodeView goalNodeView = graphView.nodeViews[goalNode.xIndex, goalNode.yIndex];

        if (goalNodeView != null) {
            goalNodeView.ColorNode(goalColor);
        }
    }

    private void ShowColors()
    {
        ShowColors(this.m_graphView, this.m_startNode, this.m_goalNode);
    }

    public IEnumerator SearchRoutine(Action<List<Node>> OnComplete, float timeStep = 0.1f)
    {
        float timeStart = Time.realtimeSinceStartup;

        yield return null;

        while (!isComplete) {
            if (this.m_frontierNodes.Count > 0) {
                Node currentNode = this.m_frontierNodes.Dequeue();
                this.m_iterations++;

                if (!this.m_exploredNodes.Contains(currentNode)) {
                    this.m_exploredNodes.Add(currentNode);
                }

                if (mode == Mode.BreadthFirstSearch) {

                    ExpandFrontierBreadthFirst(currentNode);

                }else if (mode == Mode.AStar) {

                    ExpandFrontierAStar(currentNode);

                } else if (mode == Mode.Dijkstra) {

                    ExpandFrontierDijkstra(currentNode);

                } else {

                    Debug.LogError("PATHFINDER: No search mode selected!");

                }


                if (m_frontierNodes.Contains(m_goalNode)) {
                    m_pathNodes = GetPathNodes(m_goalNode);
                   
                    if (exitOnGoal) {
                        isComplete = true;
                    }

                    if (OnComplete != null)
                    {
                        OnComplete(m_pathNodes);
                    }
                }

                if (showIterrations) {
                    ShowDiagnostics();

                    yield return new WaitForSeconds(timeStep);
                }
            } else {
                isComplete = true;

                if (OnComplete != null)
                {
                    //OnComplete(null);
                }
            }
        }
    }

    private void ShowDiagnostics()
    {
        if (showCollors) {
            ShowColors();
        }


        if (m_graphView != null && showArrows) {
            m_graphView.ShowNodeArrows(m_frontierNodes.ToList(), arrowColor);

            if (m_frontierNodes.Contains(m_goalNode)) {
                m_graphView.ShowNodeArrows(m_pathNodes, highlightColor);
            }
        }
    }

    private void ExpandFrontierBreadthFirst(Node node)
    {
        if(node != null) {
            for(int i=0; i < node.neighbors.Count; i++) {
                if (!this.m_exploredNodes.Contains(node.neighbors[i]) && !this.m_frontierNodes.Contains(node.neighbors[i])) {
                    float distanceToNeighbor = m_graph.GetNodeDistance(node, node.neighbors[i]);
                    float newDistanceTraveled = distanceToNeighbor + node.distanceTraveled;
                    node.neighbors[i].distanceTraveled = newDistanceTraveled;

                    node.neighbors[i].previous = node;
                    node.neighbors[i].priority = m_exploredNodes.Count();
                    this.m_frontierNodes.Enqueue(node.neighbors[i]);
                }
            }
        }
    }

    private void ExpandFrontierDijkstra(Node node)
    {
        if (node != null)
        {
            for (int i = 0; i < node.neighbors.Count; i++) {
                if (!this.m_exploredNodes.Contains(node.neighbors[i])) {

                    float distamnceToNeighbor = m_graph.GetNodeDistance(node, node.neighbors[i]);
                    float newDistanceTraveled = distamnceToNeighbor + node.distanceTraveled;

                    if(float.IsPositiveInfinity(node.neighbors[i].distanceTraveled) || newDistanceTraveled < node.neighbors[i].distanceTraveled) {

                        node.neighbors[i].previous = node;
                        node.neighbors[i].distanceTraveled = newDistanceTraveled;

                    }

                    if (!m_frontierNodes.Contains(node.neighbors[i])) {

                        node.neighbors[i].priority = node.neighbors[i].distanceTraveled;
                        this.m_frontierNodes.Enqueue(node.neighbors[i]);

                    }
                }
            }
        }
    }

    private void ExpandFrontierAStar(Node node)
    {
        if (node != null) {
            for (int i = 0; i < node.neighbors.Count; i++) {
                if (!this.m_exploredNodes.Contains(node.neighbors[i])) {

                    float distamnceToNeighbor = m_graph.GetNodeDistance(node, node.neighbors[i]);
                    float newDistanceTraveled = distamnceToNeighbor + node.distanceTraveled;

                    if (float.IsPositiveInfinity(node.neighbors[i].distanceTraveled) || newDistanceTraveled < node.neighbors[i].distanceTraveled) {

                        node.neighbors[i].previous = node;
                        node.neighbors[i].distanceTraveled = newDistanceTraveled;
                    }

                    if (!m_frontierNodes.Contains(node.neighbors[i]) && m_graph != null) {
                        float distanceToGoal = m_graph.GetNodeDistance(node.neighbors[i], m_goalNode);

                        node.neighbors[i].priority = node.neighbors[i].distanceTraveled + distanceToGoal;

                        this.m_frontierNodes.Enqueue(node.neighbors[i]);
                    }
                }
            }
        }
    }

    List<Node> GetPathNodes(Node endNode)
    {
        List<Node> path = new List<Node>();
        if (endNode == null) {
            return path;
        }

        path.Add(endNode);

        Node currentNode = endNode.previous;

        while (currentNode != null) {

            path.Insert(0, currentNode);
            currentNode = currentNode.previous;
        }

        return path;
    }

    public List<Node> getPath() {
        return this.m_pathNodes;
    }

}
