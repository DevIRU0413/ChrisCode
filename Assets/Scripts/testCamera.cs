using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour
{
    public Transform target;  // ���� ��� (�÷��̾�)
    public Vector3 offset = new Vector3(0, 0.5f, -0.5f);  // ž��� ������
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // ��ǥ ��ġ = �÷��̾� ��ġ + ������
        Vector3 targetPosition = target.position + offset;

        // �ε巴�� ���� (Lerp�� ����)
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // ī�޶�� �׻� �Ʒ��� ���ϰ� (ž��)
        transform.LookAt(target.position);
    }
}
