using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _currentWordText;
    [SerializeField] private Text _movementsAllowedText;
    [SerializeField] private Text _popupText;
    [SerializeField] private GameObject _panel;
    [SerializeField] private LevelConfig _levelConfig;
    public LevelConfig LevelConfig => _levelConfig;

    private int _movementsAllowed;
    public int MovementsAllowed => _movementsAllowed;


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

    public void ShowLosePopup()
    {
        _panel.SetActive(true);
        _popupText.text = "You lose!";
        StartCoroutine(DisableTiles());
    }
    
    public void ShowWinPopup()
    {
        _panel.SetActive(true);
        _popupText.text = "You win!";
        StartCoroutine(DisableTiles());
    }

    private IEnumerator DisableTiles()
    {
        yield return new WaitForSeconds(1);
        
        var tiles = FindObjectsOfType<Tile>();
    
        foreach (var tile in tiles)
        {
            tile.FinishGame();
        }
    }
}