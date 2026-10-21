using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private Rigidbody2D rb;

    // 捕まったかどうか
    private bool isCaught = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 捕まったら移動しない
        if (isCaught || rb == null)
            return;

        // WASD / 矢印キーの入力を取得
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection =
            new Vector2(moveX, moveY).normalized;

        Vector2 nextPosition =
            rb.position +
            moveDirection * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }

    // 鬼に捕まったときに呼び出す
    public void Caught()
    {
        if (isCaught)
            return;

        isCaught = true;

        Debug.Log("捕まった！");
    }

    // 鬼と接触したか確認
    void OnTriggerEnter2D(Collider2D other)
    {
        // 相手が鬼だったら
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("鬼に捕まった！");
        }
    }
}

