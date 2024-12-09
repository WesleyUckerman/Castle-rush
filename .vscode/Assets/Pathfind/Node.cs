using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class Node 
{
   public Vector2Int coordinates;
   public bool isWalkable;
   public bool isPath;
   public bool isExplored;
   public Node ConnectedTo;


   public Node(Vector2Int coordinates, bool isWalkable) 
   {
    this.coordinates = coordinates;
    this.isWalkable = isWalkable;
   }
}
