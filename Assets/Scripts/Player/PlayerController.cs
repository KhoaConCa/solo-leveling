using Newtonsoft.Json.Bson;
using Platform2D.CharacterAnimation;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Platform2D.Vector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// PlayerController - Đóng vai trò trung tâm nhằm quản lý, thực hiện và lưu trữ các thông tin và thao tác quan trọng.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/04/2025
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region --- Overrides ---

        /// <summary>
        /// Thực hiện điều phối sự di chuyển của nhân vật thông qua các Controller.
        /// </summary>
        private void OnMove()
        {
            FlipPlayerObject();

            _animationController.OnCrouch();
            _animationController.OnMove();

            if (_playerStates.IsCrouching)
            {
                _movementController.OnCrouch();
                return;
            }

            _movementController.OnMove();
        }

        /// <summary>
        /// Thực hiện điều phối sự nhảy của nhân vật thông qua các Controller
        /// </summary>
        private void OnJump()
        {
            if(_playerStates.JumpCount == 0 && _rg2D.velocity.y < 0)
                _playerStates.JumpCount++;

            if ((_playerStates.IsGrounded || _playerStates.JumpCount < _playerStates.MAXJUMP) && _playerStates.IsJumping > 0)
            {
                _movementController.OnJump();
                _playerStates.JumpCount++;
            }

            _animationController.OnJump();

            _playerStates.IsJumping = 0;

            if (_playerStates.IsGrounded)
                _playerStates.JumpCount = 0;
        }

        /// <summary>
        /// Thực hiện điều phối sự lướt của nhân vật thông qua các Controller.
        /// </summary>
        private IEnumerator OnDash()
        {
            _animationController.OnDash();
            _movementController.OnDash();

            yield return new WaitForSeconds(_playerStates.DashDuration);

            _playerStates.IsDashing = false;
            _animationController.OnDash();
        }

        #endregion

        #region --- Unity Methods ---

        public void Awake()
        {
            try
            {
                // Tìm kiếm và lấy các Component cần thiết.
                var animator = gameObject.GetComponentInChildren<Animator>();

                _rg2D = gameObject.GetComponent<Rigidbody2D>();
                _groundChecker = GameObject.FindGameObjectWithTag(GROUND_CHECKER).GetComponent<CapsuleCollider2D>();

                _playerStats = gameObject.GetComponentInChildren<PlayerStats>();
                _playerStates = gameObject.GetComponentInChildren<PlayerStates>();

                // Khởi tạo các giá trị mặc định
                _isFacingRight = true;
                _movementController = new PlayerMovementController(this);
               
                _animationController = new PlayerAnimationController(
                    playerController: this,
                    animator: animator
                ); 
            }
            catch (Exception ex)
            {
                Debug.LogError($"PlayerController Error: {ex}");
            }
        }

        public void FixedUpdate()
        {
            try
            {
                // Cập nhật vật lý nhân vật.
                _movementController.IsGrounded();
                _movementController.IsOnWall(this.gameObject.transform.localScale.x);

                _animationController.OnGrounded();

                // Thao tác vật lý nhân vật
                OnJump();

                if (_playerStates.IsDashing)
                    StartCoroutine(OnDash());
                else OnMove();
            }
            catch (Exception ex)
            {
                Debug.LogError($"PlayerController Update Failed: {ex}");
            }
            
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Thực hiện đổi chiều của nhân vật.
        /// </summary>
        public void FlipPlayerObject()
        {
            if (_playerStates == null) return;

            if (_playerStates.IsMoving > 0 && !IsFacingRight)
                IsFacingRight = true;
            else if (_playerStates.IsMoving < 0 && IsFacingRight)
                IsFacingRight = false;
        }

        #endregion

        #region --- Properties ---

        public Rigidbody2D Rg2D { get { return _rg2D; } }
        public CapsuleCollider2D GroundChecker { get { return _groundChecker; } }

        public PlayerStats PlayerStats { get { return _playerStats; } }
        public PlayerStates PlayerStates { get { return _playerStates; } }

        public bool IsFacingRight
        {
            get { return _isFacingRight; }
            private set
            {
                if(_isFacingRight != value)
                    this.gameObject.transform.localScale *= new Vector2((float)AXIS_1D.NEGATIVE, (float)AXIS_1D.POSITIVE);

                _isFacingRight = value;
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Rigidbody2D _rg2D;
        [SerializeField] private CapsuleCollider2D _groundChecker;

        private PlayerMovementController _movementController;
        private PlayerAnimationController _animationController;

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private PlayerStates _playerStates;

        [SerializeField] private bool _isFacingRight;

        [SerializeField] private const string GROUND_CHECKER = "GroundChecker";

        #endregion

    }
}