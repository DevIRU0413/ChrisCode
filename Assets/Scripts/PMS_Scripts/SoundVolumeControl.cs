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
    //[SerializeField] private AudioSource bgmAudioSource; // 배경음악 AudioSource
    //[SerializeField] private AudioSource sfxAudioSource; // 효과음 AudioSource

    void Start()
    {
        // TODO - 시작씬이나 인게임에서 둘다 조정해야하는데 Value값을 다시 설정하게 해야함

        // TODO - 슬라이더 값 변경 시 호출될 함수 연결(사운드매니저에서 함수들고오기)
        //BgmSlider.onValueChanged.AddListener(SetBgmVolume); 
        //SfxSlider.onValueChanged.AddListener(SetSfxVolume);

        //TODO -  사운드매니저에서 함수 들고오기
        BgmButton.onClick.AddListener(() => BgmbtnClick());
        SfxButton.onClick.AddListener(() => SfxbtnClick());
    }
    
    //디버깅용
    private void BgmbtnClick()
    {
        Debug.Log("BgmbtnClick");
    }

    private void SfxbtnClick()
    {
        Debug.Log("SfxbtnClick");
    }
}
