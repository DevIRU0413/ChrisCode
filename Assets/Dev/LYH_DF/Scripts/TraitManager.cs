using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
    private CharacterMove characterMove; // ��ä���� ���� �ɷ�ġ ��ũ��Ʈ ���� �ݿ� 
    private PlayerHp playerHP; // ��ä���� ���� PlayerHP.cs ���� �ݿ�
    private NewBehaviourScript playerAttack;  //*���� �����ʿ�

    // Ư�� ȿ�� ������ ���� ��ųʸ�
    private Dictionary<TraitType, System.Action<float>> traitEffects;
    // ��Ÿ�� üũ�� ��ųʸ�
    private Dictionary<TraitType, float> lastActivatedTime = new Dictionary<TraitType, float>();

    public Dictionary<TraitType, Trait> acquiredTraits = new Dictionary<TraitType, Trait>();

    private void Start()
    {
        characterMove = FindObjectOfType<CharacterMove>(); // �÷��̾� �̵��ӵ� ��ũ��Ʈ ã��
        playerHP = FindObjectOfType<PlayerHp>(); // �÷��̾� HP ��ũ��Ʈ ã��
        playerAttack = FindAnyObjectByType<NewBehaviourScript>(); //* ���� �����ʿ� / �÷��̾� ���� ��ũ��Ʈ ã��

        SetupTraitEffects(); // Ư�� ȿ�� ��ųʸ� �ʱ�ȭ
    }

    private void SetupTraitEffects()
    {
        traitEffects = new Dictionary<TraitType, System.Action<float>>
        {
            {
                TraitType.MoveSpeed, (value) =>
                {
                    if (characterMove != null)
                    {
                        characterMove.moveSpeed *= value;
                        Debug.Log($"�̵��ӵ� ����! ���� �̵��ӵ� : {characterMove.moveSpeed}");
                    }
                }
            },

            {
                TraitType.MaxHealth, (value) =>
                {
                    if (playerHP != null)
                    {
                        playerHP.m_MaxHealth = Mathf.RoundToInt(playerHP.m_MaxHealth * value);
                        Debug.Log($"�ִ�ü�� ����! ���� �ִ�ü�� : {playerHP.m_MaxHealth}");
                    }
                }
            },

            {
                TraitType.AttackPower, (value) =>
                {
                    Debug.Log($"���ݷ� ����! {value}�� ����");
                }
            },

            {
                TraitType.AttackSpeed, (value) =>
                {
                    Debug.Log($"���ݼӵ� ����! {value}�� ����");
                }
            }
        };
    }

    public void ApplyTrait(Trait trait)
    {
        if (traitEffects.TryGetValue(trait.type, out var effect))
        {
            effect.Invoke(trait.value);
            Debug.Log($"Ư�� ����! {trait.traitName} ����");
        }
        else
        {
            Debug.LogWarning($"Ư���� �̵�ϻ��� {trait.traitName}�� traitEffects�� ��ϵ��� ����");
        }

        // ���� ��ȭ ���θ� Ȯ���ϰ� ��ųʸ��� ó���Ѵ�.
        if (acquiredTraits.ContainsKey(trait.type))
        {
            var prev = acquiredTraits[trait.type];
            float newValue = prev.value * trait.value;
            acquiredTraits[trait.type].value = newValue;

            Debug.Log($"��ȭ��� {trait.traitName} �̹� ����, �߰� ��ȭ ����");
        }
        else
        {
            acquiredTraits[trait.type] = trait;
            Debug.Log($"�ű�ȹ�� {trait.traitName} ù ȹ��");
        }
    }

    public bool HasTrait(TraitType type, out float value)
    {
        if (acquiredTraits.TryGetValue(type, out var trait))
        {
            value = trait.value;
            return true;
        }
        value = 0f;
        return false;
    }

    public bool TryActivateInvincibility(float currentHP, float maxHP, float cooldown, out float duration)
    {
        duration = 2f; // �⺻ ���� �ð� (��)

        // Ư���� ���������� Ȯ���� �ʿ�����
        if (acquiredTraits.TryGetValue(TraitType.InvincibleOnLowHP, out Trait trait))
        {
            float threshold = maxHP * trait.value; // Ư�� �ߵ��� ���� ü�¼���

            if (currentHP <= threshold)
            {
                // ������ Ư�� ���� ���� ��������, ���� ������ �ߵ� ������ Ȯ���� ����������� ���� ���������� ����
                float lastTime = lastActivatedTime.ContainsKey(TraitType.InvincibleOnLowHP)
                    ? lastActivatedTime[TraitType.InvincibleOnLowHP]
                    : -999f;

                // ��Ÿ���� ���������� ���� Ȯ�� ����
                if (Time.time >= lastTime + cooldown)
                {
                    lastActivatedTime[TraitType.InvincibleOnLowHP] = Time.time;
                    Debug.Log($"�����ߵ�! ���ӽð� {duration}");
                    return true;
                }
                else
                {
                    float remainingTime = Mathf.Max(0f, lastTime + cooldown - Time.time);
                    Debug.Log($"���� ��Ÿ�� ����� ���� �ð� : {remainingTime:F1}��");
                }    
            }
        }

        // Ư�� ���� x
        return false;
    }

}
