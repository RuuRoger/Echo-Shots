using UnityEngine;

namespace Assets.Scripts.Gun
{
    public class WeaponAudio : MonoBehaviour
    {
        //Serializefield
        [SerializeField] private AudioClip m_clip;

        //private fields
        private AudioSource m_audisource;
        private Weapon m_gun;

        //Private methods
        private void Awake()
        {
            m_audisource = GetComponent<AudioSource>();
            m_gun = GetComponent<Weapon>();
            m_audisource.clip = m_clip;
        }

        private void OnEnable()
        {
            m_gun.OnShoot += ShootAudio;
        }

        private void OnDisable()
        {
            m_gun.OnShoot -= ShootAudio;
        }

        private void ShootAudio()
        {
            m_audisource.Play();
        }
    }
}