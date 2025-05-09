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

            _runState = _stateFactory.Run();
        }

        /// <summary>
        /// Cập nhật Fall State.
        /// </summary>
        public override void UpdateState() 
        {
            if (!_stateController.States.IsPenetrable && _stateController.States.OnMove != Vector2.zero && Mathf.Abs(_stateController.States.OnMove.y) < 0.7f)
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

        #region --- Fields ---

        private BaseState<PlayerCore, PlayerStateFactory> _runState;

        #endregion

    }
}