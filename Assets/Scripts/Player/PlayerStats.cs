using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// PlayerStats - Được tạo ra để lưu trữ chỉ số của nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlayerStats : MonoBehaviour
    {

        #region --- Properties ---

        public PlayerStatsSO BaseStats => _playerStatsSO;
        public float CurrentMovementSpeed => _playerStatsSO.movementSpeed;
        public float CrouchSpeed => CurrentMovementSpeed * _playerStatsSO.crouchMultiplier;
        public float DashSpeed => CurrentMovementSpeed * _playerStatsSO.dashMultiplier;

        public float CurrentHealthPoint { get { return _currentHealthPoint; } set { _currentHealthPoint = value; } }
        public float CurrentEnergyPoint { get { return _currentEnergyPoint; } set { _currentEnergyPoint = value; } }
        public float CurrentDefencePoint { get { return _currentDefencePoint; } set { _currentDefencePoint = value; } }

        public float CurrentDamage
        {
            get {
                return _currentDamage;
            }
            set {
                _currentDamage = value;
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerStatsSO _playerStatsSO;

        [SerializeField] private float _currentDamage;

        [SerializeField] private float _currentHealthPoint;
        [SerializeField] private float _currentEnergyPoint;
        [SerializeField] private float _currentDefencePoint;

        #endregion

    }

}
