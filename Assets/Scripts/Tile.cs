using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _defaultMaterial;
    
    private SelectionController _selectionController;
    private WordFinder _wordFinder;
    
    private string _letter;
    private bool _isSelectable = true;


    void Awake()
    {
        _wordFinder = FindObjectOfType<WordFinder>();
        _selectionController = FindObjectOfType<SelectionController>();
        
        var letter = (char) ('A' + Random.Range(0, 26));
        GetComponentInChildren<TextMeshPro>().text = letter.ToString();
        _letter = letter.ToString();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isSelectable = true;
        }
    }

    private void OnMouseOver()
    {
        if (_selectionController.IsPointerDown && _isSelectable)
        {
            GetComponent<SpriteRenderer>().material = _greenMaterial;
            _selectionController.SelectedTiles.Add(this);
            _selectionController.CurrentWord += _letter;
            _isSelectable = false;
            Debug.Log(_selectionController.CurrentWord);
        }
    }

    private void OnMouseUp()
    {
        if (_selectionController.CurrentWord.Length >= 3 && _wordFinder.DetectWord(_selectionController.CurrentWord))
        {
            Debug.Log(true);
        }
        else
        {
            Debug.Log(false);
        }
        
        foreach (var tile in _selectionController.SelectedTiles)
        {
            tile.GetComponent<SpriteRenderer>().material = _defaultMaterial;
        }
        
        _selectionController.SelectedTiles = new List<Tile>();
        _selectionController.CurrentWord = null;
    }
}
