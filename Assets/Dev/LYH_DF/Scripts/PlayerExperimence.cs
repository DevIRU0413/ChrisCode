using System.Collections.Generic;
using UnityEngine;

public class PlayerExperimence : MonoBehaviour
{
    public int currentExp = 0;
    public int level = 1;
    public int expToLevelUp = 100;

    public TraitUIManager traitUIManager;

    public void GainExp(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        currentExp = 0;
        expToLevelUp += 50;
        Debug.Log("레벨업! 현재 레벨 :" + level);

        if (traitUIManager != null)
        {
            List<Trait> randomTraits = TraitDataBase.Instance.GetRandomTraits(3);
            traitUIManager.OpenTraitSelection(randomTraits); // 뽑은 특성 넘기기
        }
        else
        {
            Debug.LogWarning("TraitUIManager가 연결되지 않았습니다.");
        }
    }
}
