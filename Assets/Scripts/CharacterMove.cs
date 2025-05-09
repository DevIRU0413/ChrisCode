using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMove : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Transform characterModel; // Animator가 있는 자식 모델

    public float moveSpeed = 3f;
    public float rotateSpeed = 720f;

    private Vector3 inputDirection;
    private float previousHorizontal = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // 캐릭터 물리 회전 방지
        rb.freezeRotation = true;  

        if (characterModel != null)
        {
            // 외형 아바타에 있는 에니메이트 컴포넌트 호출
            animator = characterModel.GetComponent<Animator>();
        }
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 대각선 보정
        inputDirection = new Vector3(h, 0, v).normalized;

        if (animator != null)
        {
            // 애니메이션에서 뛰는 모션을 위한 Speed 조건 (위의 정규화와 함께 0~1로 설정 가능)
            float speedValue = inputDirection.magnitude;
            animator.SetFloat("Speed", speedValue);

            // 정지 상태에서 좌/우 입력이 들어오면 speed를 감지하고 회전 트리거 실행
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

        // 이동 방향에 따라 캐릭터 회전
        if (inputDirection.magnitude > 0.1f)
        {
            // 최상 계층 회전(플레이어 같이 회전)
            Quaternion targetRot = Quaternion.LookRotation(inputDirection);
            Vector3 targetEuler = targetRot.eulerAngles;

            // Y축 회전만 남기고 회전
            Quaternion yRotationOnly = Quaternion.Euler(0f, targetEuler.y, 0f);

            // 목표 방향으로의 부드러운 회전
            characterModel.rotation = Quaternion.RotateTowards(characterModel.rotation, yRotationOnly, rotateSpeed * Time.deltaTime);
        }
        // 이전 프레임의 수평값 저장용, 정지시 애니메이션 중복 방지
        previousHorizontal = h;
    }

    // 플레이어 이동 로직
    void FixedUpdate()
    {
        Vector3 moveOffset = inputDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveOffset);
    }

    // Player와 아바타 캐릭터 콜라이더 일치
    void LateUpdate()
    {
        Vector3 offset = characterModel.position - transform.position;

        // 이동량이 너무 크면 무시 (애니메이션 폭주 방지)
        if (offset.magnitude < 1f)
        {
            // Player를 외형 아바타에 xz를 맞추고 y는 애니메이션에 의한 지형 뚫음 및 공중부양 방지
            transform.position = new Vector3(characterModel.position.x, transform.position.y, characterModel.position.z);
        }
    }
}
