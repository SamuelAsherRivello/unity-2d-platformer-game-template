using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RMC.MyProject.Scenes
{
    //  Namespace Properties ------------------------------


    //  Class Attributes ----------------------------------


    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene02_Game : MonoBehaviour
    {
        //  Events ----------------------------------------


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

        //  Unity Methods ---------------------------------

        protected void Start()
        {
            _livesStatLabel = _UIDocument.rootVisualElement.Q<VisualElement>("LivesStat").Q<Label>();
            _coinsStatLabel = _UIDocument.rootVisualElement.Q<VisualElement>("CoinsStat").Q<Label>();

            LivesCurrent = LivesDefault;
            CoinsCurrent = CoinsDefault;
        }

        protected void Update()
        {

            if(_player.deathState == true)
            {
                _player.gameObject.SetActive(false);
                GameObject deathPlayer = (GameObject)Instantiate(deathPlayerPrefab, _player.transform.position, _player.transform.rotation);
                deathPlayer.transform.localScale = new Vector3(_player.transform.localScale.x, _player.transform.localScale.y, _player.transform.localScale.z);
                _player.deathState = false;
                Invoke("ReloadLevel", 3);
            }
        }



        //  Methods ---------------------------------------
        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //  Event Handlers --------------------------------
    }
}

