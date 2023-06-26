using RMC.Core.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RMC.Platformer
{
    /// <summary>
    /// Demonstrates the in-game player experience and UI
    /// </summary>
    public class Scene02_Game : MonoBehaviour
    {
        //  Properties ------------------------------------
        public int CoinsCurrent
        {
            get
            {
                return _coinsCurrent;
            }
            set
            {
                _coinsCurrent = value;
                _coinsStatLabel.text = _coinsCurrent.ToString();
            }
        }
        
        public int LivesCurrent
        {
            get
            {
                return _livesCurrent;
            }
            set
            {
                _livesCurrent = value;
                _livesStatLabel.text = _livesCurrent.ToString();
            }
        }


        //  Fields ----------------------------------------
        [SerializeField]
        private UIDocument _UIDocument;
        

        [SerializeField]
        private Player _player;
        
        private const int LivesDefault = 3;
        private const int CoinsDefault = 0;
        private int _livesCurrent;
        private int _coinsCurrent;
        private Label _livesStatLabel;
        private Label _coinsStatLabel;
        private Button _restartGameButton;

        //  Unity Methods ---------------------------------

        protected void Start()
        {
            _livesStatLabel = _UIDocument.rootVisualElement.Q<VisualElement>("LivesStat").Q<Label>();
            _coinsStatLabel = _UIDocument.rootVisualElement.Q<VisualElement>("CoinsStat").Q<Label>();
            _restartGameButton = _UIDocument.rootVisualElement.Q<Button>("RestartGameButton");

            LivesCurrent = LivesDefault;
            CoinsCurrent = CoinsDefault;
            
            //
            _restartGameButton.RegisterCallback<ClickEvent>(RestartGameButton_OnClicked);
            _player.OnCoinCollision.AddListener(Player_OnCoinCollision);
            _player.OnEnemyCollision.AddListener(Player_OnEnemyCollision);
            
        }


        //  Methods ---------------------------------------
        private void ReloadCurrentScene()
        {
            SceneManager.LoadScene("Scene02_Game");
        }
        

        //  Event Handlers --------------------------------
        private void Player_OnCoinCollision(Player player)
        {
            CoinsCurrent++;
            AudioManager.Instance.PlayAudioClip("PlayerCollect01");
        }
        
        private void Player_OnEnemyCollision(Player player)
        {
            AudioManager.Instance.PlayAudioClip("PlayerDamage01");
            
            _player.gameObject.SetActive(false);
            GameObject deathPlayer = (GameObject)Instantiate(player.DeathPlayerPrefab, _player.transform.position, _player.transform.rotation);
            deathPlayer.transform.localScale = new Vector3(_player.transform.localScale.x, _player.transform.localScale.y, _player.transform.localScale.z);
            Invoke("ReloadCurrentScene", 3);
        }

        private void RestartGameButton_OnClicked(ClickEvent clickEvent)
        {
            AudioManager.Instance.PlayAudioClip("UIClickYes01");

            SceneManager.LoadScene("Scene01_Intro");
        }
    }
}

