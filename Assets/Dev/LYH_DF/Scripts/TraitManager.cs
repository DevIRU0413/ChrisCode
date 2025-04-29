using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
    private CharacterMove characterMove; // ��ä���� ���� �ɷ�ġ ��ũ��Ʈ ���� �ݿ� 
    private PlayerHp playerHP; // ��ä���� ���� PlayerHP.cs ���� �ݿ�

    // Ư�� ȿ�� ������ ���� ��ųʸ�
    private Dictionary<TraitType, System.Action<float>> traitEffects;

    public List<Trait> acquiredTraits = new List<Trait>();

    private void Start()
    {
        characterMove = FindObjectOfType<CharacterMove>(); // �÷��̾� �̵��ӵ� ��ũ��Ʈ ã��
        playerHP = FindObjectOfType<PlayerHp>(); // �÷��̾� HP ��ũ��Ʈ ã��

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
                        playerHP.MaxHealth = Mathf.RoundToInt(playerHP.MaxHealth * value);
                        Debug.Log($"�ִ�ü�� ����! ���� �ִ�ü�� : {playerHP.MaxHealth}");
                    }
                }
            },

            {
                TraitType.AttackSpeed, (value) =>
                {
                    if (attackSpeed != null)
                    {

                    }
                }
            }
        }
    }

    public void ApplyTrait(Trait trait)
    {
        // ���� Ư���� ��ġ���� Ȯ���ؾ� �Ѵ�.
        Trait existingTrait = acquiredTraits.Find(t => t.type == trait.type);

        if (existingTrait != null)
        {
            Debug.Log($"{trait.traitName} Ư�� ��ȭ!");

            // �̹� �ѹ� ���� Ư���� ��� �߰� ��ȭ
            switch (trait.type)
            {
                case TraitType.MoveSpeed:
                    if (cha)
            }
        }

        switch (trait.type)
        {
            case TraitType.MoveSpeed:
                if (characterMove != null)
                {
                    characterMove.moveSpeed *= trait.value;
                    Debug.Log($"Ư������! �̵��ӵ� {trait.value}�� ����!");
                }
                else
                {
                    Debug.LogWarning("[����] CharacterMove�� ã�� ���߽��ϴ�.");
                }
                break;
            case TraitType.MaxHealth:
                if (playerHP != null)
                {
                    playerHP.MaxHealth = Mathf.RoundToInt(playerHP.MaxHealth * trait.value);
                    playerHP.CurrentHealth = playerHP.MaxHealth; // Ư�� ������ ü���� Ǯ�� ä���
                    Debug.Log("�ִ�ü�� ����!");
                }
                break;
            case TraitType.AttackPower:
                Debug.Log("���ݷ� ����!");
                break;
            case TraitType.AttackSpeed:
                Debug.Log("���ݼӵ� ����!");
                break;
            case TraitType.ExtraProjectile:
                Debug.Log("�߰� ����ü �߻�!");
                break;
            default:
                Debug.LogWarning("������ �� ���� Ư���Դϴ�." + trait.traitName);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (acquiredTraits.Count > 0)
            {
                ApplyTrait(acquiredTraits[0]); // ����Ʈ ù��° Ư�� ����
                Debug.Log("TŰ ù ��° Ư�� ����Ϸ�");
            }
        }
    }
}
