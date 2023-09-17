using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridInitializer : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    

    void Start()
    {
        var tiles = FindObjectsOfType<Tile>();
        var sortedTiles = tiles.OrderBy(tile => tile.gameObject.name).ToArray();
        
        for (int i = 0; i < _levelConfig.GridConfig.TileRows.Count; i++)
        {
            for (int j = 0; j < _levelConfig.GridConfig.TileRows[i].TileData.Count; j++)
            {
                sortedTiles[i * _levelConfig.GridConfig.TileRows.Count + j].SetLetter(_levelConfig.GridConfig.TileRows[i].TileData[j].TileLetter);
            }
        }
    }
}
