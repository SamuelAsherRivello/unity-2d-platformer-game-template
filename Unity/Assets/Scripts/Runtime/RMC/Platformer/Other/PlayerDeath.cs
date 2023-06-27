using UnityEngine;

namespace RMC.Platformer
{
    /// <summary>
    /// Animates the player upon death
    /// </summary>
    public class PlayerDeath : MonoBehaviour
    {
        public float jumpForce;

        private new Rigidbody2D rigidbody;
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}

