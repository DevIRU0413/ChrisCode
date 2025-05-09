using System.Collections.Generic;
using UnityEngine;

public static class UIRegistry
{
    private static Dictionary<string, GameObject> prefabMap = new Dictionary<string, GameObject>();

    public static void RegisterPrefab(string panelName, GameObject prefab)
    {
        if (!prefabMap.ContainsKey(panelName))
        {
            prefabMap.Add(panelName, prefab);
        }
    }

    public static GameObject GetPrefab(string panelName)
    {
        prefabMap.TryGetValue(panelName, out GameObject prefab);
        return prefab;
    }
}
