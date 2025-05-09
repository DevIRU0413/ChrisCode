using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour
{
    public Transform target;  // 따라갈 대상 (플레이어)
    public Vector3 offset = new Vector3(0, 0.5f, -0.5f);  // 탑뷰용 오프셋
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 = 플레이어 위치 + 오프셋
        Vector3 targetPosition = target.position + offset;

        // 부드럽게 따라감 (Lerp로 보간)
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 카메라는 항상 아래를 향하게 (탑뷰)
        transform.LookAt(target.position);
    }
}
