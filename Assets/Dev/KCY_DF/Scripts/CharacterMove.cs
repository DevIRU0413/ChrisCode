using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMove : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    public float moveSpeed = 0.05f;
    public float jumpPower = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // ĳ������ �浹�� ���� ������Ʈ ȸ�� ����
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Debug.Log($"�Է� Ȯ��: H={h}, V={v}");

        moveDirection = new Vector3(h, 0, v).normalized;
    }

    void FixedUpdate()
    {
        Debug.Log("ȣ�� �ǳ���?");
        Vector3 moveOffset = moveDirection * moveSpeed;
        Debug.Log($"[�ӵ� Ȯ��] moveSpeed: {moveSpeed}, moveOffset: {moveOffset}");
        rb.MovePosition(rb.position + moveOffset);
    }
    //  ���� ������ ��ũ��Ʈ ���� Ȯ���� ��
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            PlayerHp playerHp = GetComponent<PlayerHp>();
            {
                if (playerHp != null)
                {
                    playerHp.TakeDamage(int damage);
                }
            }
        }
    }

    */

}
