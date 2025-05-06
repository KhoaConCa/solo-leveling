using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// EnemyStatsSO - Được tạo ra để lưu dữ liệu của các Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025
    /// </summary>
    [CreateAssetMenu(fileName = "StatsSO", menuName = "Character/EnemyStats", order = 1)]
    public class EnemyStatsSO : ScriptableObject
    {
        #region --- Properties ---

        #endregion

        #region --- Fields ---

        [Header("Movement Setting")]
        public float movementSpeed;

        [Header("Range Setting")]
        public float movementRange;

        [Header("Duration Setting")]
        public float idleDuration;

        [Header("Data Setting")]
        public Sprite sprite;
        public RuntimeAnimatorController animator;

        #endregion
    }
}
