using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMS_UIRegister : MonoBehaviour
{
    //UI��Ҹ� ������ ���� ��� �δ� ���� �� �ҷ��� �� �������
    //�ҷ� �� �� key���� UI�̸�����
    private static Dictionary<string, GameObject> prefabMap = new Dictionary<string, GameObject>();

    //UI��ҵ��� Register �ϴ°� ���߿� UI�̸��� ���� Resource���� ���� �� �ְ�
    public static void RegisterPrefab(string panelName, GameObject prefab)
    {
        if (!prefabMap.ContainsKey(panelName))
        {
            prefabMap.Add(panelName, prefab);
        }
    }

    //���������� ����� UI��Ҹ� ��������
    public static GameObject GetPrefab(string panelName)
    {
        prefabMap.TryGetValue(panelName, out GameObject prefab);
        return prefab;
    }
}
