using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitUIManager
{
    // Ư�� ����â �г��� �߰� ����� �� ���� ��õ��� �Ͻ������� ��Ų�� UI���� Ư���� �����ϸ� �ݿ��ʰ� ���ÿ� ���� �� ����

    public GameObject traitUIPanel;

    public void OpenTraitSelection()
    {
        if (traitUIPanel != null)
        {
            traitUIPanel.SetActive(true);
            Time.timeScale = 0f; //�Ͻ� ����
        }
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
