using RMC.Core.Audio;
using UnityEngine;

namespace RMC.Platformer
{
    //  Namespace Properties ------------------------------


    //  Class Attributes ----------------------------------


    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Player : MonoBehaviour
    {
        
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------

        [SerializeField]
        public float _movingSpeed = 6;
        
        [SerializeField]
        public float _jumpForce = 7;

        [SerializeField]
        private Transform _groundCheck;

        [SerializeField]
        private Rigidbody2D _rigidbody;
        
        [SerializeField]
        private Animator _animator;
        
        [SerializeField]
        private Scene02_Game _scene02_Game;

        [HideInInspector]
        public bool DeathState = false;

        private float _moveInput;
        private bool _isFacingRight = false;
        private bool _isGrounded;

        //  Unity Methods ---------------------------------
        protected void Start()
        {
        }

        protected void FixedUpdate()
        {
            CheckGround();
        }

        protected void Update()
        {
            if (Input.GetButton("Horizontal")) 
            {
                _moveInput = Input.GetAxis("Horizontal");
                Vector3 direction = transform.right * _moveInput;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _movingSpeed * Time.deltaTime);
                _animator.SetInteger("playerState", 1); // Turn on run animation
            }
            else
            {
                if (_isGrounded) _animator.SetInteger("playerState", 0); // Turn on idle animation
            }
            if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && _isGrounded )
            {
                _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
                AudioManager.Instance.PlayAudioClip("PlayerJump01");
            }
            if (!_isGrounded)_animator.SetInteger("playerState", 2); // Turn on jump animation

            if(_isFacingRight == false && _moveInput > 0)
            {
                Flip();
            }
            else if(_isFacingRight == true && _moveInput < 0)
            {
                Flip();
            }
        }
        

        //  Methods --------------------------------------
        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.transform.position, 0.2f);
            _isGrounded = colliders.Length > 1;
        }

        //  Event Handlers --------------------------------
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                DeathState = true; // Say to GameManager that player is dead
                AudioManager.Instance.PlayAudioClip("PlayerDamage01");
            }
            else
            {
                DeathState = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin")
            {
                _scene02_Game.CoinsCurrent += 1;
                AudioManager.Instance.PlayAudioClip("PlayerCollect01");
                Destroy(other.gameObject);
            }
        }
    }
}
