using System;
using UnityEngine;

namespace Assets.Scripts.Prefabs
{
    public class Bullet : MonoBehaviour
    {
        //Events
        public event Action OnHitPlayer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnHitPlayer?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}