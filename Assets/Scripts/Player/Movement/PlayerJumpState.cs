using Platform2D.CharacterController;
using Platform2D.Vector;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerJumpState - Là một Jump State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Jump.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerJumpState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerJumpState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerJumpState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Jump State.
        /// </summary>
        public override void EnterState()
        {
            _stateController.States.CanJump = true;

            if (_stateController.States.IsDoubleJump)
            {
                _stateController.CameraController.IsLerpingYDamping = false;
            }

            if (!_stateController.CameraController.IsLerpingYDamping
                && _stateController.CameraController.LerpedFromPlayerFalling)
            {
                _stateController.CameraController.LerpedFromPlayerFalling = false;
                _stateController.CameraController.LerpYDamping(false);
            }
        }

        /// <summary>
        /// Cập nhật Jump State.
        /// </summary>
        public override void UpdateState()
        {
            if (_stateController.States.IsJumping)
                JumpHandle();

            if (_stateController.States.IsCeiling)
                FocusFall();

            if (_stateController.States.OnMove != Vector2.zero && !_stateController.States.OnGround)
                RunHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Jump State.
        /// </summary>
        public override void ExitState()
        {
            _highestPos = 0f;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState()
        {
            if (_stateController.States.IsDashing && _stateController.States.CanDashing)
            {
                SwitchState(_stateFactory.Dash());
                return;
            }

            if ((int)_stateController.transform.position.y >= (int)_highestPos || _stateController.States.IsCeiling)
            {
                SwitchState(_stateFactory.Fall());
            }
        }

        /// <summary>
        /// Chuyển đổi State.
        /// </summary>
        /// <param name="newState">Biến mang kiểu dữ liệu là BaseState.</param>
        public override void SwitchState(BaseState<PlayerCore, PlayerStateFactory> newState)
        {
            base.SwitchState(newState);
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Xử lý logic khi Player đang trong Jump State.
        /// </summary>
        private void JumpHandle()
        {
            if (_stateController.States.OnGround && !_stateController.States.IsPenetrable)
            {
                JumpForce(_stateController.Stats.BaseStats.jumpSpeed);
                _stateController.States.IsDoubleJump = true;
            }
            else if (_stateController.States.IsDoubleJump)
            {
                JumpForce(_stateController.Stats.BaseStats.DoubleJumpSpeed);
                _stateController.States.IsDoubleJump = false;
                _stateController.States.CanJump = false;
                Debug.Log("Double Jump");
            }

            _stateController.States.IsJumping = false;
        }

        /// <summary>
        /// Tập trung xử lý rơi khi nhân vật đụng trần.
        /// </summary>
        private void FocusFall()
        {
            if (_stateController.MovementChecker.IsGround)
            {
                _stateController.Rg2D.velocity = new Vector2(_stateController.Rg2D.velocity.x, 0f);

            }
        }

        /// <summary>
        /// Thực hiện lực nhảy và xác định điểm cao nhất khi nhảy.
        /// </summary>
        /// <param name="jumpSpeed">Tham số đầu vào của lực.</param>
        private void JumpForce(float jumpSpeed)
        {
            // Tính độ cao cao nhất khi nhảy.
            float pForce = Mathf.Abs(Physics2D.gravity.y) * _stateController.Rg2D.gravityScale;
            float highestPoint = (jumpSpeed * jumpSpeed) / (2 * pForce);
            _highestPos = _stateController.transform.position.y + highestPoint;

            float velX = _stateController.Rg2D.velocity.x;
            float velY = jumpSpeed * (float)AXIS_1D.POSITIVE;

            _stateController.Rg2D.velocity = new Vector2(velX, velY);
        }

        /// <summary>
        /// Thực hiện di chuyển khi nhân vật đang nhảy.
        /// </summary>
        private void RunHandle()
        {
            float dirX = _stateController.States.OnMove.x < 0 ? (float)AXIS_1D.NEGATIVE : (float)AXIS_1D.POSITIVE;
            if (_stateController.transform.localScale.x != dirX)
                FlipDirection(dirX);

            if (_stateController.States.IsWall || Mathf.Abs(_stateController.States.OnMove.y) > 0.7f)
                dirX = 0f;

            float speed = _stateController.Stats.CurrentMovementSpeed * dirX;
            _stateController.Rg2D.velocity = new Vector2(speed, _stateController.Rg2D.velocity.y);
        }

        /// <summary>
        /// Thực hiện xoay hướng khi nhân vật đang nhảy.
        /// </summary>
        /// <param name="dirX">Chiều của hướng cần xoay.</param>
        private void FlipDirection(float dirX)
        {
            float dirY = _stateController.transform.localScale.y;
            _stateController.transform.localScale = new Vector2(dirX, dirY);
            _stateController.CameraFollower.TurnCalling();
        }

        #endregion

        #region --- Fields ---

        private float _highestPos;

        #endregion
    }
}