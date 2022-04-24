using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{
    public List<GameObject> wayPoints = new List<GameObject>();

    int wayIndex = 0;
    int speed = 3;
    public int health = 30;

    void Start()
    {
        GetWayPoints();
    }

    void Update()
    {
        Move();
        ChechIsALive();
    }

    void GetWayPoints()
    {
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScr>().wayPoints;
    }

    private void Move()
    {
        Transform currentWayPoint = wayPoints[wayIndex].transform;
        Vector3 currentWayPos = new Vector3(currentWayPoint.position.x + currentWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                            currentWayPoint.position.y - currentWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);

        Vector3 dir = currentWayPos - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, currentWayPos) < 0.1f)
        {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
                Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void ChechIsALive()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
