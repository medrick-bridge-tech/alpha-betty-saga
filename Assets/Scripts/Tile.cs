using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private GameObject _explosionParticles;
    
    private SelectionController _selectionController;
    private WordFinder _wordFinder;

    private TextMeshPro _text;
    private string _letter;
    private bool _isSelectable = true;

    private const float DISTANCE_BETWEEN_TILES = 0.25f;
    private const float TILE_FALLING_SPEED = 2.5f;
    private const int ALPHABET_LETTERS_COUNT = 26;
    

    void Awake()
    {
        _wordFinder = FindObjectOfType<WordFinder>();
        _selectionController = FindObjectOfType<SelectionController>();

        _text = GetComponentInChildren<TextMeshPro>();
        
        // var letter = (char) ('A' + Random.Range(0, ALPHABET_LETTERS_COUNT));
        // GetComponentInChildren<TextMeshPro>().text = letter.ToString();
        // _letter = letter.ToString();
    }

    public void SetLetter(string letter)
    {
        _text.text = letter;
        _letter = letter;
    }

    public void SetRandomLetter()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isSelectable = true;
        }
    }

    private void FixedUpdate()
    {
        CheckRaycast();
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
            foreach (var tile in _selectionController.SelectedTiles)
            {
                var tilePosition = new Vector2(tile.transform.position.x, tile.transform.position.y + 0.38f);
                Instantiate(_explosionParticles, tilePosition, quaternion.identity);
                Destroy(tile.gameObject);
            }
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

    private void CheckRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, DISTANCE_BETWEEN_TILES);
        
        if (hit.collider == null)
        {
            transform.position = Vector2.Lerp(transform.position,
                new Vector2(transform.position.x, transform.position.y - DISTANCE_BETWEEN_TILES), TILE_FALLING_SPEED);
        }
    }
}
