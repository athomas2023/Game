using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HighScoreSO", order = 1)]
public class HighScoreSO : ScriptableObject
{
    public int highScore;
}
