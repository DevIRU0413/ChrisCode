using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeHitbox : MonoBehaviour
{
    [SerializeField] private Collider m_weaponCollider;
    [SerializeField] private MonsterBase m_monsterBase;
    void Awake()
    {
        m_weaponCollider.enabled = false;
        m_monsterBase = GetComponentInParent<MonsterBase>();
    }

    public void EnableHitbox()
    {
        m_weaponCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        m_weaponCollider.enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHp>().TakeDamage(m_monsterBase.AttackDMG);
            Debug.Log($"{m_monsterBase.gameObject.name}이 플레이어에게 {m_monsterBase.AttackDMG} 데미지를 입힘");

        }
    }

  
}
