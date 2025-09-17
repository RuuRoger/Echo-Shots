using UnityEngine;
using Assets.Scripts.Prefabs;

namespace Assets.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        //Serializefields
        [SerializeField] private int m_playerLives;

        //Private fields

        private void Update()
        {
            Debug.Log(m_playerLives);
        }

        public void LivesHandler()
        {
            m_playerLives--;
        }
    }
}