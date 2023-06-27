using UnityEngine;

namespace RMC.Platformer
{
    /// <summary>
    /// Store data to persist across scene loads
    /// </summary>
    public class PersistentGameData : MonoBehaviour
    {
        /// <summary>
        /// Use static here to persist across scene loads
        /// </summary>
        public static int LivesCurrent = LivesMax; 
        public const int LivesMax = 3;
    }
}

