using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _defaultMaterial;
    
    private SelectionController _selectionController;
    private bool isSelectable;

    
    void Awake()
    {
        _selectionController = FindObjectOfType<SelectionController>();
        isSelectable = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isSelectable = true;
        }
    }
    
    private void OnMouseOver()
    {
        if (_selectionController.IsSelect && isSelectable)
        {
            isSelectable = false;
            GetComponent<SpriteRenderer>().material = _greenMaterial;
            _selectionController.SelectedTiles.Add(this);
        }
    }

    private void OnMouseUp()
    {
        foreach (var tile in _selectionController.SelectedTiles)
        {
            tile.GetComponent<SpriteRenderer>().material = _defaultMaterial;
        }
        _selectionController.SelectedTiles = new List<Tile>();
    }
}
