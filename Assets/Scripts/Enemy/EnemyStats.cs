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

        #endregion

        #region --- Fields ---

        [SerializeField] private EnemyStatsSO _baseStats;

        [SerializeField] private float _currentMovementSpeed;

        #endregion
    }
}
