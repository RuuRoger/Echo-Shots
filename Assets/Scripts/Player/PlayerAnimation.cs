using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        //private fields
        private PlayerMovement m_playerMovement;
        private Animator m_playerAnimation;
        private SpriteRenderer m_playerSpriteRender;

        //Events
        public event Action<int> OnPlayerFlip;

        //Private methods
        private void Awake()
        {
            m_playerMovement = GetComponent<PlayerMovement>();
            m_playerAnimation = GetComponent<Animator>();
            m_playerSpriteRender = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            m_playerMovement.OnPlayerInput += AnimationHandler;
        }

        private void OnDisable()
        {
            m_playerMovement.OnPlayerInput -= AnimationHandler;
        }

        private void AnimationHandler(float horizontal, float vertical)
        {
            if (horizontal == 0f && vertical == 0f) m_playerAnimation.SetBool("Walk", false);
            else m_playerAnimation.SetBool("Walk", true);

            if (horizontal > 0)
            {
                m_playerSpriteRender.flipX = false;
                OnPlayerFlip?.Invoke(1);
            }

            if (horizontal < 0)
            {
                m_playerSpriteRender.flipX = true;
                OnPlayerFlip?.Invoke(-1);
            }
        }
    }
}