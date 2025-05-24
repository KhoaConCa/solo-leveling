using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerHitState - Là một Hit State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Hit.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 12/05/2025.
    /// </summary>
    public class PlayerHitState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerHitState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerHitState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Hit State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.States.Invulnerable = true;
        }

        /// <summary>
        /// Cập nhật Hit State.
        /// </summary>
        public override void UpdateState() 
        {
            HitHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Hit State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.IsHitting = false;
            _stateController.States.Invulnerable = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (!_stateController.States.CanMove) return;

            if (_stateController.States.IsMoving)
                SwitchState(_stateFactory.Run());
            else
                SwitchState(_stateFactory.Idle());
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
        /// Xử lý logic khi Player đang trong Hit State.
        /// </summary>
        private void HitHandle()
        {
            Debug.Log(_stateController.States.IsHitting);
            if (!_stateController.States.IsHitting) return;

            var knockBackSpeed = _stateController.States.KnockBackDirection.x * _stateController.Stats.BaseStats.KnockBackForce;
            _stateController.Rg2D.velocity = new Vector2(knockBackSpeed, _stateController.Rg2D.velocity.y);
        }

        #endregion
    }
}