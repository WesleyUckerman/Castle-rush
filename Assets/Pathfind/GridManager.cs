using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{   
[SerializeField] Vector2Int gridSize;

[SerializeField] int UnityGridSize = 10;
public int unityGridSize {get{return UnityGridSize;}}
Dictionary<Vector2Int, Node> grid =  new Dictionary<Vector2Int, Node>();
public Dictionary<Vector2Int, Node> Grid {get {return grid;}}
void Awake() 
{
    CreateGrid();
}

public Node GetNode(Vector2Int coordinates)
{
    if(grid.ContainsKey(coordinates))
    {
        return grid[coordinates];
    }

    return null;
}
 
public void BlockNode(Vector2Int coordinates)
{
    if(grid.ContainsKey(coordinates))
    {
        grid[coordinates].isWalkable =false;
    }
}

public void resetnode()
{
    foreach(KeyValuePair<Vector2Int, Node> entry in grid)
    {
        entry.Value.ConnectedTo = null;
        entry.Value.isExplored = false;
        entry.Value.isPath = false;
    }
}

public Vector2Int GetCoordinatesFromPosition(Vector3 position)
{
    Vector2Int coordinates = new Vector2Int();
    coordinates.x = Mathf.RoundToInt(position.x / UnityGridSize);
    coordinates.y = Mathf.RoundToInt(position.z / UnityGridSize);

    return coordinates;
}

public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
{
    Vector3 position = new Vector3();
    position.x = Mathf.RoundToInt(coordinates.x * unityGridSize);
    position.z = Mathf.RoundToInt(coordinates.y * unityGridSize);

    return position;
}


void CreateGrid()
{
    for(int x = 0; x < gridSize.x;x++)
    {
        for(int y = 0; y< gridSize.y;y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates, true));
                Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
                
            }
    }
}
}
