using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;



public class PlayerAttack : MonoBehaviour
{
    public float attackPower = 1;
    public float attackSpeed = 1;

    private float attackTimer;
    private Transform attackPoint;  // ������ ���� ��ġ
    private AttackTracker tracker;
    private BulletSpawn bulletPerfab;


    private void Start()
    {
        tracker = GetComponent<AttackTracker>();
        attackPoint = transform;
        attackTimer = 0f;
    }

    private void Update()
    {
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
            // ������ ���� ����
            Vector3 direction = (tracker.nearestTarget.position - attackPoint.position).normalized; 

            //  �ش� ���������� ȸ����
            Quaternion rotation = Quaternion.LookRotation(direction);   

            // ��ȯ
            BulletSpawn newBullet = BulletSpawn.Spawn(bulletPerfab, attackPoint.position, rotation);
        }
    }

    //  ������ ���� ��ġ -  ���⿡ �߰��ؼ� ����
    public void SetAttackPoint(Transform point)
    {
        attackPoint = point;
    }

}
