using Scripts.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Manager
{
    public class ManagerGroup : MonoBehaviour
    {
        #region Singleton
        private static ManagerGroup m_instance;
        public static ManagerGroup Instance
        {
            get
            {
                if (m_instance == null)
                {
                    string groupName = $"@{typeof(ManagerGroup).Name}";
                    GameObject go = GameObject.Find(groupName);
                    if (go == null)
                    {
                        go = new GameObject(groupName);
                        DontDestroyOnLoad(go);
                    }

                    m_instance = go.GetOrAddComponent<ManagerGroup>();
                }

                return m_instance;
            }
        }
        #endregion

        #region PrivateVariables
        private List<IManager> m_managers = new();
        #endregion

        #region PublicMethod

        public void RegisterManager(IManager manager)
        {
            if (manager == null || m_managers.Contains(manager))
                return;

            m_managers.Add(manager);
            manager.GetGameObject().transform.parent = transform;
        }

        public void RegisterManager(GameObject managerObject)
        {
            RegisterManager(managerObject?.GetComponent<IManager>());
        }

        public void RegisterManager(params IManager[] managers)
        {
            foreach (IManager m in managers) RegisterManager(m);
        }

        public void RegisterManager(params GameObject[] managerObjects)
        {
            foreach (GameObject go in managerObjects) RegisterManager(go);
        }

        public void InitializeManagers()
        {
            SortManagersByPriorityAscending();

            foreach (var manager in m_managers)
            {
                manager.Initialize();
                Debug.Log($"[Init] {manager.GetGameObject().name}");
                manager.GetGameObject().transform.parent = transform;
            }
        }

        public void CleanupManagers()
        {
            SortManagersByPriorityDescending();
            for (int i = 0; i < m_managers.Count; i++)
            {
                IManager manager = m_managers[i];
                GameObject go = manager.GetGameObject();

                Debug.Log($"[Cleanup] {go.name}");

                manager.Cleanup();
            }
        }

        public void ClearAllManagers()
        {
            ClearManagers(true);
        }

        public void ClearManagers(bool forceClear = false)
        {
            for (int i = 0; i < m_managers.Count; i++)
            {
                IManager manager = m_managers[i];
                if (!manager.IsDontDestroy || forceClear)
                {
                    GameObject go = manager.GetGameObject();

                    Debug.Log($"[Clear] {go.name}");

                    Destroy(go);
                }
            }
        }
        #endregion

        #region PrivateMethod

        private void SortManagersByPriorityAscending()
        {
            for (int i = 0; i < m_managers.Count - 1; i++)
            {
                for (int j = 0; j < m_managers.Count - i - 1; j++)
                {
                    if (m_managers[j].Priority > m_managers[j + 1].Priority)
                    {
                        IManager temp = m_managers[j];
                        m_managers[j] = m_managers[j + 1];
                        m_managers[j + 1] = temp;
                    }
                }
            }
        }

        private void SortManagersByPriorityDescending()
        {
            for (int i = 0; i < m_managers.Count - 1; i++)
            {
                for (int j = 0; j < m_managers.Count - i - 1; j++)
                {
                    if (m_managers[j].Priority < m_managers[j + 1].Priority)
                    {
                        IManager temp = m_managers[j];
                        m_managers[j] = m_managers[j + 1];
                        m_managers[j + 1] = temp;
                    }
                }
            }
        }

        #endregion
    }
}
