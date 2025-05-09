using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody m_rb;
    private ArrowPool m_arrowPool;
    private int m_damage;
   //타이머 변수
   private float m_timer;
   private bool m_isReturned = false;

    //화살 위치
   private Transform m_shotingTransform;
   
   [SerializeField] private float m_disappearArrowTime =3f;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        //화살끼리의 충돌 무시
        Physics.IgnoreLayerCollision(this.gameObject.layer,this.gameObject.layer,true);
        //화살과 몬스터의 충돌 무시
        Physics.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Monster"),true);
    }


    void Update()
    {
        //화살 앞 방향 세팅
        if(m_rb.velocity.magnitude >2)
        {
            transform.forward =m_rb.velocity;
        }
        //타이머 동작
        m_timer += Time.deltaTime;
        //타이머가 화살이 사라지는 시간과 같다면 ->화살 파괴
        if(m_timer >= m_disappearArrowTime)
        {
            //풀에 반납하기
            ReturnToPool();
        }
    }

    //몬스터와 충돌 시
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            PlayerHp player = collision.gameObject.GetComponent<PlayerHp>();
            if(player !=null)
            {
                player.TakeDamage(m_damage);
                Debug.Log($"플레이어 피해 로직 호출. 데미지 : {m_damage}");
            }
        }
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if(m_isReturned == true) return;
        m_isReturned = true;
        m_rb.velocity = Vector3.zero;
        m_arrowPool.ReturnPool(this.gameObject);
    }

    
    public void ArrowInit(Transform arrowTransform, Vector3 direction, ArrowPool pool, float arrowSpeed, int damage)
    {
        //풀링 초기화
        this.m_arrowPool =pool;
        //화살 데미지
        this.m_damage = damage;
        this.m_timer = 0f;
        this.m_isReturned = false;
        

        //화살 위치
        m_shotingTransform = arrowTransform;
        transform.position = arrowTransform.position;

        transform.forward = direction;


        Rigidbody bulletRb = GetComponent<Rigidbody>();
        
        //화살 속도
        bulletRb.velocity = arrowSpeed * direction;

        //타이머 초기화
        m_timer =0f;
        //게임 오브젝트 활성화
        gameObject.SetActive(true);
    }

}
