using System.Collections;
using System.Collections.Generic;
using Scripts.Manager;
using UnityEngine;

public class MonsterAudio : MonoBehaviour
{
    [SerializeField] private AudioClip m_shotSound;
    [SerializeField] private AudioClip m_hurtSound;
    [SerializeField] private AudioClip m_slashSound;
    [SerializeField] private AudioClip m_deadSound;
    public void ShootingSound()
    {
        AudioManager.Instance.PlaySFX(m_shotSound);
    }
    public void HurtSound()
    {
        AudioManager.Instance.PlaySFX(m_hurtSound);
    }
    public void SlashSound()
    {
        AudioManager.Instance.PlaySFX(m_slashSound);
    }
    public void DeadSound()
    {
        AudioManager.Instance.PlaySFX(m_deadSound);
    }
}
