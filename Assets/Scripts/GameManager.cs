using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager GameInstance { get; private set; }

    public UnityEvent onGameOver = new UnityEvent();

    public float score = 0f;

    public bool gameIsLive = false;

    
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
        gameIsLive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsLive)
        {
            score += Time.deltaTime;
        }
    }
    public void GameOver()
    {
        onGameOver.Invoke();

        score = 0f;
        gameIsLive = false;

    }
    public int GetCurrentScore()
    {
        return Mathf.RoundToInt(score);
    }

    
}
