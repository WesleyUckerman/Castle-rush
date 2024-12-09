using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] [Range(0, 50)]int PoolSize = 5;
    [SerializeField] [Range(0.1f , 30f)] float SpawnTimer = 1f;
    GameObject[] pool;

    void Awake() 
    {
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[PoolSize];

        for(int i = 0; i < pool.Length; i ++)
        {
            pool[i] = Instantiate (EnemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnablePool()
    {
        for(int i = 0; i < pool.Length; i ++)
        {
           if(pool[i].activeInHierarchy == false)
           {
            pool[i].SetActive(true);
            return;
           }
        }
    }

    IEnumerator SpawnEnemy() 
    {
        while(true)
        {
            EnablePool();
            yield return new  WaitForSeconds(SpawnTimer);
        }
    }

   
}
