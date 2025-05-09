using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Vector;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerFallState - Là một Fall State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Fall.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerFallState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerFallState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerFallState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Fall State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.States.IsFalling = true;
        }

        /// <summary>
        /// Cập nhật Fall State.
        /// </summary>
        public override void UpdateState() 
        {
            if (!_stateController.States.IsPenetrable && _stateController.States.OnMove != Vector2.zero && Mathf.Abs(_stateController.States.OnMove.y) < 0.7f)
                RunHandle();

            CheckSwitchState(); 
        }

        /// <summary>
        /// Thoát Fall State.
        /// </summary>
        public override void ExitState() 
        {
            _stateController.States.IsFalling = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (_stateController.States.IsTouchOneWay) return;

            if (_stateController.States.IsDashing && _stateController.States.CanDashing)
            {
                SwitchState(_stateFactory.Dash());
                return;
            }

            if (_stateController.States.OnGround && !_stateController.States.IsPenetrable)
            {
                if (_stateController.States.OnMove == Vector2.zero || Mathf.Abs(_stateController.States.OnMove.y) > 0.7f)
                    SwitchState(_stateFactory.Idle());
                else
                    SwitchState(_stateFactory.Run());
            }
            else if (_stateController.States.IsJumping && _stateController.States.CanJump)
                    SwitchState(_stateFactory.Jump());
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
        /// Thực hiện di chuyển khi nhân vật đang rơi.
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
        /// Thực hiện xoay hướng khi nhân vật đang rơi.
        /// </summary>
        /// <param name="dirX">Chiều của hướng cần xoay.</param>
        private void FlipDirection(float dirX)
        {
            float dirY = _stateController.transform.localScale.y;
            _stateController.transform.localScale = new Vector2(dirX, dirY);
            _stateController.CameraFollower.TurnCalling();
        }

        #endregion

    }
}