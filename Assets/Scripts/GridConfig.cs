using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridConfig", fileName = "Grid")]
public class GridConfig : ScriptableObject
{
    [SerializeField] private List<TileRow> _tileRows = new List<TileRow>();

    public List<TileRow> TileRows => _tileRows;
}
