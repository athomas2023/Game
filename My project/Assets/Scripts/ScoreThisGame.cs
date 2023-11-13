using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreThisGame : MonoBehaviour
{
    [SerializeField] private HighScoreSO score;
    [SerializeField] private string textToDisplay;
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = textToDisplay + score.highScore;
    }
}
