using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Manager;

public class PMS_PlayerHp : MonoBehaviour
{
    public int CurrentHealth = 5;  // 처음 5로 시작
    public int MaxHealth = 5;
    public int LimitMaxHealth = 10;
    public bool isDead = false;
    public bool isHit = false;
    public AudioClip hitSound;
    public AudioClip deathSound;

    private bool isUntouchable = false;
    private float untouchableTime = 2f;
    private float timeSinceLastHit = 0f;
    private Animator animator;

    [SerializeField] public GameObject GameClearUI;
    [SerializeField] public GameObject GameOverUI;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        tag = "Player";
    }

    // 게임 시작 시 캐릭터의 체력을 현재 상한 최대 체력으로 설정
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

    // 플레이어 데미지 계산
    public void TakeDamage(int damage)
    {
        // 중복으로 피격 방지
        if (isDead || isHit) return;

        CurrentHealth -= damage;

        // 피격 시 사운드 _ 수동설정
        if (hitSound != null)
        {
            AudioManager.Instance.PlaySFX(hitSound);
        }

        if (CurrentHealth <= 0)
        {
            PlayerDeath();
            TMP_UIManager.Instance.OpenUI(GameOverUI);
            return;
        }
        if (animator != null)
        {
            // 죽지 않은 경우 해당 트리거(피격 트리거) 실행
            animator.SetTrigger("HitTrigger");
        }

        //피격 애니메이션 끝날 때 까지 플레이어의 공격을 막음
        StartCoroutine(HitLock());
    }

    public void PlayerDeath()
    {
        if (isDead) return;
        isDead = true;

        // 사망 시 사운드 
        if (deathSound != null)
        {
            AudioManager.Instance.PlaySFX(deathSound);
        }

        if (animator != null)
        {
            animator.SetTrigger("DeathTrigger");
        }

        // 해당 스크립트가 붙어있는 경우 죽었을 때 이동 스크립트 비활성화 
        CharacterMove moveScript = GetComponent<CharacterMove>();
        if (moveScript != null)
        {
            moveScript.enabled = false;
        }

        // 플레이어 캐릭터 사망 후 관성 작용 제어
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // 플레이어 체력 회복 시 호출
    /*
    public void IncreasePlayerCurrentHealth(int amount)
    {
        // 체력 상자와의 충돌, 상점, 게임 이벤트에서 체력 회복 이벤트가 있을 경우 호출
        // 괄호
        m_CurrentHealth = Mathf.Min(m_CurrentHealth + 1, m_MaxHealth);
        m_CurrentHealth = m_MaxHealth;
    }
    */

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        Debug.Log($"체력 회복: +{amount}, 현재 체력: {CurrentHealth}/{MaxHealth}");
    }


    // 최대 체력 상승 및 상한체력으로 제한
    public void IncreasePlayerMaxHealth(int amount)
    {
        MaxHealth = Mathf.Min(MaxHealth + amount, LimitMaxHealth);
    }

    // 몬스터 충돌 시 피해를 줄 수 있는 아이템을 소지할 겨우 
    public void CanBodyAttack()
    {
        // 공격 구현 후 만들기
        //playerAttack.
    }

    // 충동 시 데미지 계산
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster") && timeSinceLastHit >= untouchableTime)
        {
            TakeDamage(1);
            timeSinceLastHit = 0f;

            // 몬스터 함수 보고 추후 수정
            //TakeDamage(MonsterBase.attackDamage);  
        }
    }


    // 플레이어 피격 시 일정시간 무적 패턴
    public IEnumerator UntouchableTime()
    {
        IsUntouchable = true;
        yield return new WaitForSeconds(untouchableTime);
        IsUntouchable = false;
    }
    public IEnumerator HitLock()
    {
        isHit = true;

        // 애니메이션 길이
        yield return new WaitForSeconds(0.4f);
        isHit = false;

    }

    // 플레이어 현 체력 확인
    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }

    // 플레이어 최대 체력 확인
    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    // 피격 시 공격을 멈추기 위한 코루틴 (피격 확인용)
    public IEnumerator HitLock(float duration = 0.4f)
    {
        isHit = true;
        yield return new WaitForSeconds(duration);  // 피격 애니메이션 시간
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