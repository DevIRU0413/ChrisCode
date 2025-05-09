using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool s_instance;

    private void Awake()
    {
        if(s_instance ==null)
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    //화살살 프리펩
    public GameObject arrowPrefab;

    //풀 사이즈
    public int poolSize =5;
    //풀은 큐로 구현
    private Queue<GameObject> arrowPool = new Queue<GameObject>();
    void Start()
    {
        //풀 사이즈 만큼 화살 게임오브젝트 미리 생성
        for(int i = 0; i< poolSize ; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab,transform);
            //비활성화 시키기
            arrow.SetActive(false);
            //큐에 삽입
            arrowPool.Enqueue(arrow);
        }
    }

    //리턴 메서드
    public GameObject GetBulletPool()
    {
        //풀이 0보다 클때
        if(arrowPool.Count >0)
        {
            //화살 오브젝트를 큐에서 꺼내기
            GameObject arrow = arrowPool.Dequeue();
            //오브젝트 활성화
            arrow.SetActive(true);
            //화살 오브젝트 리턴해주기
            return arrow;

        }
        //풀에 아무것도 없을 때
        else
        {
            //생성
            GameObject arrow = Instantiate(arrowPrefab,transform);
            arrow.SetActive(false);
            return arrow;
        }
    }

    //반납 메서드
    public void ReturnPool(GameObject arrow)
    {
        //비활성화
        arrow.SetActive(false);
        //풀에 넣기
        arrowPool.Enqueue(arrow);
    }
}
