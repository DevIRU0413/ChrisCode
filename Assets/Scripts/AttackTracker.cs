using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackTracker : MonoBehaviour
{
    public float scanRange;  //  Ž������
    public LayerMask targetLayer;   // ���� ���̾� Ž��
    public RaycastHit[] targets;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics.SphereCastAll(transform.position,scanRange, Vector3.up, Mathf.Infinity ,targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        Debug.Log(targets.Length);
        Debug.Log("Ȯ�ο�");
        float diff = 100; // �� �Ÿ�

        foreach (RaycastHit target in targets)
        {
            Debug.Log(target.collider.name);
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }

}
