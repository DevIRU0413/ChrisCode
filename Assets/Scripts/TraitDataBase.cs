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

        TraitManager traitManager = FindAnyObjectByType<TraitManager>();
        int MaxUpgradeCount = 3;

        // �̹� ���õ� Ư�� �� ���� ���ǿ� �ش��ϴ� �� ����
        for (int i = copy.Count - 1; i >= 0; i--)
        {
            Trait trait = copy[i];

            if (traitManager.acquiredTraits.TryGetValue(trait.type, out Trait acquired))
            {
                // ���� ���� Ư���� �̹� �ִٸ� ���������� ����
                if (!trait.allowMultiple)
                {
                    copy.RemoveAt(i);
                    continue;
                }

                // ��ȭ�� ������ Ư���� �ִ밭ȭ�� ��� ����
                int currentLevel = Mathf.RoundToInt(acquired.value);
                int addLevel = Mathf.RoundToInt(trait.value);

                if (currentLevel >= MaxUpgradeCount || currentLevel + addLevel > MaxUpgradeCount)
                {
                    copy.RemoveAt(i);
                }
            }
        }

        // �����ϰ� count ������ŭ ����
        for (int i = 0; i < count; i++)
        {
            if (copy.Count == 0) break;

            int randomIndex = Random.Range(0, copy.Count);
            randomTraits.Add(copy[randomIndex]);
            copy.RemoveAt(randomIndex);
        }

        return randomTraits;
    }
}
