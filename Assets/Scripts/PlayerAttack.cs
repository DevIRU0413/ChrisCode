using UnityEngine;
using Scripts.Manager;


public class PlayerAttack : MonoBehaviour
{
    public int attackPower = 1;
    public int attackSpeed = 1;
    public BulletSpawn bulletPerfab;
    public int extraProjectileCount = 0;          // 추가 발사체 수
    public int pierceCount = 0;                           // 몬스터 관통 가능 횟수
    public float projectileSizeMultiplier = 1f;  // 발사체 크기 배율
    public float explosionRadius = 0f;              // 폭발 반경
    public AudioClip attackSound;

    private float attackTimer;
    public Transform attackPoint;  // 공격이 나갈 위치
    private AttackTracker tracker;
    private PlayerHp playerHp;

    private void Start()
    {
        tracker = GetComponent<AttackTracker>();
        playerHp = GetComponentInParent<PlayerHp>();
        attackTimer = 0f;
    }

    private void Update()
    {
        if (playerHp == null || playerHp.isDead || playerHp.isHit)
        {
            // 피격, 죽을 때 공격 금지
            return;
        }

        // 시간에 따른 공격 속도 설정  speed = 1 이면 1초에 1번

        attackTimer += Time.deltaTime;

        if (attackTimer >= 1f / attackSpeed)
        {
            attackTimer = 0f;
            TryShoot();
        }


    }

    public void TryShoot()
    {
        if (tracker.nearestTarget != null && bulletPerfab != null)
        {
            Vector3 targetPos = tracker.nearestTarget.position;
            targetPos.y = attackPoint.position.y;

            Vector3 direction = (targetPos - attackPoint.position).normalized;

            // 중앙 발사
            ShootInDirection(direction);

            // 좌우 추가 발사체 (샷건)
            float angleStep = 15f;
            for (int i = 1; i <= extraProjectileCount; i++)
            {
                Vector3 left = Quaternion.Euler(0, -i * angleStep, 0) * direction;
                Vector3 right = Quaternion.Euler(0, i * angleStep, 0) * direction;

                ShootInDirection(left);
                ShootInDirection(right);
            }

            if (attackSound != null)
            {
                AudioManager.Instance.PlaySFX(attackSound);
            }
        }
    }

    private void ShootInDirection(Vector3 dir)
    {
        Quaternion rot = Quaternion.LookRotation(dir);
        rot.x = 0f;
        rot.z = 0f;
        BulletSpawn bullet = BulletSpawn.Spawn(bulletPerfab, attackPoint.position, rot);
        bullet.BulletStartDirection(dir);
        bullet.attackPower = attackPower;
        bullet.pierceCount = pierceCount;
        bullet.explosionRadius = explosionRadius;
        bullet.transform.localScale *= projectileSizeMultiplier;
    }

    //  공격이 나갈 위치 -  무기에 추가해서 설정
    public void SetAttackPoint(Transform point)
    {
        attackPoint = point;
    }

}
