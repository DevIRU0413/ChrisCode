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

    //bgm,sfx ��ư Ŭ���� ��Ʈ �� �̹��� ����ó��

    /*[SerializeField] private Image bgmButtonImage; // Bgm ��ư�� Image ������Ʈ
    [SerializeField] private Sprite bgmUnmuteSprite; // Bgm ���Ұ� ���� �̹���
    [SerializeField] private Sprite bgmMuteSprite;   // Bgm ���Ұ� �̹���

    [SerializeField] private Image sfxButtonImage; // Bgm ��ư�� Image ������Ʈ
    [SerializeField] private Sprite sfxUnmuteSprite; // Bgm ���Ұ� ���� �̹���
    [SerializeField] private Sprite sfxMuteSprite;   // Bgm ���Ұ� �̹���
    */

    //[SerializeField] private AudioSource bgmAudioSource; // ������� AudioSource
    //[SerializeField] private AudioSource sfxAudioSource; // ȿ���� AudioSource

    //TODO - ���߿� �ּ� �� ���� �ϰ� ������ �ڵ� �� ���� �ϰ� AudioManager���� ������ �ڵ� �ּ���ü ó��
    private void Awake()
    {
        //���� ����Ŵ������� value���� ���;��Ѵ�(���� �ӽ� �׽�Ʈ��)
        bgmSlider.value = TMP_GameManager.bgmValue;
        sfxSlider.value = TMP_GameManager.sfxValue;

        //���� ���
        //SoundVolumeUpdate()
    }

    void Start()
    {
        bgmButton.onClick.AddListener(() => BgmbtnClick());
        sfxButton.onClick.AddListener(() => SfxbtnClick());
        closeButton.onClick.AddListener(() => ClosebtnClick());

        // TODO - �����̴� �� ���� �� ȣ��� �Լ� ����(����Ŵ������� �Լ�������)
        bgmSlider.onValueChanged.AddListener(OnBgmSliderValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxSliderValueChanged);
    }

    //Button Ŭ�� ������
    private void BgmbtnClick()
    {
        Debug.Log("BgmbtnClick");
        //Ŭ���� ���� Ȱ��ȭ or ���Ұ� ���� �Ǿ����� ���ӸŴ������� ����?
        //AudioManager.Instance.MuteBgm();
    }

    private void SfxbtnClick()
    {
        Debug.Log("SfxbtnClick");
        //Ŭ���� ���� Ȱ��ȭ or ���Ұ� ���� �Ǿ����� ���ӸŴ������� ����?
        //AudioManager.Instance.MuteSfx();
    }

    private void ClosebtnClick()
    {
        TMP_UIManager.Instance.CloseCurrentUI();
    }

    //BGM,SFX �����̴� ���� ������

    public void OnBgmSliderValueChanged(float value)
    {
        Debug.Log("Bgm�����̴� ���� ����Ǿ����ϴ�: " + value);

        //���� �ּ������Ͽ� ���ó��
        TMP_GameManager.bgmValue = bgmSlider.value;       
        //AudioManager.Instance.SetBgmVolume(value);
    }
    private void OnSfxSliderValueChanged(float value)
    {
        Debug.Log("Bgm�����̴� ���� ����Ǿ����ϴ�: " + value);

        //���� �ּ������Ͽ� ���ó��
        TMP_GameManager.sfxValue = sfxSlider.value;       
        //AudioManager.Instance.SetSfxVolume(value);
    }

    private void SoundVolumeUpdate()
    {
        //bgmSlider.value = AudioManager.Instance.GetBgmVolume();
        //sfxSlider.value = AudioManager.Instance.GetBgmVolume();
    }
}
