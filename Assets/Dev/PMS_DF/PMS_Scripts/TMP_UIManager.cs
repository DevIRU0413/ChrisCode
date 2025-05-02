using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_UIManager : MonoBehaviour
{
    public static TMP_UIManager Instance { get; private set; }

    private Stack<GameObject> uiStack = new Stack<GameObject>();
    public Transform uiRoot; // ��� UI�� �θ� �� Transform

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //UI �������� �ν��Ͻ�ȭ�Ͽ� `uiRoot`�� �ڽ����� ����� ���ÿ� Push�մϴ�
    public void OpenUI(GameObject uiPrefab)
    {
        GameObject uiInstance = Instantiate(uiPrefab, uiRoot);
        uiInstance.SetActive(true);
        uiStack.Push(uiInstance);
    }

    //���� ���� ���� UI���� 
    public void CloseCurrentUI()
    {
        if (uiStack.Count > 0)
        {
            GameObject topUI = uiStack.Pop();
            Destroy(topUI);
        }
    }

    //�ݺ������� Ư��Ui������ ���ټ��ִ�.
    public void CloseUI(GameObject uiToClose)
    {
        if (uiToClose != null && uiStack.Contains(uiToClose))
        {
            uiStack.Pop(); // ���ÿ��� ���� (���� ����!)
            Destroy(uiToClose);
        }
    }
}
