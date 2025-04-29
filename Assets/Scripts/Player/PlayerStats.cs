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

        public float movementSpeed { get; set; } = (float)BASE_STATS.MOVEMENT_SPEED;

        #endregion

    }
}
