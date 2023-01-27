#region

using UnityEngine;

#endregion

namespace DefaultNamespace
{
    public class DebugPage : PageBase
    {
    #region Unity events

        private void Start()
        {
            AddButton("Test1" , () => Debug.Log("1"));
            AddButton("Test2" , () => Debug.Log("2"));
            AddButton("Test3" , () => Debug.Log("3"));
            AddButton("Test4" , () => Debug.Log("4"));
        }

    #endregion
    }
}