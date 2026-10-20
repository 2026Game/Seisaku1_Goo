using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移動速度

    void Update()
    {
        // WASD / 矢印キーの入力を取得
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 移動方向を決めて正規化する
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // プレイヤーの位置を直接移動させる（Unity 6対応）
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
