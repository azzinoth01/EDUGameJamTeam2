using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas; // Pause UI
    public GameObject[] gameplayCanvasesToDisable; // Diğer UI'lar (envanter, skill, healthbar vs)

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        foreach (var ui in gameplayCanvasesToDisable)
        {
            ui.SetActive(false); // Diğer UI’leri devre dışı bırak
        }
    }

    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        foreach (var ui in gameplayCanvasesToDisable)
        {
            ui.SetActive(true); 
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1); // Ana menü sahnesine dön
    }
}
