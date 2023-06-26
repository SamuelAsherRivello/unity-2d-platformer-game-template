using RMC.Core.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RMC.Platformer
{
    /// <summary>
    /// Demonstrates the intro menu player experience and UI
    /// </summary>
    public class Scene01_Intro : MonoBehaviour
    {
        //  Properties ------------------------------------

        //  Fields ----------------------------------------
        [SerializeField]
        private UIDocument _UIDocument;
        
        private Button _playGameButton;
        private Button _viewSettingsButton;

        //  Unity Methods ---------------------------------
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");

            //
            _playGameButton = _UIDocument.rootVisualElement.Q<Button>("PlayGameButton");
            _viewSettingsButton = _UIDocument.rootVisualElement.Q<Button>("ViewSettingsButton");
            
            //
            _playGameButton.RegisterCallback<ClickEvent>(PlayGameButton_OnClicked);
            _viewSettingsButton.RegisterCallback<ClickEvent>(ViewSettingsButton_OnClicked);
        }




        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
        private void PlayGameButton_OnClicked(ClickEvent evt)
        {
            AudioManager.Instance.PlayAudioClip("UIClickYes01");
            
            SceneManager.LoadScene("Scene02_Game");
        }
        
        private void ViewSettingsButton_OnClicked(ClickEvent evt)
        {
            AudioManager.Instance.PlayAudioClip("UIClickNo01");
            
            Debug.Log("TODO: This scene does not exist yet");
            
        }
    }
}