using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You quit");
    }

    public void ReturnToMenu()
    {
        AudioManager.Instance.PlayMusic("BackgroundMusic");
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
