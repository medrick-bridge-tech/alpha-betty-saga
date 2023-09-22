using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScoreUpText : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveX(transform.position.x + 0.3f, 1);
        transform.DOMoveY(transform.position.y + 3, 1).SetEase(Ease.InBounce);
        var tween = transform.DOScale(Vector3.one * 2, 1);
        
        tween.OnComplete(() =>
        {
            transform.DOMoveX(transform.position.x - 2, 1);
            transform.DOMoveY(transform.position.y + 3, 1).SetEase(Ease.OutQuint);
            transform.DOScale(Vector3.zero, 1);
        });
    }
}
