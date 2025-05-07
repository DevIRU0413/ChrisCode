using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerHp : MonoBehaviour
{
    public int CurrentHealth = 5;  // ó�� 5�� ����
    public int MaxHealth = 5;  
    public int LimitMaxHealth = 10;
    public bool isDead = false;
    public bool isHit = false;
    private bool isUntouchable = false;
    private float untouchableTime = 2f;
    private float timeSinceLastHit = 0f;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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
        timeSinceLastHit += Time.deltaTime;
    }

    // �÷��̾� ������ ���
    public void TakeDamage(int damage)
    {
        // �ߺ����� �ǰ� ����
        if (isDead || isHit) return; 

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            PlayerDeath();
            return;
        }
        if (animator != null)
        {
            // ���� ���� ��� �ش� Ʈ����(�ǰ� Ʈ����) ����
            animator.SetTrigger("HitTrigger");
        }
        
        //�ǰ� �ִϸ��̼� ���� �� ���� �÷��̾��� ������ ����
        StartCoroutine(HitLock());
    }

    public void PlayerDeath()
    {  
        if (isDead) return;
        isDead = true;
        if (animator != null)
        {
            animator.SetTrigger("DeathTrigger");
        }

        // �ش� ��ũ��Ʈ�� �پ��ִ� ��� �׾��� �� �̵� ��ũ��Ʈ ��Ȱ��ȭ 
        CharacterMove moveScript = GetComponent<CharacterMove>();
        if (moveScript != null)
        {
            moveScript.enabled = false;
        }

        // �÷��̾� ĳ���� ��� �� ���� �ۿ� ����
        GetComponent<Rigidbody>().velocity = Vector3.zero;
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

    // �浿 �� ������ ���
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster") && timeSinceLastHit >= untouchableTime)
        {
            TakeDamage(1);
            timeSinceLastHit = 0f;

            // ���� �Լ� ���� ���� ����
            //TakeDamage(MonsterBase.attackDamage);  
        }
    }


    // �÷��̾� �ǰ� �� �����ð� ���� ����
    public IEnumerator UntouchableTime()
    {
        IsUntouchable = true;
        yield return new WaitForSeconds(untouchableTime);
        IsUntouchable = false;
    }
    public IEnumerator HitLock()
    {
        isHit = true;

        // �ִϸ��̼� ����
        yield return new WaitForSeconds(0.4f);
        isHit = false;

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

    // �ǰ� �� ������ ���߱� ���� �ڷ�ƾ (�ǰ� Ȯ�ο�)
    public IEnumerator HitLock(float duration = 0.4f)
    {
        isHit = true;
        yield return new WaitForSeconds(duration);  // �ǰ� �ִϸ��̼� �ð�
        isHit = false;
    }

    public bool IsUntouchable
    {
        get => isUntouchable;
        set
        {
            if (value && !isUntouchable)
            {
                StartCoroutine(UntouchableTime());
            }
        }
    }
}
