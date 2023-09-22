using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _explosionParticles;
    [SerializeField] private AudioClip _matchAudioClip;
    [SerializeField] private AudioClip _hoverAudioClip;
    [SerializeField] private GameObject outline;

    private SelectionController _selectionController;
    private WordFinder _wordFinder;
    private ScoreCalculator _scoreCalculator;
    private UIController _uiController;

    private Animator animator;
    private AudioSource audioSource;

    private TextMeshPro _text;
    private string _letter;
    private bool isGameFinished;
    private bool _isFalling;
    private bool _isSelectable = true;

    private const float DISTANCE_BETWEEN_TILES = 0.25f;
    private const float TILE_FALLING_SPEED = 2.5f;
    private const int ALPHABET_LETTERS_COUNT = 26;

    void Awake()
    {
        animator = GetComponent<Animator>();
        _wordFinder = FindObjectOfType<WordFinder>();
        audioSource = Camera.main.GetComponent<AudioSource>();
        _selectionController = FindObjectOfType<SelectionController>();
        _uiController = FindObjectOfType<UIController>();
        _scoreCalculator = FindObjectOfType<ScoreCalculator>();

        _text = GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        if (isGameFinished)
        {
            return;
        }
        
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
        if (!isGameFinished && _selectionController.IsPointerDown && _isSelectable)
        {
            audioSource.PlayOneShot(_hoverAudioClip);
            outline.SetActive(true);
            animator.Play("SelectBox");
            _selectionController.SelectedTiles.Add(this);
            _selectionController.CurrentWord += _letter;
            _isSelectable = false;
            _uiController.DisplayCurrentWord(_selectionController.CurrentWord);
        }
    }

    private void OnMouseUp()
    {
        if (!isGameFinished && _selectionController.CurrentWord.Length >= 3 && _wordFinder.DetectWord(_selectionController.CurrentWord))
        {
            var matchScore = _scoreCalculator.CalculateMatchScore(_selectionController.CurrentWord);
            _uiController.InstantiateScoreUpText(_selectionController.SelectedTiles.Last().transform.position + new Vector3(0, 0.38f, 0), matchScore);
            
            foreach (var tile in _selectionController.SelectedTiles)
            {
                var tilePosition = new Vector2(tile.transform.position.x, tile.transform.position.y + 0.38f);
                var particles = Instantiate(_explosionParticles, tilePosition, quaternion.identity);
                tile.animator.Play("Explode");
                Destroy(particles, 0.3f);
                Destroy(tile.gameObject, 0.3f);
                
                audioSource.pitch = GetAudioPitch(_selectionController.SelectedTiles.Count);
                audioSource.PlayOneShot(_matchAudioClip);
            }
            
            var totalScore = _scoreCalculator.CalculateTotalScore(matchScore);
            _uiController.UpdateScore(totalScore);
            _uiController.UpdateMovementsAllowed();

            if (_uiController.MovementsAllowed <= 0 && totalScore < _uiController.LevelConfig.ScoreRequiredToWin)
            {
                _uiController.ShowLosePopup();
            }
            else if (totalScore >= _uiController.LevelConfig.ScoreRequiredToWin)
            {
                _uiController.ShowWinPopup();
            }
        }

        foreach (var tile in _selectionController.SelectedTiles)
        {
            tile.outline.SetActive(false);
        }

        _selectionController.SelectedTiles = new List<Tile>();
        _selectionController.CurrentWord = null;
    }

    private float GetAudioPitch(int tileSize)
    {
        if (tileSize <= 3)
            return 1;
        if (tileSize <= 4)
            return 1.1f;
        else
            return 1.2f;
    }
    
    public void SetLetter(string letter)
    {
        _text.text = letter;
        _letter = letter;
    }

    public void SetRandomLetter()
    {
        var letter = (char) ('a' + Random.Range(0, ALPHABET_LETTERS_COUNT));
        GetComponentInChildren<TextMeshPro>().text = letter.ToString();
        _letter = letter.ToString();
    }

    private void CheckRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f * DISTANCE_BETWEEN_TILES);

        if (HasStoppedFalling())
        {
            _isFalling = false;

            var tween = transform.DOMoveY(transform.position.y - DISTANCE_BETWEEN_TILES / 3, 0.1f).SetEase(Ease.OutQuad);
            tween.SetAutoKill(false);
            tween.OnComplete(() =>
            {
                transform.DOMoveY(transform.position.y + DISTANCE_BETWEEN_TILES / 3, 0.3f).SetEase(Ease.InQuad);
            });

        }
        
        if (hit.collider == null)
        {
            _isFalling = true;
            transform.position = Vector2.Lerp(transform.position,
                new Vector2(transform.position.x, transform.position.y - DISTANCE_BETWEEN_TILES), TILE_FALLING_SPEED);
        }

        bool HasStoppedFalling()
        {
            return _isFalling && hit.collider != null;
        }
    }

    public void FinishGame()
    {
        isGameFinished = true;
    }
}
