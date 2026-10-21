using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private Rigidbody2D rb;
    private bool movementLocked = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (movementLocked || rb == null)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection =
            new Vector2(moveX, moveY).normalized;

        Vector2 nextPosition =
            rb.position +
            moveDirection * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }

    public void SetMovementLocked(bool locked)
    {
        movementLocked = locked;

        // ïﬂÇ‹Ç¡ÇΩÇ∆Ç´Ç…ï®óùìIÇ»ê®Ç¢Ç≈ìÆÇ©Ç»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.CapturePlayer();
        }
    }
    
}