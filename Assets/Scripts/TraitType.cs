using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitType
{
    MoveSpeed,
    MaxHealth,
    AttackPower,
    AttackSpeed,
    ExtraProjectile,  // 추가 발사체
    ProjectileSize,   // 발사체 크기 증가
    Pierce,           // 발사체 관통 증가
    Explosion,        // 발사체 명중시 폭발
    PassiveMissile,   // 자동 공격  
    InvincibleOnLowHP,// 체력 낮을 때 일시적 무적
    HealOnKill,       // 적 처치시 체력 회복  
    ExpBoost,         // 경험치 획득량 증가  
    CooldownReduction,  // 스킬 쿨타임 감소

}
