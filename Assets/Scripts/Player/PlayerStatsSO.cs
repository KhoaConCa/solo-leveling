using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStats
{
    /// <summary>
    /// PlayerStatsSO - Được tạo ra để lưu trữ chỉ số của nhân vật.
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
        public float movementSpeed = 5f;
        public float crouchMultiplier = 1.2f;
        public float dashMultiplier = 3.5f;

        [Header("Jump Settings")]
        public float jumpSpeed = 12f;
        public float doubleJumpMultiplier = 0.7f;

        [Header("Duration Setting")]
        public float dashDuration = 0.2f;
        public float oneWayDuration = 1f;

        #endregion

    }

}
