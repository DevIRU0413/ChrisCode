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
        AudioManager.Instance.PlaySfx(m_shotSound);
    }
    public void HurtSound()
    {
        AudioManager.Instance.PlaySfx(m_hurtSound);
    }
    public void SlashSound()
    {
        AudioManager.Instance.PlaySfx(m_slashSound);
    }
    public void DeadSound()
    {
        AudioManager.Instance.PlaySfx(m_deadSound);
    }
}
