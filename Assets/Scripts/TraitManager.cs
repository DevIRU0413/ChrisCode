using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{
    private CharacterMove characterMove; // 김채윤님 제작 능력치 스크립트 차후 반영 
    private PlayerHp playerHP; // 김채윤님 제작 PlayerHP.cs 추후 반영
    private PlayerAttack playerAttack;  //*차후 수정필요

    // 특성 효과 저장을 위한 딕셔너리
    private Dictionary<TraitType, System.Action<float>> traitEffects;
    // 쿨타임 체크용 딕셔너리
    private Dictionary<TraitType, float> lastActivatedTime = new Dictionary<TraitType, float>();
    // 현재 플레이어가 보유한 특성 목록
    public Dictionary<TraitType, Trait> acquiredTraits = new Dictionary<TraitType, Trait>();

    private const int MaxUpgradeCount = 3;

    private void Start()
    {
        characterMove = FindObjectOfType<CharacterMove>(); // 플레이어 이동속도 스크립트 찾기
        playerHP = FindObjectOfType<PlayerHp>(); // 플레이어 HP 스크립트 찾기
        playerAttack = FindAnyObjectByType<PlayerAttack>(); //* 차후 수정필요 / 플레이어 공격 스크립트 찾기

        SetupTraitEffects(); // 특성 효과 딕셔너리 초기화
    }

    private void SetupTraitEffects()
    {
        traitEffects = new Dictionary<TraitType, System.Action<float>>
        {
            // 이동속도 증가
            {
                TraitType.MoveSpeed, (value) =>
                {
                    if (characterMove != null && CanUpgrade(TraitType.MoveSpeed, value))
                    {
                        characterMove.moveSpeed *= value;
                        Debug.Log("[이동속도] 증가: x" + value);
                    }
                }
            },

            // 최대 체력 증가
            {
                TraitType.MaxHealth, (value) =>
                {
                    if (playerHP != null && CanUpgrade(TraitType.MaxHealth, value))
                    {
                        playerHP.MaxHealth = Mathf.RoundToInt(playerHP.MaxHealth * value);
                        Debug.Log("[최대체력] 증가: x" + value);
                    }
                }
            },

            // 공격력 증가
            {
                TraitType.AttackPower, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.AttackPower, value))
                    {
                        playerAttack.attackPower *= Mathf.RoundToInt(value);
                        Debug.Log("[공격력] 증가: x" + value);
                    }
                }
            },

            // 공격속도 증가
            {
                TraitType.AttackSpeed, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.AttackSpeed, value))
                    {
                        playerAttack.attackSpeed *= Mathf.RoundToInt(value);
                        Debug.Log("[공격속도] 증가: x" + value);
                    }
                }
            },

            // 추가 발사체
            {
                TraitType.ExtraProjectile, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.ExtraProjectile, value))
                    {
                        playerAttack.extraProjectileCount += Mathf.RoundToInt(value); // 추후 구현 필요
                        Debug.Log("[추가발사체] +" + value);
                    }
                }
            },

            // 발사체 크기 증가
            {
                TraitType.ProjectileSize, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.ProjectileSize, value))
                    {
                        playerAttack.projectileSizeMultiplier *= value; // 추후 구현 필요
                        Debug.Log("[발사체크기] 증가: x" + value);
                    }
                }
            },

            // 관통력 증가
            {
                TraitType.Pierce, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.Pierce, value))
                    {
                        playerAttack.pierceCount += Mathf.RoundToInt(value); // 추후 구현 필요
                        Debug.Log("[관통력] +" + value);
                    }
                }
            },

            // 폭발 특성
            {
                TraitType.Explosion, (value) =>
                {
                    if (playerAttack != null && CanUpgrade(TraitType.Explosion, value))
                    {
                        playerAttack.explosionRadius += value; // 추후 구현 필요
                        Debug.Log("[폭발반경] 증가: +" + value);
                    }
                }
            }
        };
    }

    // 업그레이드 가능한지 확인 (최대 3회)
    private bool CanUpgrade(TraitType type, float addValue)
    {
        if (acquiredTraits.TryGetValue(type, out var trait))
        {
            int currentLevel = Mathf.RoundToInt(trait.value);
            int addLevel = Mathf.RoundToInt(addValue);
            if (currentLevel >= MaxUpgradeCount)
            {
                Debug.Log("[업그레이드 제한] " + type + " 이미 최대 강화됨");
                return false;
            }
            return true;
        }
        return true;
    }

    public void ApplyTrait(Trait trait)
    {
        if (!trait.allowMultiple && acquiredTraits.ContainsKey(trait.type))
        {
            Debug.Log($"{trait.traitName} 중복 선택 불가");
            return;
        }

        if (traitEffects.TryGetValue(trait.type, out var effect))
        {
            effect.Invoke(trait.value);
            Debug.Log($"특성 적용! {trait.traitName} 적용");
        }
        else
        {
            Debug.LogWarning($"특성이 미등록상태 {trait.traitName}은 traitEffects에 등록되지 않음");
        }

        // 기존 강화 여부를 확인하고 딕셔너리로 처리한다.
        if (acquiredTraits.ContainsKey(trait.type))
        {
            var prev = acquiredTraits[trait.type];
            float newValue = prev.value * trait.value;
            acquiredTraits[trait.type].value = newValue;

            Debug.Log($"강화기록 {trait.traitName} 이미 보유, 추가 강화 적용");
        }
        else
        {
            acquiredTraits[trait.type] = trait;
            Debug.Log($"신규획득 {trait.traitName} 첫 획득");
        }

        
    }

    public bool HasTrait(TraitType type, out float value)
    {
        if (acquiredTraits.TryGetValue(type, out var trait))
        {
            value = trait.value;
            return true;
        }
        value = 0f;
        return false;
    }

    public bool TryActivateInvincibility(float currentHP, float maxHP, float cooldown, out float duration)
    {
        duration = 2f; // 기본 무적 시간 (초)

        // 특성을 보유중인지 확인할 필요있음
        if (acquiredTraits.TryGetValue(TraitType.InvincibleOnLowHP, out Trait trait))
        {
            float threshold = maxHP * trait.value; // 특성 발동의 기준 체력설정

            if (currentHP <= threshold)
            {
                // 마지막 특성 실행 시점 가져오기, 만약 마지막 발동 시점이 확인이 되지않을경우 아주 오래전으로 간주
                float lastTime = lastActivatedTime.ContainsKey(TraitType.InvincibleOnLowHP)
                    ? lastActivatedTime[TraitType.InvincibleOnLowHP]
                    : -999f;

                // 쿨타임이 끝났는지에 대한 확인 여부
                if (Time.time >= lastTime + cooldown)
                {
                    lastActivatedTime[TraitType.InvincibleOnLowHP] = Time.time;
                    Debug.Log($"무적발동! 지속시간 {duration}");
                    return true;
                }
                else
                {
                    float remainingTime = Mathf.Max(0f, lastTime + cooldown - Time.time);
                    Debug.Log($"무적 쿨타임 대기중 남은 시간 : {remainingTime:F1}초");
                }    
            }
        }

        // 특성 보유 x
        return false;
    }

    //public void HealOnKill()
    //{
    //    if (acquiredTraits.TryGetValue(TraitType.HealOnKill, out Trait trait))
    //    {
    //        // 특성 value 값만큼 회복량 계산
    //        float rawhealAmount = trait.value;
    //        int healAmount = Mathf.RoundToInt(rawhealAmount); // 소수점 반올림
    //
    //        if (playerHP != null)
    //        {
    //            playerHP.Heal(healAmount); //*** PlayerHP에 돌료가 만든 함수 구현 필요
    //            Debug.Log($"몬스터 처치 시 체력  {healAmount} 회복!");
    //        }
    //        else
    //        {
    //            Debug.LogWarning("PlayerHP 컴포넌트가 없습니다. 회복실패");
    //        }
    //    }
    //}


}
