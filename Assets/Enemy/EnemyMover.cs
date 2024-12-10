using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();
    [SerializeField] [Range(0,10)]float Speed = 1f;

    GridManager gridManager;
    PathFinder pathFinder;

    Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        
    }
    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        enemy = GetComponent<Enemy>(); 
    }

    void RecalculatePath(bool ResetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if(ResetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine (FollowPath());
    }
    
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void finishpath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }
   IEnumerator FollowPath()
   {
    for(int i = 1 ; i < path.Count; i++)
    {
        Vector3 Startposition = transform.position;
        Vector3 Endposition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
        float Travelpercentage = 0f;

        transform.LookAt(Endposition);

        while(Travelpercentage < 1)
        {
            Travelpercentage += Time.deltaTime * Speed;
            transform.position = Vector3.Lerp(Startposition,Endposition,Travelpercentage);
            yield return new WaitForEndOfFrame();
            

        }
        
    }
    finishpath();
   }
}
