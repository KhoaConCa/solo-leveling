using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerRecoveryState - Là một Recovery State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Recovery.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 12/05/2025.
    /// </summary>
    public class PlayerRecoveryState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerRecoveryState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerRecoveryState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Recovery State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.States.Invulnerable = true;
            _trigger = true;
            _stateController.Rg2D.velocity = new Vector2(0, _stateController.Rg2D.velocity.y);
        }

        /// <summary>
        /// Cập nhật Recovery State.
        /// </summary>
        public override void UpdateState() 
        {
            RecoveryHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Recovery State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.IsInteracted = false;
            _stateController.States.Invulnerable = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (!_stateController.States.CanMove) return;

            if (_stateController.States.IsDead)
            {
                SwitchState(_stateFactory.Dead());
                return;
            }

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
        /// Xử lý logic khi Player đang trong Recovery State.
        /// </summary>
        private void RecoveryHandle()
        {
            if (!_trigger) return;

            _stateController.Stats.CurrentHealthPoint = _stateController.Stats.BaseStats.healthPoint;
            _stateController.HealthBar.SetMaxHealth(_stateController.Stats.BaseStats.healthPoint, true);
            _stateController.OnRecoveryCallback?.Invoke();
        }

        #endregion

        #region --- Methods ---

        private bool _trigger;

        #endregion
    }
}