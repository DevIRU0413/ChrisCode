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

        // 이미 선택된 특성 중 제한 조건에 해당하는 것 제거
        for (int i = copy.Count - 1; i >= 0; i--)
        {
            Trait trait = copy[i];

            if (traitManager.acquiredTraits.TryGetValue(trait.type, out Trait acquired))
            {
                // 단일 선택 특성은 이미 있다면 선택지에서 제거
                if (!trait.allowMultiple)
                {
                    copy.RemoveAt(i);
                    continue;
                }

                // 강화가 가능한 특성은 최대강화일 경우 제거
                int currentLevel = Mathf.RoundToInt(acquired.value);
                int addLevel = Mathf.RoundToInt(trait.value);

                if (currentLevel >= MaxUpgradeCount || currentLevel + addLevel > MaxUpgradeCount)
                {
                    copy.RemoveAt(i);
                }
            }
        }

        // 랜덤하게 count 갯수만큼 추출
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
