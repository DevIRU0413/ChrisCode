using UnityEngine;
using UnityEngine.Events;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int m_currentExp = 0;
    private int m_level = 1;
    private int m_expToLevelUp = 100;

    public UnityAction<int> OnLevelUp;

    public int Level => m_level;
    public int CurrentExp => m_currentExp;

    public void GainExp(int amount)
    {
        m_currentExp += amount;

        while (m_currentExp >= m_expToLevelUp)
        {
            m_currentExp -= m_expToLevelUp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        m_level++;
        m_expToLevelUp = GetNextExpRequirement(m_level);
        Debug.Log($"������! ���� ����: {m_level}");
        OnLevelUp?.Invoke(m_level); // �ܺο� �̺�Ʈ �˸�
    }

    private int GetNextExpRequirement(int currentLevel)
    {
        return 100 + (currentLevel - 1) * 50;
    }
}
