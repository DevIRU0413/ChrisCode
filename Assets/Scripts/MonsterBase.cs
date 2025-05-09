using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Manager;
using UnityEditor;
using UnityEngine;
using Scripts;
using UnityEngine.Events;

//일반 몬스터 타입
public enum MonsterType{Goblin, Skeleton, Etc}

//공격 타입
public enum AttackType{Meelee =0, Ranged = 1, None =2}

public class MonsterBase : MonoBehaviour
{
    //체력 관련
    [Header("Hp")]
    //체력
    [SerializeField] protected int m_hp = 10;
    public int HP => m_hp;
    //최대 체력
    [SerializeField] protected int m_maxHp= 10;
    public int MaxHP => m_maxHp;


    //공격 관련

    [Header("Attack Func")]
    //공격 타입(근거리, 원거리) -> 기본값은 근거리.
    public AttackType attackType;
    //공격 데미지
    [SerializeField] protected int m_attackDamage = 2;
    public int AttackDMG => m_attackDamage;
    //충돌(몸박) 데미지
    [SerializeField] protected int m_collsionDamage = 1;

    //공격 쿨타임
    [SerializeField] protected float m_attackCooldown = 1f;
    //공격 쿨타임 코루틴
    //공격 가능 여부 변수
    [SerializeField] private bool m_canAttack = true;

    [SerializeField] private bool m_isAttacking = false;

    [SerializeField] private bool m_isPlayerInAttackArea = false;
    [SerializeField] private SphereCollider m_sphereCollider;
    [SerializeField] protected float m_attackRange =2f;

    private Coroutine m_attackCoroutine;
    public UnityAction<int> OnDieAction;


    //기타 정보들 ~~~
    [Header("Status Info")] 
    [SerializeField] protected Rigidbody m_rb;
    [SerializeField] protected float m_moveSpeed =3f;
    // [SerializeField] protected bool m_isMove = false;
    public MonsterType monsterType;
    [SerializeField] protected bool m_isMonsterDie = false;
    [SerializeField] private float m_speed;
    public bool isPool=false;

    [Header("Raged Info")]
    [SerializeField] private ArrowPool m_arrowPool;
    [SerializeField] private Transform m_arrowShootingTransform;
    [SerializeField] private float m_arrowSpeed =10f;

    [Header("Tracking")]
    [SerializeField]private Transform m_playerPos;
    //플레이어 추적 기능 가진 몬스터 변수 
    // (기본값: true, 원형포진이거나, 일정 방향으로 날아가는 몬스터는 false)
    [SerializeField]protected bool m_canTrackingPlayer= true;

    [Header("플레이어")]
    //플레이어 체력
    [SerializeField] private PlayerHp playerHp;

    [Header("애니메이션")]
    [SerializeField] protected Animator anim;
    public GameResultState gameResultState;

    void OnEnable()
    {
        FindingPlayer();
        InitStatus();
    }

    protected virtual void OnDisable()
    {
        if(m_attackCoroutine !=null)
        {
            StopCoroutine(m_attackCoroutine);
            m_attackCoroutine =null;
        }
        m_isPlayerInAttackArea = false;
        m_canAttack=true;

    }
    protected virtual void Awake()
    {
        m_rb= GetComponent<Rigidbody>();    
        anim= GetComponent<Animator>();
    }

    protected virtual void Start()
    {
       
        //씬 내의 플레이어 태그를 가진 오브젝트 1개를 찾기. (플레이어 2개시 해당 라인 변경 바람.)
        FindingPlayer();
        if(m_sphereCollider !=null)
        {
            m_sphereCollider.radius = m_attackRange;
        }
        if (attackType == AttackType.Ranged)
        {
            m_arrowPool = ArrowPool.s_instance; 
        }
    }

