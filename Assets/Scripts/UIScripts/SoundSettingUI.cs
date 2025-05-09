using Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundSettingUI : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private Button bgmButton;
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    //bgm,sfx 버튼 클릭시 뮤트 및 이미지 변경처리

    /*[SerializeField] private Image bgmButtonImage; // Bgm 버튼의 Image 컴포넌트
    [SerializeField] private Sprite bgmUnmuteSprite; // Bgm 음소거 해제 이미지
    [SerializeField] private Sprite bgmMuteSprite;   // Bgm 음소거 이미지

    [SerializeField] private Image sfxButtonImage; // Bgm 버튼의 Image 컴포넌트
    [SerializeField] private Sprite sfxUnmuteSprite; // Bgm 음소거 해제 이미지
    [SerializeField] private Sprite sfxMuteSprite;   // Bgm 음소거 이미지
    */

    //[SerializeField] private AudioSource bgmAudioSource; // 배경음악 AudioSource
    //[SerializeField] private AudioSource sfxAudioSource; // 효과음 AudioSource

    //TODO - 나중에 주석 다 제거 하고 디버깅용 코드 다 제거 하고 AudioManager에서 들고오는 코드 주석해체 처리
    private void Awake()
    {
        //먼저 사운드매니저에게 value값을 들고와야한다(현재 임시 테스트용)
        bgmSlider.value = TMP_GameManager.bgmValue;
        sfxSlider.value = TMP_GameManager.sfxValue;

        //추후 사용
        //SoundVolumeUpdate()
    }

    void Start()
    {
        bgmButton.onClick.AddListener(() => BgmbtnClick());
        sfxButton.onClick.AddListener(() => SfxbtnClick());
        closeButton.onClick.AddListener(() => ClosebtnClick());

        // TODO - 슬라이더 값 변경 시 호출될 함수 연결(사운드매니저에서 함수들고오기)
        bgmSlider.onValueChanged.AddListener(OnBgmSliderValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxSliderValueChanged);
    }

    //Button 클릭 디버깅용
    private void BgmbtnClick()
    {
        Debug.Log("BgmbtnClick");
        //클릭을 통한 활성화 or 음소거 설정 되었는지 게임매니저에게 전달?
        //AudioManager.Instance.MuteBgm();
    }

    private void SfxbtnClick()
    {
        Debug.Log("SfxbtnClick");
        //클릭을 통한 활성화 or 음소거 설정 되었는지 게임매니저에게 전달?
        //AudioManager.Instance.MuteSfx();
    }

    private void ClosebtnClick()
    {
        TMP_UIManager.Instance.CloseCurrentUI();
    }

    //BGM,SFX 슬라이더 조절 디버깅용

    public void OnBgmSliderValueChanged(float value)
    {
        Debug.Log("Bgm슬라이더 값이 변경되었습니다: " + value);

        //추후 주석해제하여 사용처리
        TMP_GameManager.bgmValue = bgmSlider.value;       
        //AudioManager.Instance.SetBgmVolume(value);
    }
    private void OnSfxSliderValueChanged(float value)
    {
        Debug.Log("Bgm슬라이더 값이 변경되었습니다: " + value);

        //추후 주석해제하여 사용처리
        TMP_GameManager.sfxValue = sfxSlider.value;       
        //AudioManager.Instance.SetSfxVolume(value);
    }

    private void SoundVolumeUpdate()
    {
        //bgmSlider.value = AudioManager.Instance.GetBgmVolume();
        //sfxSlider.value = AudioManager.Instance.GetBgmVolume();
    }
}
