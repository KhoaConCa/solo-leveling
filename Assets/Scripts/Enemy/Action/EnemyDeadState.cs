using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyDeadState - Là một Dead State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Dead.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 13/05/2025.
    /// </summary>
    public class EnemyDeadState : BaseState<EnemyController, EnemyStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo EnemyDeadState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu EnemyController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu EnemyStateFactory.</param>
        public EnemyDeadState(EnemyController stateController, EnemyStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Dead State.
        /// </summary>
        public override void EnterState() 
        {
            DeadHandle();
            Debug.Log("Dead");
        }

        /// <summary>
        /// Cập nhật Dead State.
        /// </summary>
        public override void UpdateState() 
        {
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
        public override void SwitchState(BaseState<EnemyController, EnemyStateFactory> newState)
        {
            base.SwitchState(newState);
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Xử lý logic khi Enemy đang trong Dead State.
        /// </summary>
        private void DeadHandle()
        {
            var knockBackSpeed = _stateController.States.KnockBackDirection.x * _stateController.Stats.BaseStats.DeadKnockBackForce;
            _stateController.Rg2D.velocity = new Vector2(knockBackSpeed, _stateController.Rg2D.velocity.y);
            Debug.Log(_stateController.Rg2D.velocity);
        }

        #endregion
    }
}