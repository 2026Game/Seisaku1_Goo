using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // 増やす鬼のPrefab
    public GameObject enemyPrefab;

    // 鬼が増える間隔（秒）
    public float spawnInterval = 10.0f;

    // 鬼を出現させる位置
    public Transform[] spawnPoints;

    // 次に鬼が増えるまでの時間
    private float timer = 0.0f;

    void Update()
    {
        // 時間を加算する
        timer += Time.deltaTime;

        // 指定した時間が経過したら
        if (timer >= spawnInterval)
        {
            // タイマーをリセットする
            timer = 0.0f;

            // 鬼を1体増やす
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Prefabが設定されていなければ何もしない
        if (enemyPrefab == null)
            return;

        // 出現位置が設定されていなければ何もしない
        if (spawnPoints == null || spawnPoints.Length == 0)
            return;

        // 出現位置をランダムに選ぶ
        int index = Random.Range(0, spawnPoints.Length);

        Transform point = spawnPoints[index];

        // 鬼を生成する
        GameObject enemy = Instantiate(
            enemyPrefab,
            point.position,
            Quaternion.identity
        );

        // 新しい鬼にもプレイヤーを設定する
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
