using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitUIManager : MonoBehaviour
{
    // Ư�� ����â �г��� �߰� ����� �� ���� ��õ��� �Ͻ������� ��Ų�� UI���� Ư���� �����ϸ� �ݿ��ʰ� ���ÿ� ���� �� ����

    public GameObject traitUIPanel;
    public TraitButton[] traitButtons;

    public void OpenTraitSelection(List<Trait> traits)
    {
        Debug.Log("UI Ư�� ����â ���� �õ�");
        if (traitUIPanel != null)
        {
            traitUIPanel.SetActive(true);
            Time.timeScale = 0f; //�Ͻ� ����

            for (int i = 0; i < traitButtons.Length; i++)
            {
                if (i < traits.Count)
                {
                    Debug.Log($"Ư�� {i + 1} Ȱ��ȭ - {traits[i].traitName}");
                    traitButtons[i].gameObject.SetActive(true); // ��ư Ȱ��ȭ
                    traitButtons[i].Setup(traits[i], OnTraitSelected);
                }
                else
                {
                    traitButtons[i].gameObject.SetActive(false); // �ʿ� ���� ��ư ��Ȱ��ȭ
                }
            }
        }
        else
        {
            Debug.Log("UI traitUIPanel�� null�Դϴ�.");
        }
    }

    public void OnTraitSelected(Trait selectedTrait)
    {
        // TraitManager ã�Ƽ� �����ϱ�
        TraitManager traitManager = FindObjectOfType<TraitManager>();

        if (traitManager != null)
        {
            traitManager.ApplyTrait(selectedTrait);
        }

        CloseTraitSelection();
    }

    public void CloseTraitSelection()
    {
        if (traitUIPanel != null)
        {
            traitUIPanel.SetActive(false);
            Time.timeScale = 1f; //���� �簳
        }
    }

}
