using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject enemyPrefab;

    
    public float spawnInterval = 10.0f;

   
    public Transform[] spawnPoints;

   
    private float timer = 0.0f;

   
    public int MaxEnemy = 3;
    private int EnemyCount = 0;

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

       
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
