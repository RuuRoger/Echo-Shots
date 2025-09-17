using UnityEngine;
using Assets.Scripts.Player;
using Assets.Scripts.Prefabs;

namespace Assets.Scripts.Gun
{
    public class WeaponShoot : MonoBehaviour
    {
        //Serializefield
        [SerializeField] private GameObject m_bulletPrefab;
        [SerializeField] private float m_speedBullet;

        //Private Fields
        private Weapon m_weapon;
        private WeaponFlipController m_weaponFlipController;
        private Transform m_gunBarrel;
        private PlayerMovement m_player;
        private PlayerManager m_playerManager;
        private bool m_isFlipped;

        private void Awake()
        {
            m_weapon = GetComponent<Weapon>();
            m_weaponFlipController = GetComponent<WeaponFlipController>();
            m_gunBarrel = transform.GetChild(0);
            m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            m_playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }

        private void OnEnable()
        {
            m_weaponFlipController.OnFlip += UpdateFlipState;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && m_weapon.BulletNumbers > 0)
            {
                Shoot();
            }
        }

        private void UpdateFlipState(int value)
        {
            m_isFlipped = (value == -1);
        }

        private void Shoot()
        {
            float directionMultiplier = m_isFlipped ? -1f : 1f;
            
            GameObject bullet = GameObject.Instantiate(m_bulletPrefab, m_gunBarrel.position, m_gunBarrel.rotation);
            bullet.transform.position += new Vector3(0.2f * directionMultiplier, 0f, 0f);
            bullet.GetComponent<Rigidbody2D>().AddForce(m_gunBarrel.right * directionMultiplier * m_speedBullet, ForceMode2D.Impulse);
            
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.OnHitPlayer += m_playerManager.LivesHandler;
            }
            
            Destroy(bullet, 10f);
        }
    }
}