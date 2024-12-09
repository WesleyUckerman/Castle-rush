using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHitPoints = 5;
    [Tooltip("Add Health when wnemy dies.")]
    [SerializeField] int DificultRamp = 1;
    int CurrentHitPoints = 0;

    Enemy enemy;
    void OnEnable()
    {
        CurrentHitPoints = MaxHitPoints;
    }

    void Start() 
    {
       enemy = GetComponent<Enemy>(); 
    }

    void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        CurrentHitPoints--;
        if(CurrentHitPoints<=0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
            MaxHitPoints += DificultRamp;
        }
    }
    
}
