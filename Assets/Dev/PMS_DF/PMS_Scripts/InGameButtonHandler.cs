using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameButtonHandler : MonoBehaviour
{
    public Button[] buttons; // ���� ��ư�� �迭�� ����

    void Start()
    {
        // ��� ��ư�� ���� �̺�Ʈ�� �������� ����
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button.name)); // ��ư �̸��� ������� ���� ����
        }
    }

    private void OnButtonClicked(string btnname)
    {
        switch (btnname)
        {
            case "OptionButton":
                Debug.Log("�ɼ�â");
                break;
            default:
                Debug.Log("�� �� ���� ��ư: " + btnname);
                break;
        }
    }
}
