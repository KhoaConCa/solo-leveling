using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Vector;
using System.Collections;
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
            _runState = _stateFactory.Run();
        }

        /// <summary>
        /// Cập nhật Jump State.
        /// </summary>
        public override void UpdateState() 
        {
            if (_stateController.States.IsJumping)
                JumpHandle();

            if(_stateController.States.OnMove != Vector2.zero)
                _runState.UpdateState();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Jump State.
        /// </summary>
        public override void ExitState() 
        {
            _runState = null;
            _highestPos = 0f;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if ((int)_stateController.transform.position.y >= (int)_highestPos)
                SwitchState(_stateFactory.Fall());
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
            if (_stateController.States.OnGround)
            {
                JumpForce(_stateController.Stats.BaseStats.jumpSpeed);
                _stateController.States.IsDoubleJump = true;
            }
            else if (_stateController.States.IsDoubleJump)
            {
                JumpForce(_stateController.Stats.BaseStats.DoubleJumpSpeed);
                _stateController.States.IsDoubleJump = false;
                _stateController.States.CanJump = false;
            }

            _stateController.States.IsJumping = false;
        }

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

        #endregion

        #region --- Fields ---

        private float _highestPos;

        private BaseState<PlayerCore, PlayerStateFactory>? _runState;

        #endregion
    }
}