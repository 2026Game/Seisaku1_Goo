using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 1.0f;

    // 壁を検知する距離
    public float checkDistance = 0.7f;

    // 壁のLayerを指定する
    public LayerMask wallLayer;

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // プレイヤーがいる方向を計算する
        Vector2 toPlayer =
            (player.position - transform.position).normalized;

        // まずはプレイヤーの方向へ進む
        Vector2 moveDirection = toPlayer;

        // 進行方向に壁があるか調べる
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            moveDirection,
            checkDistance,
            wallLayer
        );

        // 壁が見つかった場合
        if (hit.collider != null)
        {
            // 左右の方向を計算する
            Vector2 left =
                new Vector2(-toPlayer.y, toPlayer.x);

            Vector2 right =
                new Vector2(toPlayer.y, -toPlayer.x);

            // 左右に壁があるか調べる
            bool leftBlocked = Physics2D.Raycast(
                transform.position,
                left,
                checkDistance,
                wallLayer
            ).collider != null;

            bool rightBlocked = Physics2D.Raycast(
                transform.position,
                right,
                checkDistance,
                wallLayer
            ).collider != null;

            // 左右どちらかが通れるならそちらへ進む
            if (!leftBlocked && !rightBlocked)
            {
                float leftDistance = Vector2.Distance(
                    (Vector2)transform.position + left,
                    player.position
                );

                float rightDistance = Vector2.Distance(
                    (Vector2)transform.position + right,
                    player.position
                );

                moveDirection =
                    leftDistance < rightDistance ? left : right;
            }
            else if (!leftBlocked)
            {
                moveDirection = left;
            }
            else if (!rightBlocked)
            {
                moveDirection = right;
            }
            else
            {
                // 前も左右も壁なら、いったん後退する
                moveDirection = -toPlayer;
            }
        }

        // 決定した方向に移動する
        transform.position +=
            (Vector3)(moveDirection * speed * Time.deltaTime);
    }
}