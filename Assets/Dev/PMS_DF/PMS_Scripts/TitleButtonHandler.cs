using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Scripts.UI;

public class TitleButtonHandler : MonoBehaviour
{
    public Button[] buttons; // ���� ��ư�� �迭�� ����
    public Stack<GameObject> uiStack;
    public GameObject titleUI;
    public GameObject exitGamePopup;
    public GameObject soundSettingUI;
    //public GameObject ExitGamePopupPrefab;

    void Start()
    {
        // ��� ��ư�� ���� �̺�Ʈ�� �������� ����
        foreach (var button in buttons)
        {
            // ��ư �̸�(���ڿ�)�� Enum ������ ��ȯ�Ͽ� ����
            ButtonName btnName = (ButtonName)System.Enum.Parse(typeof(ButtonName), button.name);
            button.onClick.AddListener(() => OnButtonClicked(btnName)); // ��ư �̸��� ������� ���� ����
        }
    }

    private void OnButtonClicked(ButtonName btn)
    {
        switch (btn)
        {
            case ButtonName.StartButton:
                StartGame();
                break;
            case ButtonName.OptionButton:
                TMP_UIManager.Instance.OpenUI(soundSettingUI);
                break;
            case ButtonName.GameCloseButton:
                //TitleUI.SetActive(false);
                TMP_UIManager.Instance.OpenUI(exitGamePopup);
                Debug.Log("���������ư����");
                break;
            /*case ButtonName.ExitGameButton:
                //TODO - ���� ���� �����Ǹ� �������� �Լ� ȣ�� ����
                Debug.Log("���� ���� ���� �Լ� ȣ��");
                break;
            case ButtonName.CancelButton:
                TMP_UIManager.Instance.CloseUI(ExitGamePopup);
                Debug.Log("�ȳ��ϼ���");
                break;
            */
            default:
                Debug.Log("�� �� ���� ��ư: " + btn);
                break;
        }
    }

    private void StartGame()
    {
        Debug.Log("���� ����!");
    }

    private void PauseGame()
    {
        Debug.Log("���� �Ͻ� ����!");
    }

    private void ExitGame()
    {
        Debug.Log("���� ����!");
    }
}
