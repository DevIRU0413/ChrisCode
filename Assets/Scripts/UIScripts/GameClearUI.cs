using Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private Button QuitButton;

    private void Start()
    {
        QuitButton.onClick.AddListener(OnQuitButton);
    }

    private void OnQuitButton()
    {
        //Ÿ��Ʋ �� ��ȭ �޼��� �߰�
        SceneManagerEx.Instance.LoadSceneWithFade("PMS_TiTleScene");
    }
}
