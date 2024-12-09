using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0,10)]float Speed = 1f;

    Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
       FindPath();
       ReturnToStart();
       StartCoroutine (FollowPath());
    }
    void Start() 
    {
       enemy = GetComponent<Enemy>(); 
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if(waypoint !=null)
            {
                 path.Add(waypoint);
            }
           
        }
    }
    
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void finishpath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }
   IEnumerator FollowPath()
   {
    foreach(Waypoint waypoint in path)
    {
        Vector3 Startposition = transform.position;
        Vector3 Endposition = waypoint.transform.position;
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
