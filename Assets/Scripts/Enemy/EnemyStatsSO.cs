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

        public float KnockBackForce => weight * knockBackMultiplier;
        public float DeadKnockBackForce => weight * DeadknockBackMultiplier;

        #endregion

        #region --- Fields ---

        [Header("Base Setting")]
        public float healthPoint;
        public float energyPoint;
        public float defencePoint;

        [Header("Detail Setting")]
        public float weight;
        public float knockBackMultiplier;
        public float DeadknockBackMultiplier;

        [Header("Movement Setting")]
        public float movementSpeed;

        [Header("Range Setting")]
        public float movementRange;
        public float maxMovementRange;

        [Header("Duration Setting")]
        public float idleDuration;
        public float detectDuration;

        [Header("Data Setting")]
        public Sprite sprite;
        public RuntimeAnimatorController animator;

        #endregion
    }
}
