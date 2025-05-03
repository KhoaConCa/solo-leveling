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

        #region -- Int Value --
        public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
        public int AttackCount { get { return _attackCount; } set { _attackCount = value; } }
        #endregion

        #region -- Float Value --
        public float Horizontal { get { return _horizontal; } set { _horizontal = value; } }
        public float Vertical { get { return _vertical; } set { _vertical = value; } }
        #endregion

        #region -- Bool Value Movement --
        public bool IsCrouching { get { return _isCrouching; } set { _isCrouching = value; } }
        public bool IsInvincible { get { return _isInvincible; } set { _isInvincible = value; } }
        public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
        public bool IsOneWay { get { return _isOneWay; } set { _isOneWay = value; } }
        public bool CanDownard { get { return _canDownard; } set { _canDownard = value; } }
        public bool IsOnWall { get { return _isOnWall; } set { _isOnWall = value; } }
        public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
        public bool IsDoubleJump { get { return _isDoubleJump; } set { _isDoubleJump = value; } }
        #endregion

        #region -- Bool Value Action --
        public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
        #endregion

        #endregion

        #region --- Fields ---

        #region -- Int Value --
        [SerializeField] private int _jumpCount = 0;
        [SerializeField] private int _attackCount = 0;
        #endregion

        #region -- Float Value --
        [SerializeField] private float _horizontal = 0f;
        [SerializeField] private float _vertical = 0f;
        #endregion

        #region -- Bool Value Movement --
        [SerializeField] private bool _isCrouching = false;
        [SerializeField] private bool _isInvincible = false;
        [SerializeField] private bool _isDashing = false;
        [SerializeField] private bool _isGrounded = false;
        [SerializeField] private bool _isOneWay = false;
        [SerializeField] private bool _canDownard = false;
        [SerializeField] private bool _isOnWall = false;
        [SerializeField] private bool _isJumping = false;
        [SerializeField] private bool _isDoubleJump = false;
        #endregion

        #region -- Bool Value Action --
        [SerializeField] private bool _isAttacking = false;
        #endregion

        #endregion

    }
}
