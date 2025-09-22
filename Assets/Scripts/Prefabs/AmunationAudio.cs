using UnityEngine;

namespace Assets.Scripts.Prefabs
{
    public class AmunationAudio : MonoBehaviour
    {
        //serializefields
        [SerializeField] private AudioClip m_clip;

        //Private fields
        private Amunation m_amunation;
        private AudioSource m_audiosource;

        private void Awake()
        {
            m_amunation = GetComponent<Amunation>();
            m_audiosource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            m_audiosource.clip = m_clip;
        }

        private void OnEnable()
        {
            m_amunation.OnGetAmunation += GetAmunationNoise;
        }

        private void OnDisable()
        {
            m_amunation.OnGetAmunation -= GetAmunationNoise;
        }

        private void GetAmunationNoise()
        {
            m_audiosource.Play();
        }

    }
}