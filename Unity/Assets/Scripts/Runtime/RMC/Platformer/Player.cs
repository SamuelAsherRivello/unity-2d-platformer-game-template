using RMC.Core.Audio;
using UnityEngine;
using UnityEngine.Events;

namespace RMC.Platformer
{
    //  Namespace Properties ------------------------------


    //  Class Attributes ----------------------------------
    public class PlayerUnityEvent : UnityEvent<Player> {}

    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Player : MonoBehaviour
    {
        
        //  Events ----------------------------------------
        [HideInInspector]
        public PlayerUnityEvent OnCoinCollision = new PlayerUnityEvent();
        
        [HideInInspector]
        public PlayerUnityEvent OnEnemyCollision = new PlayerUnityEvent();

        //  Properties ------------------------------------
        public GameObject DeathPlayerPrefab { get { return _deathPlayerPrefab;}}

        //  Fields ----------------------------------------

        [SerializeField]
        private GameObject _deathPlayerPrefab;

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
            if (other.gameObject.CompareTag("Enemy"))
            {
                OnEnemyCollision.Invoke(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                OnCoinCollision.Invoke(this);
                Destroy(other.gameObject);
            }
        }
    }
}
