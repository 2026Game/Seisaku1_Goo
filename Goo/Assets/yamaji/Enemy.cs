using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 1.0f;

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
