using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour

{
    public GameObject credit;
    public GameObject menu;
    public void StartGame() {
        SceneManager.LoadScene(2);
    }

    public void QuitGame() {
        Application.Quit();
    }
    public void ShowCredits() {
        credit.SetActive(true);
        menu.SetActive(false);
    }

    public void HideCredits() {
        credit.SetActive(false);
        menu.SetActive(true);
    }
}
