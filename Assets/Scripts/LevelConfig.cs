using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfig", fileName = "Level")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private int _movementsAllowed;

    public int MovementsAllowed => _movementsAllowed;

    [SerializeField] private int _scoreRequiredToWin;
    
    public int ScoreRequiredToWin => _scoreRequiredToWin;
    

    [SerializeField] private GridConfig _gridConfig;

    public GridConfig GridConfig => _gridConfig;
}
