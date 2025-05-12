using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// EnemyStats - Được tạo ra để hiển thị và sử dụng chỉ số của Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025
    /// </summary>
    public class EnemyStats : MonoBehaviour
    {
        #region --- Properties ---

        public EnemyStatsSO BaseStats => _baseStats;
        public float CurrentMovementSpeed { get { return _currentMovementSpeed; } set { _currentMovementSpeed = _baseStats.movementSpeed + value; } }
        public float CurrentHealthPoint { get { return _currentHealthPoint; } set { _currentHealthPoint = value; } }
        public float CurrentEnergyPoint { get { return _currentEnergyPoint; } set { _currentEnergyPoint = value; } }
        public float CurrentDefencePoint { get { return _currentDefencePoint; } set { _currentDefencePoint = value; } }

        #endregion

        #region --- Fields ---

        [SerializeField] private EnemyStatsSO _baseStats;

        [SerializeField] private float _currentHealthPoint;
        [SerializeField] private float _currentEnergyPoint;
        [SerializeField] private float _currentDefencePoint;

        [SerializeField] private float _currentMovementSpeed;

        #endregion
    }
}
