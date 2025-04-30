using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerHp : MonoBehaviour
{
    public int m_CurrentHealth = 5;  // ó�� 5�� ����
    public int m_MaxHealth = 5;
    public int m_LimitMaxHealth = 10;
    private bool m_IsUntouchable = false;
    private float m_UntouchableTime = 2f; 

    // ���� ���� �� ĳ������ ü���� ���� ���� �ִ� ü������ ����
    void Start()
    {
        m_MaxHealth = 5;
        m_CurrentHealth = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    // �÷��̾� ������ ���
    private void TakeDamage(int damage)
    {
        if (m_IsUntouchable == true)
        {
            return;
        }
        // ���� �ð�
        m_CurrentHealth -= damage;
        StartCoroutine(UntouchableTime());

        if (m_CurrentHealth <= 0)
        {
            Debug.Log(" ���� ����/ ���ư���");
        }
    }

    // �÷��̾� ü�� ȸ�� �� ȣ��
    private void IncreasePlayerCurrentHealth(int amount)
    {
        // ü�� ���ڿ��� �浹, ����, ���� �̺�Ʈ���� ü�� ȸ�� �̺�Ʈ�� ���� ��� ȣ��
        // ��ȣ
        m_CurrentHealth = Mathf.Min(m_CurrentHealth + 1, m_MaxHealth);
        m_CurrentHealth = m_MaxHealth;
    }

    // �ִ� ü�� ��� �� ����ü������ ����
    private void IncreasePlayerMaxHealth(int amount)
    {
        m_MaxHealth = Mathf.Min(m_MaxHealth + amount, m_LimitMaxHealth);
    }

    // ���� �浹 �� ���ظ� �� �� �ִ� �������� ������ �ܿ� 
    private void canBodyAttack()
    {
        // ���� ���� �� �����
        //playerAttack.
    }

    // �÷��̾� �ǰ� �� �����ð� ���� ����
    private IEnumerator UntouchableTime()
    {
        m_IsUntouchable = true;
        yield return new WaitForSeconds(m_UntouchableTime);
        m_IsUntouchable = false;
    }

    // �÷��̾� �� ü�� Ȯ��
    public int GetCurrentHealth()
    {
        return m_CurrentHealth;
    }

    // �÷��̾� �ִ� ü�� Ȯ��
    public int GetMaxHealth()
    {
        return m_MaxHealth;
    }

}

