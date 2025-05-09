using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitButton : MonoBehaviour
{
    public TextMeshProUGUI traitName;
    public TextMeshProUGUI traitDesc;

    private Trait traitData; // �ش� ��ư�� ����ϴ� Ư�� ������
    private System.Action<Trait> onClickCallback; // Ŭ������ �� ������ �Լ�

    public void Setup(Trait trait, System.Action<Trait> callback)
    {
        traitData = trait;
        onClickCallback = callback;

        if (traitName != null)
        {
            traitName.text = trait.traitName;
        }

        if (traitDesc != null)
        {
            traitDesc.text = trait.description;
        }

        GetComponent<Button>().onClick.RemoveAllListeners(); // ���� ���� ����
        GetComponent<Button>().onClick.AddListener(OnClick); // ���� traitData �������� ���ο���
    }

    public void OnClick()
    {
        if (onClickCallback != null)
        {
            onClickCallback.Invoke(traitData); // ������ Ư���� TraitUIManager�� �ѱ�
        }
    }
}
