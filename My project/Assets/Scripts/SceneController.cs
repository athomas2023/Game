using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("TWalt97");
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
}
