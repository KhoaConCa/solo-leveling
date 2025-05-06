using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStates
{
    /// <summary>
    /// EnemyStates - Được dùng để lưu trạng thái của Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    public class EnemyStates : MonoBehaviour
    {
        #region --- Properties ---

        public float Direction {  get; set; }

        public float AnchorPosX { get; set; }

        public bool IsMoving { get; set; } = false;

        public bool OnGround { get; set; } = false;
        public bool OnWall { get; set; } = false;

        #endregion
    }
}
