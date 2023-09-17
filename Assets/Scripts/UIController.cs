using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
