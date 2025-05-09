using Scripts.UI;
using UnityEngine;

namespace Scripts.Scene
{
    public class TitleScene : SceneBase
    {
        public override SceneID SceneID => SceneID.Title;

        public override void Initialize()
        {
            base.Initialize();
            
        }

        public override void LoadManagers()
        {
            base.LoadManagers();
            GameObject[] SubscribeManagers = GameObject.FindGameObjectsWithTag("Manager");

            ManagerGroup.Instance.RegisterManager(SubscribeManagers);
            ManagerGroup.Instance.InitializeManagers();
        }
    }
}