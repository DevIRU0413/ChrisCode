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
    // TODO - �÷��̾� ��ų�� ��� ��� �� ������


    private void Awake()
    {
        //��ų 3���� ���� ������ �س����� �ش� �����͸� Awake()���� �޾Ƽ�

        //�ӽ� ��ų ����
        firstSkill = new PMS_Skill("FireBall", "A powerful fireball comes out ahead..");
        //���� ������ x
        //icon = firstSkill.icon;

        name[0].text = firstSkill.skillName;
        description[0].text = firstSkill.skillDescription;
        
    }

    private void Start()
    {
        //�� ��ư �̺�Ʈ Ȱ��ȭ���ְ�
        for(int i = 0; i < 1; i++)
        {
            playerSkillSelectButton[i].onClick.AddListener(OnSkillSelectSlot);     
        }
    }

    private void OnSkillSelectSlot()
    {
        //������ �ش� ��ų�� �����Ѱ��� ĳ���Ϳ��� ��ȯ
    }
}
