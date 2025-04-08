using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager GameInstance { get; private set; }

    public UnityEvent onGameOver = new UnityEvent();

    public float score = 0f;

    public bool gameIsLive;

    public SaveData saveData;
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
        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            saveData = JsonUtility.FromJson<SaveData>(loadedData);
        }
        else
        {
            saveData = new SaveData();
        }

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
        // Check for new high score
        if (saveData.highScore < score)
        {
            saveData.highScore = Mathf.RoundToInt(score);

            string saveString = JsonUtility.ToJson(saveData);
            SaveSystem.Save("save", saveString);
        }

        gameIsLive = false;

        // Invoke all events that care about a game over
        onGameOver.Invoke();

    }
    public int GetCurrentScore()
    {
        return Mathf.RoundToInt(score);
    }

    public void AddScore(float scoreAdd)
    {
        score += scoreAdd;
    }
}
