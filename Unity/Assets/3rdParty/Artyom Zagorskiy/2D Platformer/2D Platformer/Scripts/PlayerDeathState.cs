using UnityEngine;

namespace Platformer
{
    public class PlayerDeathState : MonoBehaviour
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

