using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private Slider expSlider;
    [SerializeField] private Text levelText;
    [SerializeField] private Text killText;
    [SerializeField] private Text timeText;

    // TODO: ���߿� GameManager���� �޾ƿ����� ����
    float curExp = 3;
    float maxExp = 10;
    int level = 999;
    int kill = 100;

    private float maxTime = 150;  //
    private float currentTime = 160;
    public float remainTime;


    //TODO - �÷��̾��� ü��
    private void LateUpdate()
    {
        UpdateExp();
        UpdateLevel();
        UpdateKill();
        UpdateTime();
    }

    private void UpdateExp()
    {
        expSlider.value = curExp / maxExp;
    }

    private void UpdateLevel()
    {
        levelText.text = string.Format("Lv.{0:F0}", level);   
    }

    private void UpdateKill()
    {
        killText.text = string.Format("{0:F0}", kill);
    }

    private void UpdateTime()
    {
        remainTime = maxTime - currentTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);
        if (min >= 0 && sec >= 0)
        {
            string.Format("{0:D2}:{1:D2}", min, sec);
        }
        else
        {
            timeText.text = "00:00"; //���̻� �ð��� �帣�� �ʰ�
        }
    }
}
