using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;



public class PlayerAttack : MonoBehaviour
{
    public int attackPower = 1;
    public int attackSpeed = 1;
    public BulletSpawn bulletPerfab;

    private float attackTimer;
    public Transform attackPoint;  // ������ ���� ��ġ
    private AttackTracker tracker;


    private void Start()
    {
        tracker = GetComponent<AttackTracker>();
        attackTimer = 0f;
    }

    private void Update()
    {
        // �ð��� ���� ���� �ӵ� ����  speed = 1 �̸� 1�ʿ� 1��

        attackTimer += Time.deltaTime;

        if (attackTimer >= 1f / attackSpeed)
        {
            attackTimer = 0f;
            Debug.Log("�ʹ߻�");
            TryShoot();
        }
    }

    public void TryShoot()
    {

        if (tracker.nearestTarget != null && bulletPerfab != null)
        {
            Debug.Log("�߻�");

            // ������ ���� ����
           

            Vector3 mostos = tracker.nearestTarget.position;

            mostos.y = attackPoint.position.y;
            Vector3 direction = (mostos - attackPoint.position).normalized;
            //direction.y = attackPoint.position.y;


            //  �ش� ���������� ȸ����
            Quaternion rotation = Quaternion.LookRotation(direction);   

            // ��ȯ
            BulletSpawn newBullet = BulletSpawn.Spawn(bulletPerfab, attackPoint.position , rotation);
            newBullet.BulletStartDirection(direction);
            newBullet.attackPower = attackPower;

        }
    }

    //  ������ ���� ��ġ -  ���⿡ �߰��ؼ� ����
    public void SetAttackPoint(Transform point)
    {
        attackPoint = point;
    }

}
