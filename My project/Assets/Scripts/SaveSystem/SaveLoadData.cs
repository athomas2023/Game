using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    public HighScoreSO highScore;
    public TextMeshProUGUI highScoreText;

    private void OnEnable() 
    {
        highScore.highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScore.highScore;
    }

    private void OnDisable() 
    {
        PlayerPrefs.SetInt("HighScore", highScore.highScore);
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScore.highScore;
    }
}
