using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerIdleState - Là một Idle State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Idle.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerIdleState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerIdleState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerIdleState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Idle State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.States.IsMoving = false;
            _stateController.Rg2D.velocity = new Vector2(0f, _stateController.Rg2D.velocity.y);
        }

        /// <summary>
        /// Cập nhật Idle State.
        /// </summary>
        public override void UpdateState() 
        {
            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Idle State.
        /// </summary>
        public override void ExitState() 
        {
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (!_stateController.States.AllowedSwitch) return;

            if (_stateController.States.IsJumping)
            {
                SwitchState(_stateFactory.Jump());
                return;
            }

            if(_stateController.States.OnMove != Vector2.zero && Mathf.Abs(_stateController.States.OnMove.y) < 0.7f)
                SwitchState(_stateFactory.Run());
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
    }
}