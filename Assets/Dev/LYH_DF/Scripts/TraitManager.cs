using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
    private CharacterMove characterMove; // ��ä���� ���� �ɷ�ġ ��ũ��Ʈ ���� �ݿ� 
    private PlayerHp playerHP; // ��ä���� ���� PlayerHP.cs ���� �ݿ�
    private PlayerAttack playerAttack;  //*���� �����ʿ�

    // Ư�� ȿ�� ������ ���� ��ųʸ�
    private Dictionary<TraitType, System.Action<float>> traitEffects;
    // ��Ÿ�� üũ�� ��ųʸ�
    private Dictionary<TraitType, float> lastActivatedTime = new Dictionary<TraitType, float>();
    // ���� �÷��̾ ������ Ư�� ���
    public Dictionary<TraitType, Trait> acquiredTraits = new Dictionary<TraitType, Trait>();

    private const int MaxUpgradeCount = 3;

    private void Start()
    {
        characterMove = FindObjectOfType<CharacterMove>(); // �÷��̾� �̵��ӵ� ��ũ��Ʈ ã��
        playerHP = FindObjectOfType<PlayerHp>(); // �÷��̾� HP ��ũ��Ʈ ã��
        playerAttack = FindAnyObjectByType<PlayerAttack>(); //* ���� �����ʿ� / �÷��̾� ���� ��ũ��Ʈ ã��

        SetupTraitEffects(); // Ư�� ȿ�� ��ųʸ� �ʱ�ȭ
    }

    private void SetupTraitEffects()
    {
        traitEffects = new Dictionary<TraitType, System.Action<float>>
        {
            // �̵��ӵ� ����
            {
                TraitType.MoveSpeed, (value) =>
                {
                    if (characterMove != null && CanUpgrade(TraitType.MoveSpeed, value))
                    {
                        characterMove.moveSpeed *= value;
                        Debug.Log("[�̵��ӵ�] ����: x" + value);
                    }
                }
            },

            // �ִ� ü�� ����
            {
                TraitType.MaxHealth, (value) =>
                {
                    if (playerHP != null && CanUpgrade(TraitType.MaxHealth, value))
                    {
                        playerHP.MaxHealth = Mathf.RoundToInt(playerHP.MaxHealth * value);
                        Debug.Log("[�ִ�ü��] ����: x" + value);
                    }
                }
            },

            // ���ݷ� ����
            {
                TraitType.AttackPower, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.AttackPower, value))
                    {
                        playerAttack.attackPower *= Mathf.RoundToInt(value);
                        Debug.Log("[���ݷ�] ����: x" + value);
                    }
                }
            },

            // ���ݼӵ� ����
            {
                TraitType.AttackSpeed, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.AttackSpeed, value))
                    {
                        playerAttack.attackSpeed *= Mathf.RoundToInt(value);
                        Debug.Log("[���ݼӵ�] ����: x" + value);
                    }
                }
            },

            // �߰� �߻�ü
            {
                TraitType.ExtraProjectile, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.ExtraProjectile, value))
                    {
                        playerAttack.extraProjectile += Mathf.RoundToInt(value); // ���� ���� �ʿ�
                        Debug.Log("[�߰��߻�ü] +" + value);
                    }
                }
            },

            // �߻�ü ũ�� ����
            {
                TraitType.ProjectileSize, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.ProjectileSize, value))
                    {
                        playerAttack.projectileSizeMultiplier *= value; // ���� ���� �ʿ�
                        Debug.Log("[�߻�üũ��] ����: x" + value);
                    }
                }
            },

            // ����� ����
            {
                TraitType.Pierce, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.Pierce, value))
                    {
                        playerAttack.pierceCount += Mathf.RoundToInt(value); // ���� ���� �ʿ�
                        Debug.Log("[�����] +" + value);
                    }
                }
            },

            // ���� Ư��
            {
                TraitType.Explosion, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.Explosion, value))
                    {
                        playerAttack.explosionRadius += value; // ���� ���� �ʿ�
                        Debug.Log("[���߹ݰ�] ����: +" + value);
                    }
                }
            }
        };
    }

    // ���׷��̵� �������� Ȯ�� (�ִ� 3ȸ)
    private bool CanUpgrade(TraitType type, float addValue)
    {
        if (acquiredTraits.TryGetValue(type, out var trait))
        {
            int currentLevel = Mathf.RoundToInt(trait.value);
            int addLevel = Mathf.RoundToInt(addValue);
            if (currentLevel >= MaxUpgradeCount)
            {
                Debug.Log("[���׷��̵� ����] " + type + " �̹� �ִ� ��ȭ��");
                return false;
            }
            return true;
        }
        return true;
    }

    public void ApplyTrait(Trait trait)
    {
        if (!trait.allowMultiple && acquiredTraits.ContainsKey(trait.type))
        {
            Debug.Log($"{trait.traitName} �ߺ� ���� �Ұ�");
            return;
        }

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

    public void HealOnKill()
    {
        if (acquiredTraits.TryGetValue(TraitType.HealOnKill, out Trait trait))
        {
            // Ư�� value ����ŭ ȸ���� ���
            float rawhealAmount = trait.value;
            int healAmount = Mathf.RoundToInt(rawhealAmount); // �Ҽ��� �ݿø�

            if (playerHP != null)
            {
                playerHP.Heal(healAmount); //*** PlayerHP�� ���ᰡ ���� �Լ� ���� �ʿ�
                Debug.Log($"���� óġ �� ü��  {healAmount} ȸ��!");
            }
            else
            {
                Debug.LogWarning("PlayerHP ������Ʈ�� �����ϴ�. ȸ������");
            }
        }
    }


}
