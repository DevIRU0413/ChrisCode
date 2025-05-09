using Scripts.Interface;
using Scripts.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Scene
{
    public class InGameScene : SceneBase
    {
        public override SceneID SceneID => SceneID.InGame;

        protected override void Initialize()
        {
            GameObject go = GameObject.FindWithTag("Player");
            MonsterSpawner.Instance.OnMonsterDieAction -= go.GetComponent<PlayerExperience>().GainExp;
            MonsterSpawner.Instance.OnMonsterDieAction += go.GetComponent<PlayerExperience>().GainExp;
        }
    }
}