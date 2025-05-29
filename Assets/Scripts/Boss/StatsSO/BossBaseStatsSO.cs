using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// BossBaseStatsSO - Được tạo ra để lưu dữ liệu của các Boss.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/05/2025
    /// </summary>
    [CreateAssetMenu(fileName = "StatsSO", menuName = "Character/BossBaseStats", order = 1)]
    public class BossBaseStatsSO : ScriptableObject
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

        [Header("Attack Setting")]
        public float attackDamage;
        public float weaknessMultiplier;

        [Header("Range Setting")]
        public float detectedRange;
        public float attackRange;

        [Header("Duration Setting")]
        public float appearanceDuration;
        public float attackDuration;
        public float chainAttackDuration;

        [Header("Data Setting")]
        public string name;
        public Sprite sprite;
        public RuntimeAnimatorController animator;

        #endregion
    }
}
