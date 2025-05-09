using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_GameManager : MonoBehaviour
{
    public static float bgmValue = 0.2f;
    public static float sfxValue = 0.2f;

    public void GameStart()
    {
        Debug.Log("게임 시작!");
    }

    public void CloseGame()
    {
        Debug.Log("게임 종료!");
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
