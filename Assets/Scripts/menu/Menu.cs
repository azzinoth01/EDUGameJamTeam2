using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{    public void StartGame()
    {
        // "GameScene" senin asıl oyun sahnenin ismi olacak
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan çıkılıyor...");
        Application.Quit();
    }
}
