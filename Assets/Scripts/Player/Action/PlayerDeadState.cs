using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerDeadState - Là một Dead State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Dead.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 12/05/2025.
    /// </summary>
    public class PlayerDeadState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerDeadState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerDeadState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Dead State.
        /// </summary>
        public override void EnterState() 
        {
        }

        /// <summary>
        /// Cập nhật Dead State.
        /// </summary>
        public override void UpdateState() 
        {
            if(_isTrigger)
                DeadHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Dead State.
        /// </summary>
        public override void ExitState()
        {
            
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (_stateController.States.IsRevived)
            {
                SwitchState(_stateFactory.Revive());
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
        /// Xử lý logic khi Player đang trong Dead State.
        /// </summary>
        private void DeadHandle()
        {
            _isTrigger = false;

            _stateController.StartCoroutine(ShowDead());
        }

        private IEnumerator ShowDead()
        {
            yield return new WaitForSeconds(1.5f);

            _stateController.OnDeadCallback?.Invoke();
        }

        #endregion

        #region --- Fields ---

        private bool _isTrigger = true;

        #endregion
    }
}