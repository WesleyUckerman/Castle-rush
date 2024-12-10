using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField]Vector2Int startCoordinates;
    public Vector2Int StartCoordinates{get{return startCoordinates;}}
    [SerializeField]Vector2Int destinationCoordinates;
    public Vector2Int DestinationtCoordinates{get{return destinationCoordinates;}}
    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left,Vector2Int.down, Vector2Int.up};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
            
        }


    }

    void Start() 
    {
       
       
       GetNewPath();
    }

    public List<Node> GetNewPath()
    {
       return GetNewPath(startCoordinates);
    }

      public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.resetnode();
        BreadthrFirstSearch(coordinates);
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborsCoords = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighborsCoords))
            {
                neighbors.Add(grid[neighborsCoords]); 
            }
        }

        foreach( Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.ConnectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthrFirstSearch (Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
            
            
        }
    }
  
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.ConnectedTo !=null)
        {
            currentNode = currentNode.ConnectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;

    }

    public bool WillBlockPath (Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousstate = grid[coordinates].isWalkable;
            
            grid[coordinates].isWalkable = false;
            List<Node> NewPath = GetNewPath();
            grid[coordinates].isExplored = previousstate;


            if(NewPath.Count <=1 )
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }


    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath" ,false, SendMessageOptions.DontRequireReceiver);
    }
}
