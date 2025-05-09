using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackTracker : MonoBehaviour
{
    public float scanRange;  //  탐색범위
    public LayerMask targetLayer;   // 몬스터 레이어 탐색
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
        Debug.Log("확인용");
        float diff = 100; // 비교 거리

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
