using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerHp : MonoBehaviour
{
    public int CurrentHealth = 5;  // ó�� 5�� ����
    public int MaxHealth = 5;
    public int LimitMaxHealth = 10;
    private bool isUntouchable = false;
    private float untouchableTime = 2f;

    private void Awake()
    {
        tag = "Player";
    }

    // ���� ���� �� ĳ������ ü���� ���� ���� �ִ� ü������ ����
    void Start()
    {
        MaxHealth = 5;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �÷��̾� ������ ���
    public void TakeDamage(int damage)
    {
        if (IsUntouchable == true)
        {
            return;
        }
        // ���� �ð�
        CurrentHealth -= damage;
        StartCoroutine(UntouchableTime());

        if (CurrentHealth <= 0)
        {
            Debug.Log(" ���� ����/ ���ư���");
        }
    }

    // �÷��̾� ü�� ȸ�� �� ȣ��
    /*
    public void IncreasePlayerCurrentHealth(int amount)
    {
        // ü�� ���ڿ��� �浹, ����, ���� �̺�Ʈ���� ü�� ȸ�� �̺�Ʈ�� ���� ��� ȣ��
        // ��ȣ
        m_CurrentHealth = Mathf.Min(m_CurrentHealth + 1, m_MaxHealth);
        m_CurrentHealth = m_MaxHealth;
    }
    */

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        Debug.Log($"ü�� ȸ��: +{amount}, ���� ü��: {CurrentHealth}/{MaxHealth}");
    }


    // �ִ� ü�� ��� �� ����ü������ ����
    public void IncreasePlayerMaxHealth(int amount)
    {
        MaxHealth = Mathf.Min(MaxHealth + amount, LimitMaxHealth);
    }

    // ���� �浹 �� ���ظ� �� �� �ִ� �������� ������ �ܿ� 
    public void CanBodyAttack()
    {
        // ���� ���� �� �����
        //playerAttack.
    }

    // �÷��̾� �ǰ� �� �����ð� ���� ����
    public IEnumerator UntouchableTime()
    {
        IsUntouchable = true;
        yield return new WaitForSeconds(untouchableTime);
        IsUntouchable = false;
    }

    // �÷��̾� �� ü�� Ȯ��
    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }

    // �÷��̾� �ִ� ü�� Ȯ��
    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public bool IsUntouchable
    {
        get => isUntouchable;
        set
        {
            if (value && !isUntouchable)
            {
                isUntouchable = true;
                StartCoroutine(UntouchableTime());
            }
        }
    }
}
