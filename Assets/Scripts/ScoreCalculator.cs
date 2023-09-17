using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator
{
    private Dictionary<char, int> _letterPoint = new Dictionary<char, int>()
    {
        { 'a', 1 },
        { 'b', 3 },
        { 'c', 2 },
        { 'd', 2 },
        { 'e', 1 },
        { 'f', 3 },
        { 'g', 3 },
        { 'h', 2 },
        { 'i', 1 },
        { 'j', 4 },
        { 'k', 4 },
        { 'l', 2 },
        { 'm', 3 },
        { 'n', 3 },
        { 'o', 1 },
        { 'p', 3 },
        { 'q', 4 },
        { 'r', 3 },
        { 's', 3 },
        { 't', 1 },
        { 'u', 2 },
        { 'v', 3 },
        { 'w', 3 },
        { 'x', 4 },
        { 'y', 3 },
        { 'z', 4 }
    };
    

    public int CalculateScore(string word)
    {
        int tileCount = word.Length;
        int letterPoints = 0;

        foreach (char letter in word)
        {
            if (_letterPoint.ContainsKey(letter))
            {
                letterPoints += _letterPoint[letter];
            }
        }

        int score = tileCount * letterPoints * 10;
        return score;
    }
}
