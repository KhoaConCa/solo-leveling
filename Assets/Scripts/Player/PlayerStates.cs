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

        public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
        public int MAXJUMP { get { return MAX_JUMP; } }
        public float IsMoving { get { return _isMoving; } set { _isMoving = value; } }
        public float IsJumping { get { return _isJumping; } set { _isJumping = value; } }
        public float DashDuration { get { return _dashDuration; } set { _dashDuration = value; } }
        public bool IsCrouching { get { return _isCrouching; } set { _isCrouching = value; } }
        public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool IsOnWall { get { return _isOnWall; } set { _isOnWall = value; } }

        #endregion

        #region --- Fields ---

        [SerializeField] private int _jumpCount = 0;
        [SerializeField] private const int MAX_JUMP = 2;

        [SerializeField] private float _isMoving;
        [SerializeField] private float _isJumping;
        [SerializeField] private float _dashDuration = 0.2f;

        [SerializeField] private bool _isCrouching;
        [SerializeField] private bool _isDashing = false;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isOnWall;

        #endregion

    }
}
