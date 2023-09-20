using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    
    
    private void FixedUpdate()
    {
        CheckRaycast();
    }

    private void CheckRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.25f);
        
        if (hit.collider == null)
        {
            var newTile = Instantiate(_tilePrefab, transform.position, quaternion.identity);
            
            newTile.transform.position = Vector2.Lerp(newTile.transform.position,
                new Vector2(transform.position.x, transform.position.y - 0.15f), 5f);
            
            newTile.GetComponent<Tile>().SetRandomLetter();
        }
    }
}
