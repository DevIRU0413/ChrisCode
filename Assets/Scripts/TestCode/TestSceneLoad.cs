using Scripts.Manager;
using UnityEngine;

namespace Scripts.TestCode
{
    public class TestSceneLoad : MonoBehaviour
    {
        public int TeetNum = 1;

        void Start()
        {
            switch (TeetNum)
            {
                case 0:
                    SceneManagerEx.Instance.LoadSceneAsync("InGameScene");
                    break;
                case 1:
                    SceneManagerEx.Instance.LoadSceneWithFade("InGameScene");
                    break;
                case 2:
                    SceneManagerEx.Instance.LoadSceneAsyncWithLoading("InGameScene");
                    break;
                case 3:
                    SceneManagerEx.Instance.UnloadSceneAsync("InGameScene");
                    break;
            }
        }
    }
}
