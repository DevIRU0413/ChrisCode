using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitType
{
    MoveSpeed,
    MaxHealth,
    AttackPower,
    AttackSpeed,
    ExtraProjectile,  // �߰� �߻�ü
    ProjectileSize,   // �߻�ü ũ�� ����
    Pierce,           // �߻�ü ���� ����
    Explosion,        // �߻�ü ���߽� ����
    PassiveMissile,   // �ڵ� ����  
    InvincibleOnLowHP,// ü�� ���� �� �Ͻ��� ����
    HealOnKill,       // �� óġ�� ü�� ȸ��  
    ExpBoost,         // ����ġ ȹ�淮 ����  
    CooldownReduction,  // ��ų ��Ÿ�� ����
    HealInkill,       // ���� óġ �� ȸ��

}
