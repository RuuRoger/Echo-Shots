using UnityEngine;
using TMPro;
using Assets.Scripts.Prefabs;
using Assets.Scripts.Player;
using System;

namespace Assets.Scripts.Enemy
    {
    public class Enemy : MonoBehaviour
    {
        //Public properties
        public GameObject bulleetPrefabEnemy;
        public Transform enemyBullets;
        public Transform playerTransform;
        public float speedEnemy;
        public float velocityBullet;

        // public TextMeshProUGUI uiWin;
        public TextMeshProUGUI m_uiEnemyLives;

        //private fields
        private Vector2 _randomEnemyPosition;
        private int[] _randomPositions;
        private int _randomIndexX;
        private int _randomIndexY;
        private int _enemyLives;
        private float _nextShotTime;
        private float _shootInterval;
        private PlayerManager m_playerManager;

        //Public properties
        public int EnemyLives
        {
            get
            {
                return _enemyLives;
            }
        }

        //Events
        public event Action<bool> OnDestroyAllEnemyBullets;

        //Meethods
        private void Awake()
        {
            m_playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }

        private void Start()
        {
            //Array with enemy positions to start in game
            _randomPositions = new int[] { -1, 1 };

            //Choose first direction
            _randomIndexX = UnityEngine.Random.Range(0, _randomPositions.Length);
            _randomIndexY = UnityEngine.Random.Range(0, _randomPositions.Length);
            _randomEnemyPosition = new Vector2(_randomPositions[_randomIndexX], _randomPositions[_randomIndexY]);


            //Movement
            GetComponent<Rigidbody2D>().AddForce(_randomEnemyPosition * speedEnemy, ForceMode2D.Impulse);

            //Time and interval to shoot
            _shootInterval = 3f;
            _nextShotTime = Time.time + _shootInterval;

            //Enemy lives
            _enemyLives = 2;

        }

        private void Update()
        {
            //UI
            m_uiEnemyLives.text = _enemyLives.ToString();

            //Time and interval to shoot
            if (Time.time >= _nextShotTime)
            {
                EnemyShoots();
                _nextShotTime = Time.time + _shootInterval;
            }

            Die();
        }

        private void EnemyShoots()
        {
            GameObject bulletRed = GameObject.Instantiate(bulleetPrefabEnemy, enemyBullets.position, enemyBullets.rotation);
            Vector3 direction = playerTransform.position - bulletRed.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletRed.transform.rotation = Quaternion.Euler(0, 0, angle);
            Vector2 nomalizeDirection = direction.normalized;
            bulletRed.GetComponent<Rigidbody2D>().AddForce(nomalizeDirection * velocityBullet, ForceMode2D.Impulse);

            Bullet bulletScript = bulletRed.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.OnHitPlayer += m_playerManager.LivesHandler;
            }

            Destroy(bulletRed, 6f);

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Green")
                _enemyLives--;
        }

        private void Die()
        {
            if (_enemyLives <= 0)
            {
                OnDestroyAllEnemyBullets?.Invoke(true);                
                Invoke("DestroyEnemy", 0.1f);
            }
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }

}

