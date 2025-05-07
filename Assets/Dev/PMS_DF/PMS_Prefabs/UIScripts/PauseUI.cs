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
        continueButton.onClick.AddListener(OnContinueClicked);
        //optionButton.onClick.AddListener();
        //restartButton.onClick.AddListener();
        //quitButton.onClick.AddListener();
    }

    private void OnContinueClicked()
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
}
