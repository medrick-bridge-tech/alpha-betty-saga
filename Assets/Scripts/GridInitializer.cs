using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridInitializer : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;

    void Start()
    {
        var tiles = FindObjectsOfType<Tile>();
        var sortedTiles = tiles.OrderBy(tile => tile.gameObject.name).ToArray();
        for (int i = 0; i < _gridConfig.TileRows.Count; i++)
        {
            for (int j = 0; j < _gridConfig.TileRows[i].TileData.Count; j++)
            {
                sortedTiles[i * _gridConfig.TileRows.Count + j].SetLetter(_gridConfig.TileRows[i].TileData[j].TileLetter);
            }
        }
    }

    void Update()
    {
        
    }
}
