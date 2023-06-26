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
        private GameObject deathPlayerPrefab;
        
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
        }


        protected void Update()
        {
            if(_player.DeathState == true)
            {
                _player.gameObject.SetActive(false);
                GameObject deathPlayer = (GameObject)Instantiate(deathPlayerPrefab, _player.transform.position, _player.transform.rotation);
                deathPlayer.transform.localScale = new Vector3(_player.transform.localScale.x, _player.transform.localScale.y, _player.transform.localScale.z);
                _player.DeathState = false;
                Invoke("ReloadCurrentScene", 3);
            }
        }



        //  Methods ---------------------------------------
        private void ReloadCurrentScene()
        {
            SceneManager.LoadScene("Scene02_Game");
        }
        

        //  Event Handlers --------------------------------
        private void RestartGameButton_OnClicked(ClickEvent evt)
        {
            AudioManager.Instance.PlayAudioClip("UIClickYes01");

            SceneManager.LoadScene("Scene01_Intro");
        }
    }
}

