using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// BossInjureState - Là một Detect State của Boss được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Detect.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 28/05/2025.
    /// </summary>
    public class BossInjureState : BaseState<BossController, BossStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo BossInjureState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu BossController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu BossStateFactory.</param>
        public BossInjureState(BossController stateController, BossStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Detect State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.Animator.SetTrigger(AnimationStrings.WeaknessTrigger);
            _stateController.States.IsWeakness = true;

            _isDone = false;
        }

        /// <summary>
        /// Cập nhật Detect State.
        /// </summary>
        public override void UpdateState() 
        {
            if(!_isDone)
                InjureHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Detect State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.IsWeakness = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (_stateController.States.IsDead)
            {
                SwitchState(_stateFactory.Dead());
                return;
            }

            if (_stateController.States.CanAttack)
            {
                SwitchState(_stateFactory.Attack());
            }
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
        /// Xử lý logic khi Boss đang trong Detect State.
        /// </summary>
        private void InjureHandle()
        {
            _isDone = false;
            _stateController.StartCoroutine(ResetChainAttack());
        }

        private IEnumerator ResetChainAttack()
        {
            yield return new WaitForSeconds(_stateController.Stats.CurrentChainAttackDuration);
            _stateController.States.CanAttack = true;
        }

        #endregion

        #region --- Fields ---

        private float _timeHeal;
        private bool _isDone;

        #endregion
    }
}