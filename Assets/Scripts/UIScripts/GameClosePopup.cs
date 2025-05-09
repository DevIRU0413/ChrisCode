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
        //TODO - 게임 종료 구현되면 게임종료 함수 호출 쓰기
    Debug.Log(exitGameButton);
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif      
    }

void OnCancelButtonClicked()
    {
        TMP_UIManager.Instance.CloseCurrentUI();
        Debug.Log(cancelButton);
    }
}
