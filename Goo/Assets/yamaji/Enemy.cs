using UnityEngine;

public class Enemy : MonoBehaviour
{
        public Transform player;
        public float speed = 2.0f;

        public float checkDistance = 0.6f;
        public LayerMask wallLayer;

        private Rigidbody2D rb;
        private Collider2D enemyCollider;

        
        private int avoidSide = 1;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            enemyCollider = GetComponent<Collider2D>();
        }

        void FixedUpdate()
        {
            if (player == null || rb == null)
                return;

            Vector2 toPlayer =
                (Vector2)player.position - rb.position;

            if (toPlayer.sqrMagnitude < 0.001f)
                return;

            toPlayer.Normalize();

            Vector2 moveDirection = toPlayer;

            
            if (IsBlocked(toPlayer))
            {
                Vector2 left =
                    new Vector2(-toPlayer.y, toPlayer.x);

                Vector2 right =
                    new Vector2(toPlayer.y, -toPlayer.x);

                bool leftBlocked = IsBlocked(left);
                bool rightBlocked = IsBlocked(right);

                if (!leftBlocked && !rightBlocked)
                {
                   
                    moveDirection = avoidSide == 1 ? left : right;

                  
                    if (IsBlocked(moveDirection))
                    {
                        avoidSide *= -1;
                        moveDirection =
                            avoidSide == 1 ? left : right;
                    }
                }
                else if (!leftBlocked)
                {
                    moveDirection = left;
                    avoidSide = 1;
                }
                else if (!rightBlocked)
                {
                    moveDirection = right;
                    avoidSide = -1;
                }
                else
                {
                    
                    return;
                }
            }

            Vector2 nextPosition =
                rb.position +
                moveDirection * speed * Time.fixedDeltaTime;

            rb.MovePosition(nextPosition);
        }

        bool IsBlocked(Vector2 direction)
        {
            if (enemyCollider == null)
                return Physics2D.Raycast(
                    rb.position, direction,
                    checkDistance, wallLayer
                ).collider != null;

           
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