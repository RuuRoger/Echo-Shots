using UnityEngine;
using Assets.Scripts.Gun;
using Assets.Scripts.Prefabs;
using Assets.Scripts.Player;
using Assets.Scripts.Enemy;
using TMPro;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        //Serializfields
        [SerializeField] private Enemy.Enemy m_enemy;
        [SerializeField] private GameObject m_amunationPrefab;
        [SerializeField] private TextMeshProUGUI m_amunationNumberUI;
        [SerializeField] private TextMeshProUGUI m_playerLivesUI;
        [SerializeField] private TextMeshProUGUI m_winUI;
        [SerializeField] private TextMeshProUGUI m_loseUI;

        //Private Fields
        private Weapon m_gun;
        private PlayerManager m_player;
        private float m_randomX;
        private float m_randomY;

        //Private Methods
        private void Awake()
        {
            m_gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Weapon>();
            m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }

        private void Start()
        {
            m_gun.OnWithoutBullets += ShowAmunation;
            m_enemy.OnDestroyAllEnemyBullets += DestroyBullets;
            m_player.OnDestroyBulletsInScene += DestroyBullets;
        }

        void Update()
        {
            UIHandler();
            ShowUiText();
        }

        private void ShowAmunation()
        {
            m_randomX = Random.Range(-7.5f, 7.5f);
            m_randomY = Random.Range(-3.5f, 3.5f);
            Vector3 amunationPosition = new Vector3(m_randomX, m_randomY, 0f);

            GameObject amunationObject = GameObject.Instantiate(m_amunationPrefab, amunationPosition, Quaternion.identity);
            Amunation amunationScript = amunationObject.GetComponent<Amunation>();
            amunationScript.OnGetAmunation += m_gun.FullAmunation;
        }

        private void UIHandler()
        {
            m_amunationNumberUI.text = m_gun.BulletNumbers.ToString();
            m_playerLivesUI.text = m_player.PlayerLives.ToString();
        }

        public void ForceUIUpdate()
        {
            UIHandler();
        }

        private void DestroyBullets(bool value)
        {
            if (value)
            {
                GameObject[] greenBullets = GameObject.FindGameObjectsWithTag("Green");
                GameObject[] redBullets = GameObject.FindGameObjectsWithTag("Red");

                foreach (GameObject bullets in greenBullets)
                {
                    Destroy(bullets);
                }

                foreach (GameObject bullets in redBullets)
                {
                    Destroy(bullets);
                }

                ShowUiText();
            }
        }

        private void ShowUiText()
        {
            //Avoid displaying errors in console
            if (m_player == null || m_enemy == null)
            {
                return;
            }

            if (m_player.PlayerLives <= 0)
            {
                Destroy(m_player.gameObject);
                Destroy(m_enemy.gameObject);
                m_loseUI.gameObject.SetActive(true);
            }

            if (m_enemy.EnemyLives <= 0)
            {
                Destroy(m_player.gameObject);
                Destroy(m_enemy.gameObject);
                m_winUI.gameObject.SetActive(true);
            }

        }
    }
}