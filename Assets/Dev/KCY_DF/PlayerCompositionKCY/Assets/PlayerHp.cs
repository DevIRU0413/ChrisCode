using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public int CurrentHealth = 5;  // ó�� 5�� ����
    public int MaxHealth = 5;
    public int LimitMaxHealth = 10;


    // ���Ϳ��� �������� �޴� ���  Update() �κп��� ���� (�ε����� �Ǵ� ���Ͱ� ��� ���ݿ� �ε�����)
    public void TakeDamage()
    {
        CurrentHealth -= 1;
        if (CurrentHealth <= 0)
        {
            Debug.Log(" ���� ����/ ���ư���");
        }
    }

    // Ǯ���̾ ����ִ� �ڵ�,  collision(����)�� �ε��� ��� ����ġ�Ǹ� �޴´�.
    // �÷��̾ ���� �ڿ� �ٽ� �������� ���� ������, ������������ �˹� �����غ���
    public void OnCollisionEnter(Collision collision)
    {


        // ���� ���� �Ǵ� ���� �浹 �ÿ��� ����
        /*public void OnCollisionEnter(Collision collision)
         * {
         *    string tag = collison.gameObject.tag
         *    if(tag == Monster || MonsterAttack )
         *      {
         *         CurrentHealth -= 1;
                     if (CurrentHealth <= 0)
                         {
                             Debug.Log(" ���� ����/ ���ư���");
                         }
         *      }
         *  }
        */


        // ���� ���� �� ĳ������ ü���� ���� ���� �ִ� ü������ ����
        void Start()
        {
            MaxHealth = 5;
            CurrentHealth = MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            /*
             * if()
             *
             * 
            */

        }
    }
}
