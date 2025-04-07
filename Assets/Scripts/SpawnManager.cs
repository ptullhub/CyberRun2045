using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager SpawnInstance { get; private set; }

    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private float obstacleSpawnTime = 2f;

    private void Awake()
    {
        // Singleton setup
        if (SpawnInstance != null && SpawnInstance != this)
        {
            Destroy(gameObject); // Avoid duplicates
            return;
        }

        SpawnInstance = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), obstacleSpawnTime, obstacleSpawnTime);
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(Spawn));
    }

    public void SetSpawnRate(float newRate)
    {
        CancelInvoke(nameof(Spawn));
        obstacleSpawnTime = newRate;
        InvokeRepeating(nameof(Spawn), obstacleSpawnTime, obstacleSpawnTime);
    }
}
