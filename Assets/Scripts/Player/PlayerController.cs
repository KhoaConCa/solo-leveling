using Newtonsoft.Json.Bson;
using Platform2D.CharacterAnimation;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStates;
using Platform2D.CharacterStats;
using Platform2D.Vector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        #region --- Unity Methods ---

        public void Awake()
        {
            try
            {
                // Tìm kiếm và lấy các Component cần thiết.
                var animator = gameObject.GetComponentInChildren<Animator>();

                _rg2D = gameObject.GetComponent<Rigidbody2D>();
                _capCol2D = GameObject.FindGameObjectWithTag(GROUND_CHECKER).GetComponent<CapsuleCollider2D>();

                _transform = gameObject.transform;

                _playerStats = gameObject.GetComponentInChildren<PlayerStats>();
                _playerStates = gameObject.GetComponentInChildren<PlayerStates>();

                // Khởi tạo các giá trị mặc định
                _isFacingRight = true;
                _movementController = new PlayerMovementController(this);
                _actionController = new PlayerActionController(this);
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
                MovementHandle();

                ActionHandle();
            }
            catch (Exception ex)
            {
                Debug.LogError($"PlayerController Update Failed: {ex}");
            }
            
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Thực hiện xử lý các hành động di chuyển của nhân vật.
        /// </summary>
        private void MovementHandle()
        {
            // Cập nhật vật lý nhân vật.
            _movementController.IsGrounded();
            _movementController.IsOnWall(this.gameObject.transform.localScale.x);

            _animationController.OnGrounded();

            // Thao tác vật lý nhân vật
            if (_playerStates.CanDownard)
                StartCoroutine(OnDownward());

            _animationController.OnJump();
            if (_playerStates.IsJumping)
                OnJump();

            if (_playerStates.IsDashing)
                StartCoroutine(OnDash());
            else OnMove();

            if (_playerStates.IsGrounded)
                _playerStates.JumpCount = 0;
        }

        /// <summary>
        /// Thực hiện xử lý các hành động tương tác, tấn công của nhân vật.
        /// </summary>
        private void ActionHandle()
        {
            _actionController.OnAttack();
            _animationController.OnAttack();
        }

        /// <summary>
        /// Thực hiện đổi chiều của nhân vật.
        /// </summary>
        public void FlipPlayerObject()
        {
            if (_playerStates == null) return;

            if (_playerStates.Horizontal > 0 && !IsFacingRight)
                IsFacingRight = true;
            else if (_playerStates.Horizontal < 0 && IsFacingRight)
                IsFacingRight = false;
        }

        /// <summary>
        /// Thực hiện điều phối sự di chuyển của nhân vật thông qua các Controller.
        /// </summary>
        private void OnMove()
        {
            FlipPlayerObject();

            _animationController.OnMove();
            _animationController.OnCrouch();
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
            if (_playerStates.IsGrounded)
            {
                Debug.Log("one");
                _movementController.OnJump();
                _playerStates.JumpCount++;
                _playerStates.IsDoubleJump = true;
            }
            else if (_playerStates.IsDoubleJump)
            {
                Debug.Log("double");
                _movementController.OnJump();
                _playerStates.JumpCount++;
                _playerStates.IsDoubleJump = false;
            }
            else if (!_playerStates.IsGrounded && _playerStates.JumpCount == 0 && !_playerStates.IsDoubleJump)
            {
                Debug.Log("hi");
                _movementController.OnJump();
                _playerStates.JumpCount++;
            }


            _playerStates.IsJumping = false;
        }

        /// <summary>
        /// Thực hiện điều phối sự lướt của nhân vật thông qua các Controller.
        /// </summary>
        private IEnumerator OnDash()
        {
            _animationController.OnDash();
            _movementController.OnDash();

            yield return new WaitForSeconds(_playerStats.PlayerStatsSO.dashDuration);

            _playerStates.IsDashing = false;
            _animationController.OnDash();
        }

        /// <summary>
        /// Thực hiện điều phối sự xuyên qua vật thể của nhân vật thông qua Collider.
        /// </summary>
        private IEnumerator OnDownward()
        {
            _capCol2D.enabled = false;
            yield return new WaitForSeconds(_playerStats.PlayerStatsSO.oneWayDuration);
            _capCol2D.enabled = true;
        }

        #endregion

        #region --- Properties ---

        public Rigidbody2D Rg2D => _rg2D;
        public CapsuleCollider2D CapCol2D => _capCol2D;

        public Transform Transform => _transform;

        public PlayerStats PlayerStats =>  _playerStats;
        public PlayerStates PlayerStates => _playerStates;

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
        [SerializeField] private CapsuleCollider2D _capCol2D;

        [SerializeField] private Transform _transform;

        private PlayerMovementController _movementController;
        private PlayerActionController _actionController;
        private PlayerAnimationController _animationController;

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private PlayerStates _playerStates;

        [SerializeField] private bool _isFacingRight;

        [SerializeField] private const string GROUND_CHECKER = "GroundChecker";

        #endregion

    }
}