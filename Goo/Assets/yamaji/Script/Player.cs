using UnityEngine;

public class Player : MonoBehaviour
{
        public float moveSpeed = 10.0f;

        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (rb == null)
                return;

            // WASD / –îˆóƒL[‚Ì“ü—Í‚ğæ“¾
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            // ˆÚ“®•ûŒü‚ğŒˆ‚ß‚Ä³‹K‰»‚·‚é
            Vector2 moveDirection =
                new Vector2(moveX, moveY).normalized;

            // Rigidbody2D‚ğg‚Á‚ÄˆÚ“®‚·‚é
            Vector2 nextPosition =
                rb.position +
                moveDirection * moveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(nextPosition);
        }
}

