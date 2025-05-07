using UnityEngine;



public class PlayerAttack : MonoBehaviour
{
    public int attackPower = 1;
    public int attackSpeed = 1;
    public BulletSpawn bulletPerfab;
    public int extraProjectileCount = 0;          // �߰� �߻�ü ��
    public int pierceCount = 0;                           // ���� ���� ���� Ƚ��
    public float projectileSizeMultiplier = 1f;  // �߻�ü ũ�� ����
    public float explosionRadius = 0f;              // ���� �ݰ�


    private float attackTimer;
    public Transform attackPoint;  // ������ ���� ��ġ
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
            // �ǰ�, ���� �� ���� ����
            return;
        }

        // �ð��� ���� ���� �ӵ� ����  speed = 1 �̸� 1�ʿ� 1��

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

            // �߾� �߻�
            ShootInDirection(direction);

            // �¿� �߰� �߻�ü (����)
            float angleStep = 15f;
            for (int i = 1; i <= extraProjectileCount; i++)
            {
                Vector3 left = Quaternion.Euler(0, -i * angleStep, 0) * direction;
                Vector3 right = Quaternion.Euler(0, i * angleStep, 0) * direction;

                ShootInDirection(left);
                ShootInDirection(right);
            }
        }
    }

    private void ShootInDirection(Vector3 dir)
    {
        Quaternion rot = Quaternion.LookRotation(dir);
        BulletSpawn bullet = BulletSpawn.Spawn(bulletPerfab, attackPoint.position, rot);
        bullet.BulletStartDirection(dir);
        bullet.attackPower = attackPower;
        bullet.pierceCount = pierceCount;
        bullet.explosionRadius = explosionRadius;
        bullet.transform.localScale *= projectileSizeMultiplier;
    }

    //  ������ ���� ��ġ -  ���⿡ �߰��ؼ� ����
    public void SetAttackPoint(Transform point)
    {
        attackPoint = point;
    }

}
