#region

using UnityEngine;

#endregion

namespace rStart.UnityCheatTool
{
    public class DebugPageController : MonoBehaviour
    {
    #region Unity events

        private void Start()
        {
            CheatTool.Instance.AddPage<DebugPage>();
        }

    #endregion
    }
}