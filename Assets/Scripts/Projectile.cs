using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float lifetime = 3f;

    private float timer;
    private ObjectPool pool;

    public void SetPool(ObjectPool objectPool)
    {
        pool = objectPool;
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime && pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the hit actor implements the damage interface and send a message if so
        IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
        if (idamageable != null)
        {
            idamageable.DamageIncoming();
            pool.ReturnToPool(gameObject);
        }
        else
        {
            pool.ReturnToPool(gameObject);
        }
    }
}
