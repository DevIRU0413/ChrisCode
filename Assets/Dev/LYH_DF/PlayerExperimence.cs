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
        Debug.Log("������! ���� ���� :" + level);

        if (traitUIManager != null)
        {
            traitUIManager.OpenTraitSelection();
        }
    }

}
