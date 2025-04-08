using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MovingObjectParent
{
    [SerializeField] private float obstacleSpeed;
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        transform.position = spawnLocation;
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

    private new void OnDestroy()
    {
        base.OnDestroy();
    }
}
