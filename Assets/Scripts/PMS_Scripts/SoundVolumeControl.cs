using Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeConrtol : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private Slider BgmSlider;
    [SerializeField] private Slider SfxSlider;
    [SerializeField] private Button BgmButton;
    [SerializeField] private Button SfxButton;
    //[SerializeField] private AudioSource bgmAudioSource; // ������� AudioSource
    //[SerializeField] private AudioSource sfxAudioSource; // ȿ���� AudioSource

    void Start()
    {
        // TODO - ���۾��̳� �ΰ��ӿ��� �Ѵ� �����ؾ��ϴµ� Value���� �ٽ� �����ϰ� �ؾ���

        // TODO - �����̴� �� ���� �� ȣ��� �Լ� ����(����Ŵ������� �Լ�������)
        //BgmSlider.onValueChanged.AddListener(SetBgmVolume); 
        //SfxSlider.onValueChanged.AddListener(SetSfxVolume);

        //TODO -  ����Ŵ������� �Լ� ������
        BgmButton.onClick.AddListener(() => BgmbtnClick());
        SfxButton.onClick.AddListener(() => SfxbtnClick());
    }
    
    //������
    private void BgmbtnClick()
    {
        Debug.Log("BgmbtnClick");
    }

    private void SfxbtnClick()
    {
        Debug.Log("SfxbtnClick");
    }
}
