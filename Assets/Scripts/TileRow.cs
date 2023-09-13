using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileRow
{
    [SerializeField] private List<TileData> _tileData = new List<TileData>();

    public List<TileData> TileData => _tileData;
}

[System.Serializable]
public class TileData
{
    [SerializeField] private string _tileLetter;

    public string TileLetter => _tileLetter;
}