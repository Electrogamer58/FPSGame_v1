using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public int enemyCount;
    public int maxEnemyCount;
    public float spawnWaitTime;
    int xPos;
    int yPos;
    int zPos;
    

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < maxEnemyCount)
        {
            xPos = Random.Range(-31, 32);
            yPos = Random.Range(5, 15);
            zPos = Random.Range(-31, 32);
            Instantiate(enemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnWaitTime);
            enemyCount ++;
        }
    }
}
