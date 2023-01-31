#region

using UnityEngine;

#endregion

namespace rStart.UnityCommandPanel
{
    public class DebugPageController : MonoBehaviour
    {
    #region Unity events

        private void Start()
        {
            CommandPanel.Instance.AddPage<DebugPage>();
        }

    #endregion
    }
}