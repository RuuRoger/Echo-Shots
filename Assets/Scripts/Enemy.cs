using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Enemy : MonoBehaviour
{
    //Public properties
    public GameObject bulleetPrefabEnemy;
    public Transform enemyBullets;
    public float speedEnemy;
    public float velocityBullet;
    public TextMeshProUGUI uiWin;

    //private attributes
    private Vector2 _randomEnemyPosition;
    private int[] _randomPositions;
    private int _randomIndexX;
    private int _randomIndexY;
    private int _enemyLives;
    private float _nextShotTime;
    private float _shootInterval;
    

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

        //Time and interval to shoot
        _shootInterval = 3f;
        _nextShotTime = Time.time + _shootInterval;

        //Enemy lives
        _enemyLives = 20;

        //UI
        uiWin.enabled = false;

    }

    private void Update()
    {
        //Time and interval to shoot
        if (Time.time >= _nextShotTime)
        {
            EnemyShoots();              
            _nextShotTime = Time.time + _shootInterval;
        }

        //Win
        if (_enemyLives <= 0)
        {
            Debug.Log("Has ganado!");
            Time.timeScale = 0;
            uiWin.enabled = true;
        }

        Debug.Log("Vidas enemigo: " + _enemyLives);

    }
    private void EnemyShoots ()
    {
        GameObject bulletRed = GameObject.Instantiate(bulleetPrefabEnemy, enemyBullets.position, enemyBullets.rotation);
        bulletRed.GetComponent<Rigidbody2D>().AddForce(-enemyBullets.right * velocityBullet, ForceMode2D.Impulse);
        Destroy(bulletRed, 5f );

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Green") _enemyLives--;
    }

}
