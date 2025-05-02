using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    //ź��
    public float bulletSpeed = 10f;

    // �ѹ� ������
    private Vector3 bulletDir;


    // ������Ʈ ���� Ǯ
    private static List<BulletSpawn> pool = new List<BulletSpawn>();
    
    // ����� �Ѿ� ��ġ (��ġ�� ���� ������� ��)
    private Vector3 bulletPos;

    // ������ ��� ���� ���� Ȯ���ϰ� �����Ѵ�.
    [SerializeField]private float maxDistance = 10f;


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


        //  ���� �Ÿ� �̻� ������ �� ź ������
        if (Vector3.Distance(bulletPos, transform.position) > maxDistance)
        {
            ReturnPool();
        }
    }

    //  ���Ϳ� �ε����� ��� ź ���ֱ�
    private void OnTriggerEnter(Collider monster)
    {
        if (monster.CompareTag("Monster"))
        {
            ReturnPool();
        }
    }

}
