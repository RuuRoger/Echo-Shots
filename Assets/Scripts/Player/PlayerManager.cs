using UnityEngine;
using Assets.Scripts.Prefabs;

namespace Assets.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        //Serializefields
        [SerializeField] private int m_playerLives;

        //Private fields

        //! It's only provisional
        public void LivesHandler()
        {
            m_playerLives--;

            if (m_playerLives <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}