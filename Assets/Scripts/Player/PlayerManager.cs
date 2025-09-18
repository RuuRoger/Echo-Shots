using UnityEngine;
using Assets.Scripts.Prefabs;

namespace Assets.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        //Serializefields
        [SerializeField] private int m_playerLives;

        //Private fields

        public void LivesHandler()
        {
            m_playerLives--;
        }
    }
}