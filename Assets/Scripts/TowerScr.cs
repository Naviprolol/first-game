using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScr : MonoBehaviour
{
    float range = 2;
    public float CurrentCooldown, Cooldown;

    public GameObject Projectile;

    void Update()
    {
        if (CanShoot())
            SearchTarget();

        if (CurrentCooldown > 0)
            CurrentCooldown -= Time.deltaTime;
    }

    bool CanShoot()
    {
        if (CurrentCooldown <= 0)
            return true;
        return false;
    }
    
    void SearchTarget()
    {
        Transform nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (currentDistance < nearestEnemyDistance && 
                currentDistance <= range)
            {
                nearestEnemy = enemy.transform;
                nearestEnemyDistance = currentDistance;
            }
        }

        if (nearestEnemy != null)
            Shoot(nearestEnemy);

    }

    void Shoot(Transform enemy)
    {
        CurrentCooldown = Cooldown;
        GameObject proj = Instantiate(Projectile);
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProjectileScr>().SetTarget(enemy);
    }
}
