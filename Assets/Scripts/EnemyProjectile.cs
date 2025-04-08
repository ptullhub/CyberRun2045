using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float offscreenX = -12f;

    private void Update()
    {
        if (transform.position.x < offscreenX)
        {
            EnemyProjectilePool.Instance.ReturnProjectile(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the hit actor implements the damage interface and send a message if so
        IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
        if (idamageable != null)
        {
            idamageable.DamageIncoming();
            EnemyProjectilePool.Instance.ReturnProjectile(gameObject);
        }
        else
        {
            EnemyProjectilePool.Instance.ReturnProjectile(gameObject);
        }
    }
}
