using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class AudioManager : MonoBehaviour
    {
        //Serializefileds
        [SerializeField] private AudioClip[] m_song;
        [SerializeField] private AudioClip m_winClip;
        [SerializeField] private AudioClip m_loseClip;

        //Private Methods
        private AudioSource m_audioSource;
        private GameManager m_gameManager;
        private bool m_stopSong;

        private void Awake()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_gameManager = GetComponent<GameManager>();
            m_stopSong = false;
        }

        private void Start()
        {
            RandomSong();
        }

        private void Update()
        {
            if (!m_audioSource.isPlaying && !m_stopSong)
            {
                RandomSong();
            }
        }

        private void OnEnable()
        {
            m_gameManager.OnWinSong += WinSong;
            m_gameManager.OnGameOverSong += GameOverSong;
        }

        private void OnDisable()
        {
            m_gameManager.OnWinSong -= WinSong;
            m_gameManager.OnGameOverSong -= GameOverSong;
        }

        private void RandomSong()
        {
            int index = UnityEngine.Random.Range(0, m_song.Length);
            AudioClip randomClip = m_song[index];
            m_audioSource.clip = randomClip;
            m_audioSource.Play();
        }

        private void WinSong()
        {
            m_audioSource.clip = m_winClip;
            m_audioSource.volume = 0.6f;
            m_audioSource.loop = false;
            m_audioSource.Play();
            m_stopSong = true;
        }

        private void GameOverSong()
        {
            m_audioSource.clip = m_loseClip;
            m_audioSource.volume = 0.6f;
            m_audioSource.loop = false;
            m_audioSource.Play();
            m_stopSong = true;
        }

    }

}