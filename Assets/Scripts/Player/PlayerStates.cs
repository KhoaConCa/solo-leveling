using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStates
{
    /// <summary>
    /// PlayerStates - Được tạo ra để lưu trữ trạng thái của nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlayerStates : MonoBehaviour
    {

        #region --- Properties ---

        public float IsMoving { get { return _isMoving; } set { _isMoving = value; } }
        public float IsJumping { get { return _isJumping; } set { _isJumping = value; } }
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool IsOnWall { get { return _isOnWall; } set { _isOnWall = value; } }

        #endregion

        #region --- Fields ---

        [SerializeField] private float _isMoving;
        [SerializeField] private float _isJumping;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isOnWall;

        #endregion

    }
}
