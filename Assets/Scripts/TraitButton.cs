using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitButton : MonoBehaviour
{
    public TextMeshProUGUI traitName;
    public TextMeshProUGUI traitDesc;

    private Trait traitData; // 해당 버튼이 담당하는 특성 데이터
    private System.Action<Trait> onClickCallback; // 클릭했을 때 실행할 함수

    public void Setup(Trait trait, System.Action<Trait> callback)
    {
        traitData = trait;
        onClickCallback = callback;

        if (traitName != null)
        {
            traitName.text = trait.traitName;
        }

        if (traitDesc != null)
        {
            traitDesc.text = trait.description;
        }

        GetComponent<Button>().onClick.RemoveAllListeners(); // 기존 연결 삭제
        GetComponent<Button>().onClick.AddListener(OnClick); // 현재 traitData 기준으로 새로연결
    }

    public void OnClick()
    {
        if (onClickCallback != null)
        {
            onClickCallback.Invoke(traitData); // 선택한 특성을 TraitUIManager로 넘김
        }
    }
}
