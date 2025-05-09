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
        //타이틀 씬 전화 메서드 추가
        SceneManagerEx.Instance.LoadSceneWithFade("PMS_TiTleScene");
    }
}
