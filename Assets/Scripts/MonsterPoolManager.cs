using Scripts;
using Scripts.Interface;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : SimpleSingleton<MonsterPoolManager>
{
    [SerializeField] private GameObject[] m_prefabs;
    [SerializeField] private int m_poolSize = 5;
    private List<GameObject>[] m_pools;

    public int Priority => (int)ManagerPriority.MonsterManager;

    void InitPools()
    {
        m_pools = new List<GameObject>[m_prefabs.Length];

        for (int i = 0; i < m_prefabs.Length; i++)
        {
            m_pools[i] = new List<GameObject>();

            for (int j = 0; j < m_poolSize; j++)
            {
                GameObject monster = Instantiate(m_prefabs[i],this.transform);
                monster.SetActive(false);
                m_pools[i].Add(monster);
            }
        }
    }
    public GameObject GetPool(MonsterType type)
    {
        int index = (int)type;

        foreach (var mon in m_pools[index])
        {
            if (!mon.activeSelf)
            {
                mon.SetActive(true);
                mon.GetComponent<MonsterBase>().isPool = true; // 추후 논의 후 태그를 pooled로 바꾸는 것도 검토 필요.
                return mon;
            }
        }

        GameObject newMon = Instantiate(m_prefabs[index],this.transform);
        m_pools[index].Add(newMon);
        return newMon;
    }

    public GameObject GetPool(int index)
    {
        foreach (var mon in m_pools[index])
        {
            if (mon == null) continue;

            if (!mon.activeSelf)
            {
                mon.SetActive(true);
                mon.GetComponent<MonsterBase>().isPool = true;
                return mon;
            }
        }

        GameObject newMon = Instantiate(m_prefabs[index],this.transform);
        m_pools[index].Add(newMon);
        return newMon;
    }

    public GameObject GetRandomPool()
    {
        int randomIndex = UnityEngine.Random.Range(0, m_prefabs.Length);
        return GetPool(randomIndex);
    }

    public void ReturnPool(MonsterBase monster)
    {
        monster.gameObject.SetActive(false);
    }

    protected override void Awake()
    {
        base.Awake();
        InitPools();
    }
}
