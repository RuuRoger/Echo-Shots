using UnityEngine;

namespace Assets.Scripts.Gun
{
    public class WeaponAnimation : MonoBehaviour
    {
        //Private Fields
        private Weapon m_weapon;
        private WeaponFlipController m_weaponFlip;
        private Animator m_weaponAnimator;

        private void Awake()
        {
            m_weapon = GetComponent<Weapon>();
            m_weaponFlip = GetComponent<WeaponFlipController>();
            m_weaponAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            m_weapon.OnNumberOfBullets += AnimationHandler;
        }

        private void AnimationHandler()
        {
            m_weaponAnimator.SetTrigger("Shoot");
        }

    }
}