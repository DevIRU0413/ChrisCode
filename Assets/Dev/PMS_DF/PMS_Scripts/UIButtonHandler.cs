using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButtonHandler : MonoBehaviour
{
    public Button[] buttons; // ���� ��ư�� �迭�� ����
    public GameObject TitleUI;
    public GameObject ExitGamePopup;

    void Start()
    {
        // ��� ��ư�� ���� �̺�Ʈ�� �������� ����
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button.name)); // ��ư �̸��� ������� ���� ����
        }
    }

    void OnButtonClicked(string buttonName)
    {
        switch (buttonName)
        {
            case "StartButton":
                StartGame();
                break;
            case "OptionButton":
                Debug.Log("OptionButton Click");
                break;
            case "ExitButton":
                //TitleUI.SetActive(false);
                ExitGamePopup.SetActive(true);
                break;
            case "CancelButton":
                ExitGamePopup.SetActive(false);
                break;
            default:
                Debug.Log("�� �� ���� ��ư: " + buttonName);
                break;
        }
    }

    void StartGame()
    {
        Debug.Log("���� ����!");
    }

    void PauseGame()
    {
        Debug.Log("���� �Ͻ� ����!");
    }

    void ExitGame()
    {
        Debug.Log("���� ����!");
    }
}
