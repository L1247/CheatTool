#region

using UnityEngine;

#endregion

namespace DefaultNamespace
{
    public class CheatTool : MonoBehaviour
    {
    #region Public Variables

        public static CheatTool Instance { get; private set; }

    #endregion

    #region Unity events

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

    #endregion
    }
}