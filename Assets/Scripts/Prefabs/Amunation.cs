using System;
using UnityEngine;

namespace Assets.Scripts.Prefabs
{
    public class Amunation : MonoBehaviour
    {
        //Events
        public event Action OnGetAmunation;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnGetAmunation?.Invoke();
                Destroy(gameObject, 0.2f);
            }
        }
    }
}