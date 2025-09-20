using UnityEngine;
using Assets.Scripts.Gun;
using Assets.Scripts.Prefabs;
using Assets.Scripts.Player;
using TMPro;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        //Serializfields
        [SerializeField] private GameObject m_amunationPrefab;
        [SerializeField] private TextMeshProUGUI m_amunationNumberUI;
        [SerializeField] private TextMeshProUGUI m_playerLivesUI;

        //Private Fields
        private Weapon m_gun;
        private PlayerManager m_player;
        private float m_randomX;
        private float m_randomY;

        //Private Methods
        private void Awake()
        {
            m_gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Weapon>();
        }

        void Update()
        {
            m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            UIHandler();
        }

        private void OnEnable()
        {
            m_gun.OnWithoutBullets += ShowAmunation;
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

    }    
}