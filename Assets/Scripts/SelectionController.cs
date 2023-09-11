using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    private List<Tile> _selectedTiles = new List<Tile>();

    public List<Tile> SelectedTiles
    {
        get => _selectedTiles;
        set => _selectedTiles = value;
    }
    
    private string _currentWord;

    public string CurrentWord
    {
        get => _currentWord;
        set => _currentWord = value;
    }

    private bool _isPointerDown;
    public bool IsPointerDown => _isPointerDown;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _isPointerDown = true;
        }
        else
        {
            _isPointerDown = false;
        }
    }
}
