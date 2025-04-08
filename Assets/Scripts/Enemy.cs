using System.Collections;
using UnityEngine;

public class Enemy : MovingObjectParent, IDamageable
{
    [SerializeField] private int health = 1;
    [SerializeField] private float scoreValue = 5f;

    [SerializeField] private float shootInterval = 1f;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float projectileSpeed = 10f;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        transform.position = spawnLocation;

        StartCoroutine(ShootRoutine());
        GameManager.GameInstance.onGameOver.AddListener(StopShooting);
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
        GameManager.GameInstance.AddScore(scoreValue);
        GameManager.GameInstance.onGameOver.RemoveListener(StopShooting);
        base.OnDestroy();
    }

    public void DamageIncoming()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            GameObject proj = EnemyProjectilePool.Instance.GetProjectile();
            proj.transform.position = firePoint.position;

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.left * projectileSpeed;
            }
        }
    }

    private void StopShooting()
    {
        StopCoroutine(ShootRoutine());
    }
}
