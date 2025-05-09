using Scripts;
using Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("PauseUI")]

    [SerializeField] private GameObject optionUI;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        optionButton.onClick.AddListener(OnOptionButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    private void OnContinueButtonClicked()
    {
        //일시정지 해제 및 게임 진행
        //현재 Open된 UI 닫기
        TMP_UIManager.Instance.CloseCurrentUI();
        InGameManager.Instance.SetPlayState(GamePlayState.Playing);
    }

    private void OnOptionButtonClicked()
    {
        //옵션UI 뛰우게 해주고 
        TMP_UIManager.Instance.OpenUI(optionUI);
    }

    private void OnRestartButtonClicked()
    {
        //게임 재시작 하기
    }

    private void QuitButtonClicked()
    {
        //게임 종료 -> 타이틀 화면 가기
        SceneManagerEx.Instance.LoadSceneWithFade("PMS_TiTleScene");
    }
}
