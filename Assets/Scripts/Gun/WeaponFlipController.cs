using UnityEngine;
using Assets.Scripts.Player;
using System;

namespace Assets.Scripts.Gun
{
    public class WeaponFlipController : MonoBehaviour
    {
        private PlayerAnimation m_playerAnimation;
        private WeaponFlipController m_weaponFlipController;
        private Transform m_gunBarrel;
        private SpriteRenderer m_gunRenderer;
        private bool m_gunIsWithFlip;

        //Public Properties
        public bool Flip
        {
            get
            {
                return m_gunIsWithFlip;
            }
        }

        //Event
        public event Action<int> OnFlip;

        private void Awake()
        {
            m_playerAnimation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>();
            m_weaponFlipController = GameObject.FindGameObjectWithTag("Gun").GetComponent<WeaponFlipController>();
            m_gunRenderer = GetComponent<SpriteRenderer>();
            m_gunIsWithFlip = false;
            m_gunBarrel = transform.GetChild(0);
        }

        private void OnEnable()
        {
            m_playerAnimation.OnPlayerFlip += PositionGun;
        }

        private void Update()
        {
            MoveGun();
        }

        private void MoveGun()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 direction = mousePosition - transform.position;
            
            if (m_gunIsWithFlip)
            {
                direction = direction * (-1);
            }
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }

        private void PositionGun(int value)
        {
            Vector3 newPosition = new Vector3(m_playerAnimation.transform.position.x + (0.7f * value), m_playerAnimation.transform.position.y - 0.3f, transform.position.z);
            transform.position = newPosition;
            m_gunBarrel.transform.localPosition = new Vector3(0.407f * value, 0.141f, 0);

            if (value == 1)
            {
                m_gunIsWithFlip = false;
                m_gunRenderer.flipX = false;
                OnFlip?.Invoke(value);
            }

            if (value == -1)
            {
                m_gunIsWithFlip = true;
                m_gunRenderer.flipX = true;
                OnFlip?.Invoke(value);
            }
        }
    }
}