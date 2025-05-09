using System.Collections.Generic;
using UnityEngine;
using Scripts.Manager;

public class BulletSpawn : MonoBehaviour
{
    public int pierceCount = 0;
    public float explosionRadius = 0f;


    //탄속
    public float bulletSpeed = 10f;

    public AudioClip hitSound;
    public AudioClip explosionSound;

    // 총발 방향계산
    private Vector3 bulletDir;

    public int attackPower;


    // 오브젝트 저장 풀
    private static List<BulletSpawn> pool = new List<BulletSpawn>();

    // 사용자 총알 위치 (위치에 따라 사라지게 함)
    private Vector3 bulletPos;

    // 다음의 경우 게임 맵을 확인하고 변경한다.
    [SerializeField] private float maxDistance = 10f;


    // 탄환 풀에서 꺼내기 및 새로 생성하기   
    public static BulletSpawn Spawn(BulletSpawn prefab, Vector3 pos, Quaternion rot)
    {
        // 실제 날라가는 플레이어 탄환
        BulletSpawn instance;

        if (pool.Count > 0)
        {
            instance = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        }
        else
        {
            instance = Instantiate(prefab);
        }

        instance.transform.position = pos;  //  발사 위치와 발사 회전 정도를 바꾸기
        instance.transform.rotation = rot;
        instance.gameObject.SetActive(true);

        return instance;
    }

    // 오브젝트 비활성화 및 탄환 반환
    private void ReturnPool()
    {
        gameObject.SetActive(false);
        transform.rotation = Quaternion.identity;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        pool.Add(this);
    }

    // 탄환 방향 설정
    public void BulletStartDirection(Vector3 dir)
    {
        bulletDir = dir.normalized;
    }



    private void OnEnable()
    {
        bulletPos = transform.position; // 발사 시점 위치 저장
    }
    private void Update()
    {

        //  탄속 설정
        transform.position += bulletDir * bulletSpeed * Time.deltaTime;

        // 탄환 회전 (직선형으로 날아가는 탄환은 부자연스러움)
        // transform.Rotate(0f, 0f, 360f * Time.deltaTime);

        //  일정 거리 이상 떨어질 때 탄 없에기
        if (Vector3.Distance(bulletPos, transform.position) > maxDistance)
        {
            ReturnPool();
        }
    }

    //  몬스터와 부딪히는 경우 탄 없애기
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("충돌!!");
            // 타격음 재생 _ 수동설정
            if (hitSound != null)
            {
                AudioManager.Instance.PlaySFX(hitSound); 
            }
         

            MonsterBase monsterBase = other.gameObject.GetComponent<MonsterBase>();
            if (monsterBase != null)
            {
                monsterBase.TakeDamage(attackPower);
            }


            // 폭발 특성
            if (explosionRadius > 0f)
            {
                // 폭발음_ 수동설정
                if (explosionSound != null)
                { AudioManager.Instance.PlaySFX(explosionSound); }

                Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Monster") && hit != other.gameObject)
                    {
                        MonsterBase otherMonster = hit.GetComponent<MonsterBase>();
                        if (otherMonster != null)
                        {
                            otherMonster.TakeDamage(attackPower / 2); // 주변은 절반 피해
                        }
                    }
                }
            }

            // 관통특성
            if (pierceCount > 0)
            {
                pierceCount--;
                return; // 관통 남았으므로 탄환 유지
            }

            ReturnPool(); // 관통 없음 제거
        }
        else if (!other.gameObject.CompareTag("Player"))
        {
            // 벽이나 기타 오브젝트에 부딪히면 파괴
            ReturnPool();
        }
    }
}

