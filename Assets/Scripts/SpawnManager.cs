using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager SpawnInstance { get; private set; }

    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent; // For organization
    [SerializeField] private float runTime;
    [Range(0, 1)] public float difficultyFactor = 0.1f;
    public float obstacleSpawnTime = 4f;
    public float obstacleSpeed = 4f;

    private float baseSpawnTime;
    private float baseObstacleSpeed;
    private float lastAppliedSpawnTime; 


    private void Awake()
    {
        // Singleton setup
        if (SpawnInstance != null && SpawnInstance != this)
        {
            Destroy(gameObject); // Avoid duplicates
            return;
        }

        SpawnInstance = this;

        StartSpawning();
    }

    private void Start()
    {
        GameManager.GameInstance.onGameOver.AddListener(StopSpawning);

        runTime = 1f;

        baseSpawnTime = obstacleSpawnTime;
        baseObstacleSpeed = obstacleSpeed;
        lastAppliedSpawnTime = obstacleSpawnTime;

    }

    private void Update()
    {
        // Update difficulty over time so obstacles get faster and spawn sooner
        if (GameManager.GameInstance.gameIsLive)
        {
            runTime += Time.deltaTime;
            CalculateDifficulty();
        }
    }
    private void Spawn()
    {
        // Pick random obstacle to spawn
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = Instantiate(prefab, transform.position, Quaternion.identity, obstacleParent); // Parent for organization

        // Set it in motion
        Rigidbody2D rb = spawnedObstacle.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.left * obstacleSpeed;
        }
    }

    public void StartSpawning()
    {
        InvokeRepeating(nameof(Spawn), obstacleSpawnTime, obstacleSpawnTime);
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

    private void CalculateDifficulty()
    {
        // Calculate difficulty based off elapsed time
        float difficultyMultiplier = Mathf.Pow(runTime, difficultyFactor);

        obstacleSpeed = baseObstacleSpeed * difficultyMultiplier;

        float newSpawnTime = baseSpawnTime / difficultyMultiplier;
        newSpawnTime = Mathf.Max(newSpawnTime, 0.5f); // Clamp min value

        // Only update if spawn time has changed significantly
        if (Mathf.Abs(newSpawnTime - lastAppliedSpawnTime) > 0.15f)
        {
            obstacleSpawnTime = newSpawnTime;
            SetSpawnRate(obstacleSpawnTime);
            lastAppliedSpawnTime = newSpawnTime;
        }
    }

}
