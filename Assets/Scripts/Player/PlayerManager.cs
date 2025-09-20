using UnityEngine;
using Assets.Scripts.Manager;
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

        //Private fields
        public void LivesHandler()
        {
            m_playerLives--;

            if (m_playerLives <= 0)
            {
                m_playerLives = 0;
                FindAnyObjectByType<GameManager>().ForceUIUpdate();
                this.gameObject.SetActive(false);
            }
        }
    }
}