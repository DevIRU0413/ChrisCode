using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp , Level, Kill , Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    //�ӽ� ������ ���߿� ���ӸŴ������� �ش簪���� ���;���
    float curExp = 3;  
    float maxExp = 10;
    int level = 999;
    int kill = 100;

    private float maxTime = 150;  //
    private float currentTime = 160;
    public float remainTime;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = 3;   //TODO - ���� ����ġ ��������
                float maxExp = 10;  //TODO - ���� ���� ����ġ ��������
                mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", level);    //TODO - ���� ���� �ҷ�����
                break;

            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", kill);    //TODO - ���� ���� �ҷ�����
                break;

            case InfoType.Time:
                remainTime = maxTime - currentTime;             //TODO -  �� ���� ���߿�
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                Debug.Log(remainTime);
                if (min >= 0 && sec >= 0)
                    myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                else
                {
                    myText.text = "00:00"; //���̻� �ð��� �帣�� �ʰ�
                }
                break;

            case InfoType.Health:
                break;
        }
    }
}
