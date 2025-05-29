using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Vector;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerReviveState - Là một Revive State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Revive.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerReviveState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerReviveState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerReviveState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Revive State.
        /// </summary>
        public override void EnterState() 
        {
            _isTrigger = true;
        }

        /// <summary>
        /// Cập nhật Revive State.
        /// </summary>
        public override void UpdateState() 
        {
            if(_isTrigger)
                ReviveHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Revive State.
        /// </summary>
        public override void ExitState() 
        {
            _stateController.States.IsRevived = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (!_stateController.States.IsDead)
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
        /// Xử lý logic khi Player đang trong Revive State.
        /// </summary>
        private void ReviveHandle()
        {
            _isTrigger = false;

            _stateController.Stats.CurrentDamage = 0;
            _stateController.Stats.CurrentHealthPoint = _stateController.Stats.BaseStats.healthPoint;
            _stateController.HealthBar.SetMaxHealth(_stateController.Stats.BaseStats.healthPoint, true);

            _stateController.States.IsDead = false;
        }



        #endregion

        #region --- Fields ---

        private bool _isTrigger = true;

        #endregion

    }
}