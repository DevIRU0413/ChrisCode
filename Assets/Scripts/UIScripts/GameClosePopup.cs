using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClosePopup : MonoBehaviour
{
    public Button exitGameButton;
    public Button cancelButton;

    void Start()
    {
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    void OnExitGameButtonClicked()
    {
        //TODO - ���� ���� �����Ǹ� �������� �Լ� ȣ�� ����
    Debug.Log(exitGameButton);
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif      
    }

void OnCancelButtonClicked()
    {
        TMP_UIManager.Instance.CloseCurrentUI();
        Debug.Log(cancelButton);
    }
}
