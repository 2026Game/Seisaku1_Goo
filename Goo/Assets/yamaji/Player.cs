using UnityEngine;

public class Player : MonoBehaviour
{
    // プレイヤーの移動速度
    public float speed = 5.0f;

    void Update()
    {
        // WASDキーの入力を取得
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // 移動する方向を作る
        Vector3 direction = new Vector3(x, 0, z).normalized;

        // プレイヤーを移動させる
        transform.position += direction * speed * Time.deltaTime;
    }
}
