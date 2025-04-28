using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
    //private CharacterMove characterMove; // ��ä���� ���� �ɷ�ġ ��ũ��Ʈ ���� �ݿ� 
    //private PlayerHP playerHP; // ��ä���� ���� PlayerHP.cs ���� �ݿ�

    public List<Trait> acquiredTraits = new List<Trait>();

    private void Start()
    {
        //characterMove = FindObjectOfType<CharacterMove>(); // �÷��̾� �̵��ӵ� ��ũ��Ʈ ã��
        //playerHP = FindObjectOfType<PlayerHP>(); // �÷��̾� HP ��ũ��Ʈ ã��
    }

    public void ApplyTrait(Trait trait)
    {
        acquiredTraits.Add(trait); // ����Ʈ�� �߰�

        switch (trait.type)
        {
            case TraitType.MoveSpeed:
                //characterMove.moveSpeed *= trait.value;
                break;
            case TraitType.MaxHealth:
                //if (playerHP != null)
                {
                    //playerHP.MaxHealth = Mathf.RoundToInt(playerHP.MaxHealth * trait.value);
                    //playerHP.CurrentHealth = playerHP.MaxHealth; // Ư�� ������ ü���� Ǯ�� ä���
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
}
