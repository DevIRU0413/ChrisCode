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

    //[SerializeField] private AudioSource bgmAudioSource; // ������� AudioSource
    //[SerializeField] private AudioSource sfxAudioSource; // ȿ���� AudioSource

    private void Awake()
    {
        // TODO - ���۾��̳� �ΰ��ӿ��� �Ѵ� �����ؾ��ϴµ� Value���� �ٽ� �����ϰ� �ؾ���
        //���� ����Ŵ������� value���� ���;��Ѵ�(���� �ӽ� ������)
        bgmSlider.value = TMP_GameManager.bgmValue;
        sfxSlider.value = TMP_GameManager.sfxValue;
    }

    void Start()
    {
        //TODO -  ����Ŵ������� �Լ� ������

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
    }

    private void SfxbtnClick()
    {
        Debug.Log("SfxbtnClick");
        //Ŭ���� ���� Ȱ��ȭ or ���Ұ� ���� �Ǿ����� ���ӸŴ������� ����?
    }

    private void ClosebtnClick()
    {
        TMP_UIManager.Instance.CloseCurrentUI();
    }

    //BGM,SFX �����̴� ���� ������

    public void OnBgmSliderValueChanged(float value)
    {
        Debug.Log("Bgm�����̴� ���� ����Ǿ����ϴ�: " + value);
        TMP_GameManager.bgmValue = bgmSlider.value;
    }
    private void OnSfxSliderValueChanged(float value)
    {
        Debug.Log("Bgm�����̴� ���� ����Ǿ����ϴ�: " + value);
        TMP_GameManager.sfxValue = sfxSlider.value;
    }
}
