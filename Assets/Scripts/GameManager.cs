 using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameInstance { get; private set; }

    public float score = 0f;

    private void Awake()
    {
        // Singleton setup
        if (GameInstance != null && GameInstance != this)
        {
            Destroy(gameObject); // Avoid duplicates
            return;
        }

        GameInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCurrentScore()
    {
        return Mathf.RoundToInt(score);
    }
}
