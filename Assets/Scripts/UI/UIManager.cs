using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private Transform _uiRoot;
        private SceneID _currentMainUISceneType = SceneID.None;

        private readonly Dictionary<string, UIPanelBase> _panelInstances = new(); // 패널들
        private readonly Stack<UIPanelBase> _panelStack = new(); // 패널 스택(저장공간)
        private readonly List<string> _savedPanelStack = new(); // 저장된 패널 스택(이름)

        private void Awake()
        {
            InitializeSingleton();
        }

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
                SceneManager.sceneLoaded += OnSceneLoaded;
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

        public void LoadSceneUI(SceneID sceneType, GameObject uiScenePrefab = null)
        {
            string sceneStr= sceneType.ToString();

            GameObject sceneObject;
            if (uiScenePrefab == null)
                sceneObject = Resources.Load<GameObject>($"UI/UI_Scene_{sceneStr}");
            else
                sceneObject = uiScenePrefab;

            Instantiate(sceneObject);
        }

        public void OpenPanel(string panelName)
        {
            if (_panelInstances.ContainsKey(panelName))
            {
                PushPanel(_panelInstances[panelName]);
                return;
            }

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
}