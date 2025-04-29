using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitDataBase : MonoBehaviour
{
    public static TraitDataBase Instance;

    public List<Trait> allTraits = new List<Trait>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Trait> GetRandomTraits(int count)
    {
        List<Trait> randomTraits = new List<Trait>();
        List<Trait> copy = new List<Trait>(allTraits);

        for (int i = 0; i < count; i++)
        {
            if (copy.Count == 0)// ������ ������ ����
            {
                break;
            }

            int randomIndex = Random.Range(0, copy.Count); // �������� �ϳ� ����
            randomTraits.Add(copy[randomIndex]); // ������ �� ��� ����Ʈ�� �߰�
            copy.RemoveAt(randomIndex); // �̹� ���� �� �����ؼ� �ߺ� ����
        }

        return randomTraits; // ��� ����
    }
}
