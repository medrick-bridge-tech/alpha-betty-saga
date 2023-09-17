using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _currentWordText;
    [SerializeField] private Text _movementsAllowedText;

    private int _movementsAllowed;
    
    
    void Start()
    {
        _scoreText.text = "0 / " + _levelConfig.ScoreRequiredToWin;
        
        _movementsAllowed = _levelConfig.MovementsAllowed;
        _movementsAllowedText.text = _movementsAllowed.ToString();
    }
    
    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString() + " / " + _levelConfig.ScoreRequiredToWin;
    }

    public void DisplayCurrentWord(string currentWord)
    {
        _currentWordText.text = currentWord;
    }

    public void UpdateMovementsAllowed()
    {
        _movementsAllowed--;
        _movementsAllowedText.text = _movementsAllowed.ToString();
    }
}
