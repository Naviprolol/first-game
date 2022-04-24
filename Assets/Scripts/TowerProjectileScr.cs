using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScr : MonoBehaviour
{
    Transform target;
    float speed = 7;
    int damage = 10;

    void Update()
    {
        Move();
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    void Move()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < .1f)
            {
                target.GetComponent<EnemyScr>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * speed);
            }
        }
        else
            Destroy(gameObject);
    }
}
