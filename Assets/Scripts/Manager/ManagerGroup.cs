using Scripts.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
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
                    const string groupName = "@ManagerGroup";
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
            if (manager == null)
                return;

            if (m_managers.Contains(manager))
                return;

            m_managers.Add(manager);

            GameObject go = manager.GetGameObject();
            if (go != null)
                go.transform.parent = transform;
        }

        public void RegisterManager(GameObject managerObject)
        {
            if (managerObject == null)
                return;

            IManager manager = managerObject.GetComponent<IManager>();
            if (manager != null)
                RegisterManager(manager);
        }

        public void RegisterManager(params IManager[] managers)
        {
            for (int i = 0; i < managers.Length; i++)
                RegisterManager(managers[i]);
        }

        public void RegisterManager(params GameObject[] managerObjects)
        {
            for (int i = 0; i < managerObjects.Length; i++)
                RegisterManager(managerObjects[i]);
        }

        public void InitializeManagers()
        {
            SortManagersByPriorityAscending();

            for (int i = 0; i < m_managers.Count; i++)
            {
                IManager manager = m_managers[i];
                manager.Initialize();

                GameObject go = manager.GetGameObject();
                Debug.Log("InIt " + go.name);
                if (go != null)
                    go.transform.parent = transform;
            }
        }

        public void CleanupManagers()
        {
            SortManagersByPriorityDescending();

            for (int i = 0; i < m_managers.Count; i++)
                m_managers[i].Cleanup();
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
