using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// BossDeadState - Là một Dead State của Boss được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Dead.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 12/05/2025.
    /// </summary>
    public class BossDeadState : BaseState<BossController, BossStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo BossDeadState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu BossController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu BossStateFactory.</param>
        public BossDeadState(BossController stateController, BossStateFactory stateFactory) : base(stateController, stateFactory) { }

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
        }

        /// <summary>
        /// Chuyển đổi State.
        /// </summary>
        /// <param name="newState">Biến mang kiểu dữ liệu là BaseState.</param>
        public override void SwitchState(BaseState<BossController, BossStateFactory> newState)
        {
            base.SwitchState(newState);
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Xử lý logic khi Boss đang trong Dead State.
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