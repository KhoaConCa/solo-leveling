using Platform2D.CharacterInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// BossBaseStats - Được tạo ra để hiển thị và sử dụng chỉ số của BossBase.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025
    /// </summary>
    public class BossBaseStats : MonoBehaviour
    {
        #region --- Properties ---

        public BossBaseStatsSO BaseStats => _baseStats;
        public float CurrentMovementSpeed { get { return _currentMovementSpeed; } set { _currentMovementSpeed = value; } }
        public float CurrentHealthPoint { get { return _currentHealthPoint; } set { _currentHealthPoint = value; } }
        public float CurrentDefencePoint { get { return _currentDefencePoint; } set { _currentDefencePoint = value; } }
        public float CurrentAttackDamage { get { return _currentAttackDamage; } set { _currentAttackDamage = value; } }
        public float CurrentChainAttackDuration { get { return _currentChainAttackDuration; } set { _currentChainAttackDuration = value; } }
        public float CurrentWeaknessMultiplier { get { return _currentWeaknessMultiplier; } set { _currentWeaknessMultiplier = value; } }


        public void SetStats()
        {
            CurrentHealthPoint = _baseStats.healthPoint;
            CurrentDefencePoint = _baseStats.defencePoint;
            CurrentMovementSpeed = _baseStats.movementSpeed;
            CurrentAttackDamage = _baseStats.attackDamage;
            CurrentChainAttackDuration = _baseStats.chainAttackDuration;
            CurrentWeaknessMultiplier = _baseStats.weaknessMultiplier;
        }

        public void RampageStats()
        {
            CurrentMovementSpeed *= 1.6f;
            CurrentAttackDamage *= 1.6f;
            CurrentChainAttackDuration *= 1.6f;
            CurrentDefencePoint *= 1.6f;
            CurrentWeaknessMultiplier *= 1.4f;
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private BossBaseStatsSO _baseStats;

        [SerializeField] private float _currentHealthPoint;
        [SerializeField] private float _currentEnergyPoint;
        [SerializeField] private float _currentDefencePoint;

        [SerializeField] private float _currentAttackDamage;
        [SerializeField] private float _currentChainAttackDuration;
        [SerializeField] private float _currentWeaknessMultiplier;

        [SerializeField] private float _currentMovementSpeed;

        #endregion
    }
}
