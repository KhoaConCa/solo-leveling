using Platform2D.CharacterController;
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
            _runState = _stateFactory.Run();

            if (_stateController.Rg2D.velocity.y < _stateController.fallSpeedYDampingThreshold
                && !_stateController.CameraController.IsLerpingYDamping
                && !_stateController.CameraController.LerpedFromPlayerFalling)
            {
                _stateController.CameraController.LerpYDamping(true);
            }
        }

        /// <summary>
        /// Cập nhật Fall State.
        /// </summary>
        public override void UpdateState()
        {
            if (_stateController.States.OnMove != Vector2.zero)
                _runState.UpdateState();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Fall State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.IsFalling = false;
            _runState = null;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState()
        {
            if (_stateController.States.OnGround)
            {
                if (_stateController.States.OnMove == Vector2.zero)
                    SwitchState(_stateFactory.Idle());
                else if (Mathf.Abs(_stateController.States.OnMove.x) > 0.01f)
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

        #region --- Fields ---

        private BaseState<PlayerCore, PlayerStateFactory>? _runState;

        #endregion

    }
}