    protected virtual void Update()
    {
        MonsterAnimationController();

        gameResultState= InGameManager.Instance.GetGameResultState();
        // 플레이어 객체 
        if(m_playerPos  == null || m_isMonsterDie == true) return;
            
        //공격범위 내에 있을 경우 플레이어를 바라보면서 공격, 아닐 경우 플레이어 위치 추척하며 이동
        if (m_canTrackingPlayer == true)
        {
            //공격범위에 플레이어가 없을 때
            if (m_isPlayerInAttackArea == false) 
            {
                //공격중이 아니면 플레이어 추적적
                if(m_isAttacking == false)
                {
                    MoveToPlayer();
                }
                //공격중이면 움직임 정지.
                else
                {
                    m_rb.velocity = Vector3.zero;
                }
            }
            else
            {
                if(m_isAttacking == false)// 공격중이 아닐 때 
                {
                    LookPlayer();
                }
                else
                {
                    m_rb.velocity = Vector3.zero;
                }

                if (m_canAttack)
                {
                    AttackPlayer();
                    m_canAttack = false;
                }
            }
        }
    }


    protected virtual void InitStatus()
    {
        //애니메이션 상태 초기화
        m_isMonsterDie = false;

        //리지드 바디 초기화
        m_rb.constraints = RigidbodyConstraints.FreezeRotation;
        // m_rb.constraints &= ~RigidbodyConstraints.FreezeRotationY; //y축 회전 문제 없애기
        m_rb.isKinematic = false;

        //스텟 및 상태 초기화
        m_hp = m_maxHp;
        m_isPlayerInAttackArea = false;
        m_canAttack= true;
        

    }
    #region 몬스터의 플레이어 추적 로직

