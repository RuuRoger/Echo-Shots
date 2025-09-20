using UnityEngine;
using Assets.Scripts.Manager;
using System;
namespace Assets.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        //Serializefields
        [SerializeField] private int m_playerLives;

        //Public properties
        public int PlayerLives
        {
            get
            {
                return m_playerLives;
            }
        }

        //Events
        public event Action<bool> OnDestroyBulletsInScene;

        //Private Mmethods
        public void LivesHandler()
        {
            m_playerLives--;

            if (m_playerLives <= 0)
            {
                m_playerLives = 0;
                FindAnyObjectByType<GameManager>().ForceUIUpdate();
                OnDestroyBulletsInScene?.Invoke(true);
                this.gameObject.SetActive(false);
            }
        }

    }
}