using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordFinder : MonoBehaviour
{
    private List<string> _words = new List<string>();
    
    private SelectionController _selectionController;


    void Awake()
    {
        _selectionController = FindObjectOfType<SelectionController>();
        
        var words = File.ReadAllLines($"D:/Projects/words.txt");
        
        foreach (var word in words)
        {
            _words.Add(word);
        }
    }

    public bool DetectWord(string selectedWord)
    {
        foreach (var word in _words)
        {
            if (selectedWord.ToLower() == word)
            {
                return true;
            }
        }

        return false;
    }
}
