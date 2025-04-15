using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene("EndlessScene");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
