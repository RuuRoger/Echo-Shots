using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //Serialized fields
        [SerializeField] private float m_speedPlayer;

        //Events
        public event Action<float, float> OnPlayerInput;

        //Private methods
        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            /*
                displacement = direction × sense × magnitude × Δt
            */

            //Move in X-Axis
            transform.Translate(
            Vector2.right *
            Input.GetAxis("Horizontal") *
            m_speedPlayer *
            Time.deltaTime);

            //Move in Y-Axis
            transform.Translate(
            Vector2.up *
            Input.GetAxis("Vertical") *
            m_speedPlayer *
            Time.deltaTime);

            OnPlayerInput?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
        
        
    }
}