using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.MyProject.Scenes
{
    //  Namespace Properties ------------------------------


    //  Class Attributes ----------------------------------


    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene01_Intro : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------

        //  Fields ----------------------------------------
        [SerializeField]
        private UIDocument _UIDocument;

        //  Unity Methods ---------------------------------
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
        }

        //  Methods ---------------------------------------
        public string SamplePublicMethod(string message)
        {
            return message;
        }


        //  Event Handlers --------------------------------
        public void Target_OnCompleted(string message)
        {

        }
    }
}