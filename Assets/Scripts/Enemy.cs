using UnityEngine;
using TMPro;
public class Enemy : MonoBehaviour
{
    //Public properties
    public GameObject bulleetPrefabEnemy;
    public Transform enemyBullets;
    public Transform playerTransform;
    public float speedEnemy;
    public float velocityBullet;
    public TextMeshProUGUI uiWin;
    public TextMeshProUGUI uiEnemyLives;

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
        _randomPositions = new int[] { -1, 1 };

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
        _enemyLives = 80;

        //UI
        uiWin.enabled = false;

    }

    private void Update()
    {
        //UI
        uiEnemyLives.text = _enemyLives.ToString();

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
        Vector3 direction = playerTransform.position - bulletRed.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletRed.transform.rotation = Quaternion.Euler(0, 0,angle);
        Vector2 nomalizeDirection = direction.normalized;
        bulletRed.GetComponent<Rigidbody2D>().AddForce(nomalizeDirection * velocityBullet, ForceMode2D.Impulse);
        Destroy(bulletRed, 6f );

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Green")
            _enemyLives--;
    }

}
