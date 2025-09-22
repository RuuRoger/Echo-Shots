using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAudio : MonoBehaviour
    {
        //Serializefields
        [SerializeField] private AudioClip m_clip;

        //Private fields
        private Enemy m_enemy;
        private AudioSource m_audioSource;

        private void Awake()
        {
            m_enemy = GetComponent<Enemy>();
            m_audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            m_audioSource.clip = m_clip;
        }

        private void OnEnable()
        {
            m_enemy.onEnemyShoot += ShootEnemyAudio;
        }

        private void OnDisable()
        {
            m_enemy.onEnemyShoot -= ShootEnemyAudio;
        }

        private void ShootEnemyAudio()
        {
            m_audioSource.Play();
        }
    }
}