using Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PMS_UIManager : MonoBehaviour
{
    public static PMS_UIManager Instance { get; private set; }  //�̱��� ��ü����
    [SerializeField] private Transform _uiRoot;     //ui�� �θ� �Ǵ� ���
    private SceneType _currentMainUISceneType = SceneType.None; //���� � UI�� �ٿ� ��Ÿ������ //���߿� �Ƹ� ���� SceneManager���� �ٲ� �� � UI��Ҹ� ���������

    private readonly Dictionary<string, UIPanelBase> _panelInstances = new(); //�гε� �б��������� ?
    private readonly Stack<UIPanelBase> _panelStack = new(); // �г� ����(�������)
    private readonly List<string> _savedPanelStack = new(); // ����� �г� ����(�̸�)

    private void Awake() //�̱��� ���
    {
        InitializeSingleton();
    }

    //?
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        CloseAllPanels();
    }

    //��Ÿ�Կ� �°� UI��Ҹ� ������ ���
    public void LoadSceneUI(SceneType sceneType, GameObject uiScenePrefab = null)
    {
        //���� �̸��� ���ͼ�
        string sceneStr = sceneType.ToString();

        GameObject sceneObject;
        //���� �´� Ui��ҵ��� Resources�� ���� �����°� 
        if (uiScenePrefab == null)
            sceneObject = Resources.Load<GameObject>($"UI/UI_Scene_{sceneStr}");
        else
            sceneObject = uiScenePrefab;    //else������ �߸𸣰ٴ�.

        //�ش� UI��ü ����
        Instantiate(sceneObject);
    }

    //�ش� UI��Ҹ� ���°�
    public void OpenPanel(string panelName)
    {
        //UI�� �̸��� Ű������ ���� ���´�, �ش� Ui������Ʈ�� ������ ���ͼ� ����
        if (_panelInstances.ContainsKey(panelName))
        {
            //�̹� ������� �г��̸� ���� ������ �ʰ� ���ÿ� �ױ⸸ ��
            PushPanel(_panelInstances[panelName]);
            return;
        }

        //UIRegister���� �ش�
        var panelPrefab = UIRegistry.GetPrefab(panelName);
        if (panelPrefab == null)
        {
            Debug.LogError($"[UIManager] Cannot find prefab for {panelName}");
            return;
        }

        var panelInstance = Instantiate(panelPrefab, _uiRoot);
        var panelBase = panelInstance.GetComponent<UIPanelBase>();
        _panelInstances.Add(panelName, panelBase);

        PushPanel(panelBase);
    }

    public void CloseCurrentPanel()
    {
        if (_panelStack.Count == 0)
            return;

        var topPanel = _panelStack.Pop();
        topPanel.Hide();
        Destroy(topPanel.gameObject, 1f);

        if (_panelStack.Count > 0)
        {
            var previousPanel = _panelStack.Peek();
            previousPanel.Show();
        }
    }

    public void CloseAllPanels()
    {
        while (_panelStack.Count > 0)
        {
            var panel = _panelStack.Pop();
            if (panel != null)
            {
                panel.Hide();
                Destroy(panel.gameObject, 1f);
            }
        }

        _panelInstances.Clear();
    }

    public bool HasPanels()
    {
        return _panelStack.Count > 0;
    }

    public void SavePanelStack()
    {
        _savedPanelStack.Clear();

        foreach (var panel in _panelStack)
        {
            _savedPanelStack.Add(panel.name.Replace("(Clone)", "").Trim());
        }

        _savedPanelStack.Reverse();
        Debug.Log("[UIManager] Panel Stack Saved");
    }

    public void RestorePanelStack()
    {
        if (_savedPanelStack.Count == 0)
            return;

        CloseAllPanels();

        foreach (var panelName in _savedPanelStack)
        {
            OpenPanel(panelName);
        }

        Debug.Log("[UIManager] Panel Stack Restored");
    }

    //stack�� ui��� 
    private void PushPanel(UIPanelBase newPanel)
    {
        if (_panelStack.Count > 0)
        {
            var current = _panelStack.Peek();
            current.Hide();
        }

        _panelStack.Push(newPanel);
        newPanel.Show();
    }
}
