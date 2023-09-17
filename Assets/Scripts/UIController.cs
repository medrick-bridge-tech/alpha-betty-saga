using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _currentWordText;
    
    
    void Start()
    {
        _scoreText.text = "0 / " + _levelConfig.ScoreRequiredToWin;
    }
    
    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString() + " / " + _levelConfig.ScoreRequiredToWin;
    }

    public void DisplayCurrentWord(string currentWord)
    {
        _currentWordText.text = currentWord;
    }
}
