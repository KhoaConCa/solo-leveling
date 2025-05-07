using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// BaseStats - Được tạo ra để lưu trữ chỉ số của nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 02/05/2025
    /// </summary>
    using UnityEngine;

    [CreateAssetMenu(fileName = "StatsSO", menuName = "Character/PlayerStats", order = 0)]
    public class PlayerStatsSO : ScriptableObject
    {

        #region --- Properties ---

        public float DoubleJumpSpeed => jumpSpeed * doubleJumpMultiplier;

        #endregion

        #region --- Fields ---

        [Header("Movement Settings")]
        public float movementSpeed;
        public float crouchMultiplier;
        public float dashMultiplier;

        [Header("Jump Settings")]
        public float jumpSpeed;
        public float doubleJumpMultiplier;

        [Header("Duration Setting")]
        public float dashDuration;
        public float oneWayDuration;

        [Header("Cooldown Setting")]
        public float jumpCooldown;
        public float dashCoolDown;

        [Header("Data Setting")]
        public Sprite sprite;
        public RuntimeAnimatorController animator;

        #endregion

    }

}
