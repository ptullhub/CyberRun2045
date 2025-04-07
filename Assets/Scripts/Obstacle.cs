using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float obstacleSpeed;
    [SerializeField] private float spawnHeight;
    // Start is called before the first frame update
    private void Start()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * obstacleSpeed;
        transform.position = new Vector2(9.5f, -3.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the hit actor implements the damage interface and send a message if so
        IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
        if (idamageable != null)
        {
            idamageable.DamageIncoming();
        }
    }
}
