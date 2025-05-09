using Scripts.Interface;
using Scripts.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Scene
{
    public class InGameScene : SceneBase
    {
        public override SceneID SceneID => SceneID.InGame;


        // 필요 리소드 데이터들
        public override void Initialize()
        {
            base.Initialize();
            GameObject go = GameObject.FindWithTag("Player");
            MonsterSpawner.Instance.OnMonsterDieAction -= go.GetComponent<PlayerExperimence>().GainExp;
            MonsterSpawner.Instance.OnMonsterDieAction += go.GetComponent<PlayerExperimence>().GainExp;
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