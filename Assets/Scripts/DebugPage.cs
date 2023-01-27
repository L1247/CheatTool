#region

using UnityEngine;

#endregion

namespace DefaultNamespace
{
    public class DebugPage : MonoBehaviour
    {
    #region Unity events

        private void Start()
        {
            CheatTool.Instance.AddButton("Test1" , () => Debug.Log("1"));
            CheatTool.Instance.AddButton("Test2" , () => Debug.Log("2"));
            CheatTool.Instance.AddButton("Test3" , () => Debug.Log("3"));
            CheatTool.Instance.AddButton("Test4" , () => Debug.Log("4"));
        }

    #endregion
    }
}