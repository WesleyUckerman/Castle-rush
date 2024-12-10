using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower TowerPrefab;
    [SerializeField] bool IsPlaceable;
    public bool isPlaceable {get {return IsPlaceable;}}

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>(); 
        pathFinder = FindObjectOfType<PathFinder>();  
    }

    private void Start() 
    {
        if(gridManager != null)   
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }
   void OnMouseDown() 
   {
    if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isSuccessfull = TowerPrefab.CreateTower(TowerPrefab, transform.position);
            if(isSuccessfull)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
            
           
        }
   }
}
