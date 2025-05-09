using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_UIManager : MonoBehaviour
{
    public static TMP_UIManager Instance { get; private set; }

    private Stack<GameObject> uiStack = new Stack<GameObject>();
    public Transform uiRoot; // 모든 UI의 부모가 될 Transform

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //UI 프리팹을 인스턴스화하여 `uiRoot`의 자식으로 만들고 스택에 Push합니다
    public void OpenUI(GameObject uiPrefab)
    {
        GameObject uiInstance = Instantiate(uiPrefab, uiRoot);
        uiInstance.SetActive(true);
        uiStack.Push(uiInstance);
    }

    //스택 제일 위의 UI제거 
    public void CloseCurrentUI()
    {
        if (uiStack.Count > 0)
        {
            GameObject topUI = uiStack.Pop();
            Destroy(topUI);
        }
    }

    //반복문으로 특정Ui전까지 없앨수있다.
    public void CloseUI(GameObject uiToClose)
    {
        if (uiToClose != null && uiStack.Contains(uiToClose))
        {
            uiStack.Pop(); // 스택에서 제거 (순서 주의!)
            Destroy(uiToClose);
        }
    }
}
