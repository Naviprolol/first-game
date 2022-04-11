using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScr : MonoBehaviour
{
    public float timeToSpawn = 4;
    int spawnCount = 0;

    public GameObject enemyPref, wayPointParent;

    void Start()
    {
        
    }

    void Update()
    {
        if (timeToSpawn <= 0)
        {
            StartCoroutine(SpawnEnemy(spawnCount + 1));
            timeToSpawn = 4; 
        }
        timeToSpawn -= Time.deltaTime;
    }
    IEnumerator SpawnEnemy(int enemyCount)
    {
        spawnCount++;
        for (var i = 0; i < enemyCount; i++)
        {
            GameObject tmpEnemy = Instantiate(enemyPref);
            tmpEnemy.transform.SetParent(gameObject.transform, false);
            tmpEnemy.GetComponent<EnemyScr>().wayPointsParent = wayPointParent;

            yield return new WaitForSeconds(0.2f);
        }
    }

}
