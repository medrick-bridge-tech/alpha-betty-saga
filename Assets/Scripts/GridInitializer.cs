using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInitializer : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;

    void Start()
    {
        var tiles = FindObjectsOfType<Tile>();
        for (int i = 0; i < _gridConfig.TileRows.Count; i++)
        {
            for (int j = 0; j < _gridConfig.TileRows[i].TileData.Count; j++)
            {
                tiles[i * _gridConfig.TileRows.Count + j].SetLetter(_gridConfig.TileRows[i].TileData[j].TileLetter);
            }
        }
    }

    void Update()
    {
        
    }
}
