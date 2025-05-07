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
        //�Ͻ����� ���� �� ���� ����
        //���� Open�� UI �ݱ�
        TMP_UIManager.Instance.CloseCurrentUI();
    }

    private void OnOptionButtonClicked()
    {
        //�ɼ�UI �ٿ�� ���ְ� 
        TMP_UIManager.Instance.OpenUI(optionUI);
    }

    private void OnRestartButtonClicked()
    {
        //���� ����� �ϱ�
    }

    private void QuitButtonClicked()
    {
        //���� ���� -> Ÿ��Ʋ ȭ�� ����
    }
}
