using UnityEngine;

namespace Scripts.Scene
{
    // ���� �ε��ϰ� ���� ���� ã�� ������Ʈ�� �־��� .cs
    public abstract class SceneBase : MonoBehaviour
    {
        // �ش� ���� � ������
        public abstract SceneType SceneType { get; }

        // ���ҽ� �ε� �Ŵ��� ���߽� ���� ����

        [SerializeField] protected GameObject _sceneUIPrefab;

        protected virtual void Start()
        {
            InitializeSceneUI();
        }

        protected abstract void InitializeSceneUI();
    }
}