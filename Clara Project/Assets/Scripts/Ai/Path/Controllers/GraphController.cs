using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour 
{

    // Use this for initialization
    public MapData mapData;
    public Graph graph;

    public bool showGraphVisuals = false;

    PathFinder pathFinder;
    //public PathFinder pathFinder;

    int startX = 0;
    int startY = 0;

    int goalX = 0;
    int goalY = 0;

    public float timeStep = 0.1f;

    private void Awake()
    {
        this.startX = 0;
        this.startY = 0;

        this.goalX = 0;
        this.goalY = 0;

        pathFinder = new PathFinder();
        pathFinder.mode = Mode.Dijkstra;
    }

    private void StartSearch(Action<List<Node>> OnComplete)
    {
        if (mapData != null && graph != null)
        {
            int[,] mapInstance = mapData.MakeMap();
            graph.Init(mapInstance);

            GraphView graphView = graph.gameObject.GetComponent<GraphView>();

            if (graphView != null && showGraphVisuals)
            {
                graphView.Init(graph);               
            }
            else
            {
                graphView = null;
            }

            if (graph.isWithInBounds(startX, startY) && graph.isWithInBounds(goalX, goalY))
            {
                Node startNode = graph.nodes[startX, startY];
                Node goalNode = graph.nodes[goalX, goalY];


                if (pathFinder.Init(graph, graphView, startNode, goalNode))
                {                   
                    StartCoroutine(pathFinder.SearchRoutine((List<Node> returnPath) => { OnComplete(returnPath); }, timeStep));
                }
                else
                {
                    if (OnComplete != null)
                    {
                        OnComplete(null);
                    }
                }

            }
        }
        else
        {
            if (OnComplete != null)
            {
                OnComplete(null);
            }
        }
    }

    public void StartSearch(Vector2 from, Vector2 to, Action<List<Node>> OnComplete)
    {

        setStartPosition((int)from.x, (int)from.y);
        setEndPosition((int)to.x, (int)to.y);

        //StartSearch(OnComplete);
        StartSearch((List<Node> obj) => {
            OnComplete(pathFinder.getPath());  
         });
    }

    void setStartPosition(int x, int y)
    {
        this.startX = x;
        this.startY = y;
    }

    void setEndPosition(int x, int y)
    {
        this.goalX = x;
        this.goalY = y;
    }
}
