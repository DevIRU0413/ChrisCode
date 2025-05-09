using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectionUI : MonoBehaviour
{
    [Header("SKill UI Settings")]
    [SerializeField] private Image[] icon;
    [SerializeField] private TextMeshProUGUI[] name;
    [SerializeField] private TextMeshProUGUI[] description;

    private PMS_Skill firstSkill;

    public Button[] playerSkillSelectButton;
    // TODO - 플레이어 스킬을 어떻게 들고 올 것인지


    private void Awake()
    {
        //스킬 3가지 랜덤 제공만 해놓으면 해당 데이터를 Awake()에서 받아서

        //임시 스킬 생성
        firstSkill = new PMS_Skill("FireBall", "A powerful fireball comes out ahead..");
        //현재 아이콘 x
        //icon = firstSkill.icon;

        name[0].text = firstSkill.skillName;
        description[0].text = firstSkill.skillDescription;
        
    }

    private void Start()
    {
        //각 버튼 이벤트 활성화해주고
        for(int i = 0; i < 1; i++)
        {
            playerSkillSelectButton[i].onClick.AddListener(OnSkillSelectSlot);     
        }
    }

    private void OnSkillSelectSlot()
    {
        //누르면 해당 스킬을 선택한것을 캐릭터에게 반환
    }
}
