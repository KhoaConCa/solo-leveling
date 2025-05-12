using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyHitState - Là một Hit State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Hit.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    public class EnemyHitState : BaseState<EnemyController, EnemyStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo EnemyHitState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu EnemyController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu EnemyStateFactory.</param>
        public EnemyHitState(EnemyController stateController, EnemyStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Hit State.
        /// </summary>
        public override void EnterState() 
        {
        }

        /// <summary>
        /// Cập nhật Hit State.
        /// </summary>
        public override void UpdateState() 
        {
            CheckSwitchState();

            HitHandle();
        }

        /// <summary>
        /// Thoát Hit State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.IsHitting = false;
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
        public override void SwitchState(BaseState<EnemyController, EnemyStateFactory> newState)
        {
            base.SwitchState(newState);
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Xử lý logic khi Enemy đang trong Hit State.
        /// </summary>
        private void HitHandle()
        {
            var knockBackSpeed = _stateController.States.KnockBackDirection.x * _stateController.Stats.BaseStats.KnockBackForce;
            _stateController.Rg2D.velocity = new Vector2(knockBackSpeed, _stateController.Rg2D.velocity.y);
            Debug.Log("hello");
        }

        #endregion

        #region --- Fields ---

        private float _currentTime;

        #endregion
    }
}