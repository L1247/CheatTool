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
            // CommandPanel.Instance.AddPage<ExamplePage1>();
            var rootPage = CommandPanel.Instance.GetOrCreateInitialPage();
            rootPage.AddPageLinkButton<ExamplePage1>(nameof(ExamplePage1));
        }

    #endregion
    }
}