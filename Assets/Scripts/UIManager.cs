using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private GameManager gameManager;

    [SerializeField] private GameObject gameOverScreen;
    private void Start()
    {
        gameOverScreen.SetActive(false);

        gameManager = GameManager.GameInstance;
        gameManager.onGameOver.AddListener(ShowGameOverScreen);
    }
    private void OnGUI()
    {
        scoreText.text = "Score: " + gameManager.GetCurrentScore().ToString();
    }

    private void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    // Methods called from buttons
    public void RestartLevel()
    {
        // I will admit reloading the scene is a bit lazy, but so am I. A restart event dispatcher would proably be better
        SceneManager.LoadScene("EndlessScene");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
