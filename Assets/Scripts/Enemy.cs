using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    //Public properties
    public float speedEnemy;

    //private attributes
    private Vector2 _randomEnemyPosition;
    private int[] _randomPositions;
    private int _randomIndexX;
    private int _randomIndexY;
    

    //Meethods
    private void Start()
    {
        //Array with enemy positions to start in game
        _randomPositions = new int[] { -1, 0, 1 };

        //Choose first direction
        _randomIndexX = Random.Range(0, _randomPositions.Length);
        _randomIndexY = Random.Range(0,_randomPositions.Length);
        _randomEnemyPosition = new Vector2(_randomPositions[_randomIndexX], _randomPositions[_randomIndexY]);

        //Movement
        GetComponent<Rigidbody2D>().AddForce(_randomEnemyPosition * speedEnemy, ForceMode2D.Impulse);

    }

    private void Update()
    {

        
    }
}
