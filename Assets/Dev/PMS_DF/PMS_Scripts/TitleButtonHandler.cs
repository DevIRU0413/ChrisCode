using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Scripts.UI;
using Scripts.Manager;

public class TitleButtonHandler : MonoBehaviour
{
    public Button[] buttons; // ���� ��ư�� �迭�� ����
    public Stack<GameObject> uiStack;
    public GameObject titleUI;
    public GameObject exitGamePopup;
    public GameObject soundSettingUI;
    //public GameObject ExitGamePopupPrefab;

    private void Start()
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
                TMP_UIManager.Instance.OpenUI(exitGamePopup);
                Debug.Log("���������ư����");
                break;
            default:
                Debug.Log("�� �� ���� ��ư: " + btn);
                break;
        }
    }

    private void StartGame()
    {
        SceneManagerEx.Instance.LoadSceneWithFade("PMS_InGameScene");
        Debug.Log("�� ��ȯ");
    }
}
