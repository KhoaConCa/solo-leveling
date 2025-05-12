using Platform2D.CharacterAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterStates
{
    /// <summary>
    /// PlayerStatesAlter - Được tạo ra để lưu trữ trạng thái của nhân vật.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlayerStatesAlter : MonoBehaviour
    {

        #region --- Properties ---

        public Animator PlayerAnimator => _animator;

        #region -- Movement States --
        public Vector2 Direction => this.transform.localScale;
        public Vector2 OnMove {
            get {
                return _onMove;
            }
            set {
                _onMove = value;
            }
        }
        public bool IsMoving {
            get {
                return _isMoving;
            }
            set {
                _isMoving = value;
                _animator.SetBool(AnimationStrings.IsMoving, value);
            }
        }
        #endregion

        #region -- Jumping States --
        public bool IsJumping
        {
            get
            {
                return _isJumping;
            }
            set
            {
                _isJumping = value;
                if (value)
                    _animator.SetTrigger(AnimationStrings.JumpTrigger);
            }
        }
        public bool IsDoubleJump { get; set; }

        public bool CanJump { get; set; }
        public bool IsFalling
        {
            get
            {
                return _isFalling;
            }
            set
            {
                _isFalling = value;
                _animator.SetBool(AnimationStrings.IsFalling, value);
            }
        }
        #endregion

        #region -- Crouch States --
        public bool UnholdCrouch { get; set; }
        public bool IsCrouch
        {
            get {
                return _isCrouch;
            }
            set {
                _isCrouch = value;
                _animator.SetBool(AnimationStrings.IsCrouching, value);
            }
        }
        public bool IsPenetrable { get; set; } = false;
        #endregion

        #region --- Dashing States --
        public bool CanDashing { get; set; } = true;
        public bool IsDashing
        {
            get {
                return _isDashing;
            }
            set {
                _isDashing = value;
                if (_isDashing)
                    _animator.SetTrigger(AnimationStrings.DashTrigger);
            }
        }
        #endregion

        #region -- Checking States --
        public bool AllowedSwitch { get; set; } = true;
        public bool OnGround
        {
            get
            {
                return _onGround;
            }
            set
            {
                _onGround = value;
                _animator.SetBool(AnimationStrings.IsGrounded, value);
            }
        }
        public bool IsTouchOneWay { get; set; } = false;

        public bool IsWall { get; set; }
        public bool IsCeiling {
            get {
                return _isCeiling;
            }
            set {
                _isCeiling = value;
                _animator.SetBool(AnimationStrings.IsCeiling, value);
            }
        }

        #endregion

        #endregion

        #region --- Fields ---

        [SerializeField] private Animator _animator;

        [SerializeField] private Vector2 _onMove = Vector2.zero;

        [SerializeField] private bool _isMoving = false;
        [SerializeField] private bool _isJumping = false;
        [SerializeField] private bool _isFalling = false;
        [SerializeField] private bool _isCrouch = false;
        [SerializeField] private bool _isDashing = false;

        [SerializeField] private bool _onGround = false;
        [SerializeField] private bool _isCeiling = false;

        #endregion

    }
}
