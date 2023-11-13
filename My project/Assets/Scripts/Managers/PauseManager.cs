using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseMenuManager;
    private bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            Pause();
            if (paused)
            {
                pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
                pauseMenuManager.transform.GetChild(0).gameObject.SetActive(true);

                pauseMenu.transform.GetChild(1).gameObject.SetActive(false);
                pauseMenuManager.transform.GetChild(1).gameObject.SetActive(false);

                pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
                pauseMenuManager.transform.GetChild(2).gameObject.SetActive(false);

                pauseMenu.transform.GetChild(3).gameObject.SetActive(false);
                pauseMenuManager.transform.GetChild(3).gameObject.SetActive(false);
            }
        }

        if (paused)
        {

            pauseMenu.SetActive(true);
            pauseMenuManager.SetActive(true);

            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            pauseMenuManager.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Pause()
    {
        paused = !paused;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}