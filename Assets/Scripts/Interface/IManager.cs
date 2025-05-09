using UnityEngine;

namespace Scripts.Interface
{
    public interface IManager
    {
        int Priority { get; }  // �ʱ�ȭ ���� ����
        bool IsDontDestroy { get; }
        void Initialize();     // �ʱ�ȭ
        void Cleanup();        // ����/����
        GameObject GetGameObject();  // �Ҽ� GameObject ��ȯ
    }
}

