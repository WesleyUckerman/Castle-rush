using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform Weapon;
    [SerializeField] ParticleSystem ProjectileParticle;
    [SerializeField] float range = 15f;
    Transform Target;
    void Update()
    {
        FindClosestTarget();
        Aimweapon();
    }

    void FindClosestTarget() 
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform ClosestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetdistance = Vector3.Distance(transform.position,enemy.transform.position);
            if(targetdistance< maxDistance)
            {
                ClosestTarget = enemy.transform;
                maxDistance = targetdistance;
            }
        }
        Target = ClosestTarget;
    }

    void Aimweapon()
    {
        float  targetdistance = Vector3.Distance(transform.position, Target.position);
        

        if(targetdistance < range)
        {
            Attack(true);
            Weapon.LookAt(Target);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionmodule = ProjectileParticle.emission;
        emissionmodule.enabled = isActive;
    }
}
