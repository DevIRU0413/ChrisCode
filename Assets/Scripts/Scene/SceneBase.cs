using UnityEngine;

namespace Scripts.Scene
{
    public abstract class SceneBase : MonoBehaviour
    {
        public abstract SceneID SceneID { get; }

        protected void Awake()
        {
            Initialize();
            LoadManagers();
        }

        public virtual void Initialize()
        {
            Debug.Log($"Scene {SceneID} Initialized.");
        }

        public virtual void LoadManagers()
        {
            Debug.Log($"Scene {SceneID} Load Managers.");
        }
    }

}