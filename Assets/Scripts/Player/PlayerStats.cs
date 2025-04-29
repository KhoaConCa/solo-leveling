using Platform2D.BaseStats;
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

        public float MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }
        public float JumpSpeed { get { return _jumpSpeed; } set { _jumpSpeed = value; } }

        #endregion

        #region --- Fields ---

        [SerializeField] private float _movementSpeed = (float)BASE_STATS.MOVEMENT_SPEED;
        [SerializeField] private float _jumpSpeed = (float)BASE_STATS.JUMP_SPEED;

        #endregion

    }
}
