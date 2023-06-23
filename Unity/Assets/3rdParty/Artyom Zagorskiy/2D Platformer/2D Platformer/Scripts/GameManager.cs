using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Platformer
{
    /// <summary>
    /// 
    /// </summary>
    public class GameManager : MonoBehaviour
    {

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

        [SerializeField]
        private UIDocument _UIDocument;
        
        [SerializeField]
        private GameObject deathPlayerPrefab;
        
        [SerializeField]
        private PlayerController _playerController;
        
        private const int LivesDefault = 3;
        private const int CoinsDefault = 0;
        private int _livesCurrent;
        private int _coinsCurrent;
        private Label _livesStatLabel;
        private Label _coinsStatLabel;

        protected void Start()
        {
            _livesStatLabel = _UIDocument.rootVisualElement.Q<VisualElement>("LivesStat").Q<Label>();
            _coinsStatLabel = _UIDocument.rootVisualElement.Q<VisualElement>("CoinsStat").Q<Label>();

            LivesCurrent = LivesDefault;
            CoinsCurrent = CoinsDefault;
        }

        protected void Update()
        {

            if(_playerController.deathState == true)
            {
                _playerController.gameObject.SetActive(false);
                GameObject deathPlayer = (GameObject)Instantiate(deathPlayerPrefab, _playerController.transform.position, _playerController.transform.rotation);
                deathPlayer.transform.localScale = new Vector3(_playerController.transform.localScale.x, _playerController.transform.localScale.y, _playerController.transform.localScale.z);
                _playerController.deathState = false;
                Invoke("ReloadLevel", 3);
            }
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
