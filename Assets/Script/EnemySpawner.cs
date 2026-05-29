
using UnityEngine;



public class EnemySpawner : MonoBehaviour

{

    [Header("敵Prefab")]

    public GameObject enemyPrefab;



    [Header("スポーン位置")]

    public Transform[] spawnPoints;



    [Header("スポーン設定")]

    public float spawnInterval = 2f;

    public int maxEnemyCount = 10;



    private float timer;



    void Update()

    {

        timer += Time.deltaTime;



        if (timer >= spawnInterval)

        {

            timer = 0f;

            SpawnEnemy();

        }

    }



    void SpawnEnemy()

    {

        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;



        if (enemyCount >= maxEnemyCount)

        {

            return;

        }



        int index = Random.Range(0, spawnPoints.Length);



        Instantiate(

        enemyPrefab,

        spawnPoints[index].position,

        spawnPoints[index].rotation

        );

    }

}


