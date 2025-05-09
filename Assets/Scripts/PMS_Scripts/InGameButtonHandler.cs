using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameButtonHandler : MonoBehaviour
{
    public Button[] buttons; // 여러 버튼을 배열로 관리

    void Start()
    {
        // 모든 버튼에 대해 이벤트를 동적으로 연결
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button.name)); // 버튼 이름을 기반으로 동작 결정
        }
    }

    private void OnButtonClicked(string btnname)
    {
        switch (btnname)
        {
            case "OptionButton":
                Debug.Log("옵션창");
                break;
            default:
                Debug.Log("알 수 없는 버튼: " + btnname);
                break;
        }
    }
}
