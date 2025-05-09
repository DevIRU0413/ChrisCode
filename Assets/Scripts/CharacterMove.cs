using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMove : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Transform characterModel; // Animator�� �ִ� �ڽ� ��

    public float moveSpeed = 3f;
    public float rotateSpeed = 720f;

    private Vector3 inputDirection;
    private float previousHorizontal = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // ĳ���� ���� ȸ�� ����
        rb.freezeRotation = true;  

        if (characterModel != null)
        {
            // ���� �ƹ�Ÿ�� �ִ� ���ϸ���Ʈ ������Ʈ ȣ��
            animator = characterModel.GetComponent<Animator>();
        }
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // �밢�� ����
        inputDirection = new Vector3(h, 0, v).normalized;

        if (animator != null)
        {
            // �ִϸ��̼ǿ��� �ٴ� ����� ���� Speed ���� (���� ����ȭ�� �Բ� 0~1�� ���� ����)
            float speedValue = inputDirection.magnitude;
            animator.SetFloat("Speed", speedValue);

            // ���� ���¿��� ��/�� �Է��� ������ speed�� �����ϰ� ȸ�� Ʈ���� ����
            if (speedValue < 0.1f)
            {
                if (previousHorizontal == 0 && h < 0)
                {
                    animator.SetTrigger("TurnLeft");
                }
                else if (previousHorizontal == 0 && h > 0)
                {
                    animator.SetTrigger("TurnRight");
                }
            }
        }

        // �̵� ���⿡ ���� ĳ���� ȸ��
        if (inputDirection.magnitude > 0.1f)
        {
            // �ֻ� ���� ȸ��(�÷��̾� ���� ȸ��)
            Quaternion targetRot = Quaternion.LookRotation(inputDirection);
            Vector3 targetEuler = targetRot.eulerAngles;

            // Y�� ȸ���� ����� ȸ��
            Quaternion yRotationOnly = Quaternion.Euler(0f, targetEuler.y, 0f);

            // ��ǥ ���������� �ε巯�� ȸ��
            characterModel.rotation = Quaternion.RotateTowards(characterModel.rotation, yRotationOnly, rotateSpeed * Time.deltaTime);
        }
        // ���� �������� ���� �����, ������ �ִϸ��̼� �ߺ� ����
        previousHorizontal = h;
    }

    // �÷��̾� �̵� ����
    void FixedUpdate()
    {
        Vector3 moveOffset = inputDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveOffset);
    }

    // Player�� �ƹ�Ÿ ĳ���� �ݶ��̴� ��ġ
    void LateUpdate()
    {
        Vector3 offset = characterModel.position - transform.position;

        // �̵����� �ʹ� ũ�� ���� (�ִϸ��̼� ���� ����)
        if (offset.magnitude < 1f)
        {
            // Player�� ���� �ƹ�Ÿ�� xz�� ���߰� y�� �ִϸ��̼ǿ� ���� ���� ���� �� ���ߺξ� ����
            transform.position = new Vector3(characterModel.position.x, transform.position.y, characterModel.position.z);
        }
    }
}
