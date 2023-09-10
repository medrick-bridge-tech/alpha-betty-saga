using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public List<Tile> SelectedTiles = new List<Tile>();
    
    [SerializeField] private bool isSelect;
    public bool IsSelect => isSelect;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isSelect = true;
        }
        else
        {
            isSelect = false;
        }
    }

    
    
}
