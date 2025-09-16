using UnityEngine;
using Assets.Scripts.Player;
using System;

namespace Assets.Scripts.Gun
{
    public class Weapon : MonoBehaviour
    {
        //Serializefield
        [SerializeField] GameObject m_bulletPrefab;
        [SerializeField] GameObject m_amunationPrefab;
        [SerializeField] float m_speedBullet;

        //Private fields
        private PlayerMovement m_player;
        private Transform m_gunBarrel;
        private Animator m_gunAnimator;
        private SpriteRenderer m_gunRender;
        private byte m_bulletNumbers;

        //Public Properties
        public byte BulletNumbers
        {
            get
            {
                return m_bulletNumbers;
            }
        }

        //Events
        public event Action OnNumberOfBullets;

        //Private Methods
        private void Awake()
        {
            m_gunBarrel = GetComponentInChildren<Transform>();
            m_gunAnimator = GetComponent<Animator>();
            m_gunRender = GetComponent<SpriteRenderer>();
            m_bulletNumbers = 12;
        }

        private void Update()
        {
            GunAnimation();
            Debug.Log($"tienes {m_bulletNumbers} balas");
        }

        private void GunAnimation()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_bulletNumbers >= 1)
                {
                    OnNumberOfBullets?.Invoke();
                    m_bulletNumbers--;
                }
            }
        }
    }
}