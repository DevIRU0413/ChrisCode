using UnityEngine;

namespace Scripts.Interface
{
    public interface IManager
    {
        int Priority { get; }  // �ʱ�ȭ ���� ����
        void Initialize();     // �ʱ�ȭ
        void Cleanup();        // ����/����
        GameObject GetGameObject();  // �Ҽ� GameObject ��ȯ
    }
}

