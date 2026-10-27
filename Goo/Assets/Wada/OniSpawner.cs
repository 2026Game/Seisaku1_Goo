using UnityEngine;

public class OniSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;


    GameObject OniCount;


    public float spawnInterval = 0.0f;



    public Transform[] spawnPoints;



    private float timer = 0.0f;



    public int MaxEnemy = 3;
    private int EnemyCount = 0;

    private void Start()
    {
        this.OniCount = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (MaxEnemy <= EnemyCount)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {

            timer = 0.0f;


            SpawnEnemy();


            EnemyCount++;


            this.OniCount.GetComponent<GameDirector>().OniPlus();
        }
    }


    void SpawnEnemy()
    {
        
        if (enemyPrefab == null)
            return;



        if (spawnPoints == null || spawnPoints.Length == 0)
            return;



        int index = Random.Range(0, spawnPoints.Length);


        Transform point = spawnPoints[index];


        GameObject enemy = Instantiate(
            enemyPrefab,
            point.position,
            Quaternion.identity
        );



        OniController enemyScript = enemy.GetComponent<OniController>();


        if (enemyScript != null)
        {
            enemyScript.player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
