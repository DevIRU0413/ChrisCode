using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Manager;

public class HUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI timeText;

    //hp ����
    private PlayerHp hp;

    private PlayerExperience playerExp;


    // TODO: ���߿� GameManager���� �޾ƿ����� ����
    float curExp = 3;
    float maxExp = 10;
    int level = 999;
    int kill = 100;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            hp = player.GetComponent<PlayerHp>();
            playerExp = player.GetComponent<PlayerExperience>();
        }
    }

    private float currenTime;

    //TODO - �÷��̾��� ü��
    private void LateUpdate()
    {
        UpdateExp();
        UpdateLevel();
        UpdateHp();
        UpdateHpText();
        UpdateKill();
        UpdateTime();
    }

    //ü��
    private void UpdateHpText()
    {
        hpText.text = string.Format("{0}/{1}", hp.GetCurrentHealth(), hp.GetMaxHealth());
    }

    private void UpdateHp()
    {
        hpSlider.value = hp.GetCurrentHealth();
    }

    private void UpdateExp()
    {
        expSlider.value = playerExp.CurrentExp;
    }

    private void UpdateLevel()
    {
        levelText.text = string.Format("Lv.{0:F0}", playerExp.Level);
    }

    private void UpdateKill()
    {
        killText.text = string.Format("{0:F0}", kill);
    }

    private void UpdateTime()
    {
        currenTime = InGameManager.Instance.GetInGameCurrenttTime();
        int min = Mathf.FloorToInt(currenTime / 60);
        int sec = Mathf.FloorToInt(currenTime % 60);

        if (min >= 0 && sec >= 0)
        {
            timeText.text = string.Format("{0:D2}:{1:D2}", min, sec);
        }
    }
}
