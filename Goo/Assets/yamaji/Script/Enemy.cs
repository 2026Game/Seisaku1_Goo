using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2.0f;

    public float checkDistance = 0.6f;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private Collider2D enemyCollider;

    // 壁を避けている最中かどうか
    private bool isAvoidingWall = false;

    // 1なら左、-1なら右
    private int avoidSide = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (GameManager.EnemiesPaused)
        return;

        if (player == null || rb == null)
            return;

        Vector2 toPlayer =
            (Vector2)player.position - rb.position;

        if (toPlayer.sqrMagnitude < 0.001f)
            return;

        toPlayer.Normalize();

        // 壁を避けていないとき
        if (!isAvoidingWall)
        {
            if (IsBlocked(toPlayer))
            {
                // 壁を発見！避けるモードに入る
                isAvoidingWall = true;

                Vector2 left =
                    new Vector2(-toPlayer.y, toPlayer.x);

                Vector2 right =
                    new Vector2(toPlayer.y, -toPlayer.x);

                bool leftBlocked = IsBlocked(left);
                bool rightBlocked = IsBlocked(right);

                // 通れる側を選ぶ
                if (!leftBlocked && rightBlocked)
                {
                    avoidSide = 1;
                }
                else if (leftBlocked && !rightBlocked)
                {
                    avoidSide = -1;
                }
                else if (!leftBlocked && !rightBlocked)
                {
                    // プレイヤーに近づきやすい側を選ぶ
                    float leftScore =
                        Vector2.Dot(left, toPlayer);

                    float rightScore =
                        Vector2.Dot(right, toPlayer);

                    avoidSide =
                        leftScore >= rightScore ? 1 : -1;
                }
            }
        }
        else
        {
            // 壁を避けている間も、
            // プレイヤーへの直線経路を確認する
            if (!IsBlocked(toPlayer))
            {
                // 壁がなくなったので追跡に戻る
                isAvoidingWall = false;
            }
        }

        Vector2 moveDirection;

        if (isAvoidingWall)
        {
            // 壁に沿って横方向へ移動する
            Vector2 tangent =
                new Vector2(-toPlayer.y, toPlayer.x)
                * avoidSide;

            if (IsBlocked(tangent))
            {
                // 進めない場合は反対側を試す
                avoidSide *= -1;

                tangent =
                    new Vector2(-toPlayer.y, toPlayer.x)
                    * avoidSide;
            }

            // 両側がふさがっている場合は一旦止まる
            if (IsBlocked(tangent))
                return;

            moveDirection = tangent;
        }
        else
        {
            // 通常はプレイヤーへ向かう
            moveDirection = toPlayer;
        }

        Vector2 nextPosition =
            rb.position +
            moveDirection * speed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }

    bool IsBlocked(Vector2 direction)
    {
        if (enemyCollider == null)
        {
            return Physics2D.Raycast(
                rb.position,
                direction,
                checkDistance,
                wallLayer
            ).collider != null;
        }

        float radius = Mathf.Min(
            enemyCollider.bounds.extents.x,
            enemyCollider.bounds.extents.y
        ) * 0.8f;

        RaycastHit2D hit = Physics2D.CircleCast(
            rb.position,
            radius,
            direction,
            checkDistance,
            wallLayer
        );

        return hit.collider != null;
    }
}