    protected virtual void FindingPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            m_playerPos = playerObj.transform;
        }
    }
    //플레이어 위치 & 방향
    protected virtual void MoveToPlayer()
    {
        if(m_playerPos == null) return;
        Vector3 direction = (m_playerPos.transform.position - transform.position).normalized;
        Vector3 velocity = direction * m_moveSpeed;

        m_rb.velocity = new Vector3(velocity.x, m_rb.velocity.y, velocity.z);
        
        Vector3 lookDir = m_playerPos.position - transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }
    protected virtual void LookPlayer()
    {
        if (m_playerPos == null) return;
        m_rb.velocity = Vector3.zero;
        Vector3 direction = m_playerPos.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    //플레이어 사망 이벤트
    protected virtual void HandlePlayerDied()
    {
        Debug.Log("플레이어 사망 이벤트");
        //공격 사거리 및 위치 해제
        m_isPlayerInAttackArea = false;
        m_playerPos = null;
    }


    #endregion

    #region 몬스터 피격 & 공격 로직

    // 피해 받는 로직
    public void TakeDamage(int damage)
    {
        if(m_isMonsterDie == true) return;

        m_hp -= damage;
        m_hp = Mathf.Clamp(m_hp, 0, m_maxHp);
        
        //TODO<김승태> 필요시: 몬스터 체력 바 UI 이벤트 호출
        
        if(m_hp <=0)
        {
            Debug.Log("몬스터 사망");
            OnDieAction?.Invoke(10);
            MonsterDied();
            return;
        }

        //피격 상태에서 피격상태가 아님에도 
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            anim.SetTrigger("isHit");
        }
    }

    //몸박 로직
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")&& m_isMonsterDie == false)
        {
            //플레이어가 몬스터와 충돌할 경우-> 플레이어가 몸박 데미지 만큼 피해를 입게 함.
            playerHp = collision.gameObject.GetComponent<PlayerHp>();
            playerHp.TakeDamage(m_collsionDamage);
        }
    }
    
    #region 근거리 공격

    //공격 로직: sphere 트리거를 이용해 공격 범위를 설정.
    protected virtual void AttackPlayer()
    {
        if(m_playerPos != null)
        {
            Debug.Log($"{gameObject.name} : 플레이어에게 공격로직 실행");
            anim.SetTrigger("isAttack");

            //공격 이펙트 오브젝트 풀 패턴
        }
    }

    //공격 애니메이션 첫 프레임에 호출출
    public void HitboxStart()
    {
        m_isAttacking= true;
        GetComponentInChildren<MeeleeHitbox>().EnableHitbox();
    }
    //공격 애니메이션 마지막 프레임에 호출
    public void HitboxEnd()
    {
        m_isAttacking = false;
        AttackCoolDownStart();
        GetComponentInChildren<MeeleeHitbox>().DisableHitbox();
    }
    #endregion

    #region 원거리 공격

    //모션 중간에 발사하도록 애니메이션 프레임에 삽입
    public void ShotingArrow()
    {
        //오브젝트 풀링
        GameObject arrowObject = m_arrowPool.GetBulletPool();

        Vector3 directionToPlayer = (m_playerPos.position - m_arrowShootingTransform.position).normalized;

        arrowObject.GetComponent<Arrow>().ArrowInit(m_arrowShootingTransform,directionToPlayer,m_arrowPool, m_arrowSpeed, AttackDMG);
    }

    //공격 애니메이션 첫 프레임에 호출
    public void ShootArrowStart()
    {
        m_isAttacking= true;
    }
    //공격 애니메이션 마지막 프레임에 호출
    public void ShootArrowEnd()
    {
        m_isAttacking = false;
        AttackCoolDownStart();
    }
    #endregion

    #region 마법 공격

    //공격 애니메이션 첫 프레임에 호출출
    public void MageAttackStart()
    {
        m_isAttacking= true;
    }
    //공격 애니메이션 마지막 프레임에 호출
    public void MageAttackwEnd()
    {
        m_isAttacking = false;
        AttackCoolDownStart();
    }
    #endregion

    //공격 쿨타임 코루틴
    private IEnumerator IE_AttackCooldown()
    {
        yield return new WaitForSeconds(m_attackCooldown);
        m_canAttack = true;
        m_attackCoroutine = null;
    }
    
    //공격 쿨타임 시작 
    public void AttackCoolDownStart()
    {
        if (m_attackCoroutine == null)
        {
            m_attackCoroutine = StartCoroutine(IE_AttackCooldown());
        }
    }

    //공격범위 감지 트리거
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerHp = other.GetComponent<PlayerHp>();
            m_isPlayerInAttackArea = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_isPlayerInAttackArea = false;
            playerHp =null;
        }
    }

    //공격범위 기즈모
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        if(m_sphereCollider !=null)
        {
            Gizmos.DrawWireSphere(m_sphereCollider.transform.TransformPoint(m_sphereCollider.center),m_sphereCollider.radius);
            // Gizmos.DrawWireSphere(m_sphereCollider.transform.position+m_sphereCollider.center,m_sphereCollider.radius);
        }
    }
    #endregion

    #region 몬스터 생성 및 사망 로직
    //몬스터 사망 로직
    protected virtual void MonsterDied() 
    {
        m_isMonsterDie = true;
        m_rb.freezeRotation = true; //프리징
        m_rb.isKinematic = true;
    }

    // monster Die Animation Clip의 마지막 프레임에 호출
    protected virtual void ReturnPoolOrDestory()
    {
        if(isPool == true)
        {
            Debug.Log($"{gameObject.name} 풀 반납");
            MonsterPoolManager.Instance.ReturnPool(this);
        }
        else
        {
            Debug.Log($"{gameObject.name} 파괴");

            Destroy(gameObject);
        }
    }
    #endregion

    #region 몬스터 애니메이션 관리

    protected virtual void MonsterAnimationController()
    {
        m_speed = m_rb.velocity.magnitude;
        anim.SetFloat("Speed",m_speed,0.2f,Time.deltaTime);
        anim.SetBool("isMonsterDie", m_isMonsterDie);
        anim.SetInteger("AttackType", (int)attackType);
        anim.SetInteger("GameResultState", (int)gameResultState);
    }
    #endregion
}
