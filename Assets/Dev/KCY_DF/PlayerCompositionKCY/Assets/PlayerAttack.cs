using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Pool;

/*
 �⺻ ������ �����ɽ�Ʈ�� �̿��Ͽ� �÷��̾� ���� ���� ����� ������ ������ �߻��Ѵ�.
  �� 360���� �������� ���.
 ���� ������ ���� �÷����� ���̵� ���� �� �����Ѵ�.
 �⺻ ������ �߻� �ӵ��� ������ �̵��ӵ��� �����Ǵ� �ӵ��� ���� ���� �����Ѵ�.
 �⺻ ������ �������� ������ ü�¿� ���� ���� �����Ѵ�. 
 ���� źȯ�� ������Ʈ Ǯ�� �����Ѵ�.
 */

public class NewBehaviourScript : MonoBehaviour
{
    float AttackPower = 1;
    float AttackSpeed = 1;
    public GameObject PlayerBullet;
    private List<GameObject> PlayerBulletList = new List<GameObject>();
    public int size = 150;

    /*public void PlayerAttack(Monset monster)
    {
       
    
    }
    */

  

    // Start is called before the first frame update
    private void Awake()
    {

        for (int i = 0; i < size; i++)
        {
            GameObject instance = Instantiate(PlayerBullet);
            instance.gameObject.SetActive(false);
            PlayerBulletList.Add(instance);
        }
    }

   /* public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if (PlayerBulletList.Count == 0)
        {
            return Instantiate(PlayerBullet);
        }
        GameObject instance = PlayerBulletList[PlayerBulletList - 1];
        PlayerBulletList.RemoveAt(PlayerBulletList.Count - 1);

        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);

        return instance;

    }
   */

    // Update is called once per frame
    void Update()
    {
        
    }
}
