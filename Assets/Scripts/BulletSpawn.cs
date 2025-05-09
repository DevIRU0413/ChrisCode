using System.Collections.Generic;
using UnityEngine;
using Scripts.Manager;

public class BulletSpawn : MonoBehaviour
{
    public int pierceCount = 0;
    public float explosionRadius = 0f;


    //ź��
    public float bulletSpeed = 10f;

    public AudioClip hitSound;
    public AudioClip explosionSound;

    // �ѹ� ������
    private Vector3 bulletDir;

    public int attackPower;


    // ������Ʈ ���� Ǯ
    private static List<BulletSpawn> pool = new List<BulletSpawn>();

    // ����� �Ѿ� ��ġ (��ġ�� ���� ������� ��)
    private Vector3 bulletPos;

    // ������ ��� ���� ���� Ȯ���ϰ� �����Ѵ�.
    [SerializeField] private float maxDistance = 10f;


    // źȯ Ǯ���� ������ �� ���� �����ϱ�   
    public static BulletSpawn Spawn(BulletSpawn prefab, Vector3 pos, Quaternion rot)
    {
        // ���� ���󰡴� �÷��̾� źȯ
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

        instance.transform.position = pos;  //  �߻� ��ġ�� �߻� ȸ�� ������ �ٲٱ�
        instance.transform.rotation = rot;
        instance.gameObject.SetActive(true);

        return instance;
    }

    // ������Ʈ ��Ȱ��ȭ �� źȯ ��ȯ
    private void ReturnPool()
    {
        gameObject.SetActive(false);
        transform.rotation = Quaternion.identity;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        pool.Add(this);
    }

    // źȯ ���� ����
    public void BulletStartDirection(Vector3 dir)
    {
        bulletDir = dir.normalized;
    }



    private void OnEnable()
    {
        bulletPos = transform.position; // �߻� ���� ��ġ ����
    }
    private void Update()
    {

        //  ź�� ����
        transform.position += bulletDir * bulletSpeed * Time.deltaTime;

        // źȯ ȸ�� (���������� ���ư��� źȯ�� ���ڿ�������)
        // transform.Rotate(0f, 0f, 360f * Time.deltaTime);

        //  ���� �Ÿ� �̻� ������ �� ź ������
        if (Vector3.Distance(bulletPos, transform.position) > maxDistance)
        {
            ReturnPool();
        }
    }

    //  ���Ϳ� �ε����� ��� ź ���ֱ�
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("�浹!!");
            // Ÿ���� ��� _ ��������
            if (hitSound != null)
            {
                AudioManager.Instance.PlaySFX(hitSound); 
            }
         

            MonsterBase monsterBase = other.gameObject.GetComponent<MonsterBase>();
            if (monsterBase != null)
            {
                monsterBase.TakeDamage(attackPower);
            }


            // ���� Ư��
            if (explosionRadius > 0f)
            {
                // ������_ ��������
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
                            otherMonster.TakeDamage(attackPower / 2); // �ֺ��� ���� ����
                        }
                    }
                }
            }

            // ����Ư��
            if (pierceCount > 0)
            {
                pierceCount--;
                return; // ���� �������Ƿ� źȯ ����
            }

            ReturnPool(); // ���� ���� ����
        }
        else if (!other.gameObject.CompareTag("Player"))
        {
            // ���̳� ��Ÿ ������Ʈ�� �ε����� �ı�
            ReturnPool();
        }
    }
}

