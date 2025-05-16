using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyIdleState - Là một Idle State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Idle.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025.
    /// </summary>
    public class EnemyIdleState : BaseState<EnemyController, EnemyStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo EnemyIdleState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu EnemyController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu EnemyStateFactory.</param>
        public EnemyIdleState(EnemyController stateController, EnemyStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Idle State.
        /// </summary>
        public override void EnterState() 
        {
            _idleTimer = new Timer();
            _idleTimer.StartCountdown();

            _idleDuration = _stateController.Stats.BaseStats.idleDuration;
            _stateController.States.IsMoving = false;

            _stateController.Rg2D.velocity = new Vector2(0f, _stateController.Rg2D.velocity.y);
        }

        /// <summary>
        /// Cập nhật Idle State.
        /// </summary>
        public override void UpdateState() 
        {
            if (!_stateController.States.IsMoving)
                IdleHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Idle State.
        /// </summary>
        public override void ExitState() 
        {
            _idleTimer = null;
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

            if (_stateController.States.IsHitting)
            {
                SwitchState(_stateFactory.Hit());
                return;
            }

            if (_stateController.States.IsDetecting)
            {
                SwitchState(_stateFactory.Detect());
                return;
            }

            if (_stateController.States.IsMoving)
                SwitchState(_stateFactory.Run());
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
        /// Xử lý logic khi Enemy đang trong Idle State.
        /// </summary>
        private void IdleHandle()
        {
            _stateController.States.IsMoving = _idleTimer.FixedTimeCountdown(_idleDuration);

            if (!_stateController.States.IsMoving) return;

            if(_stateController.States.FirstFlipDirection)
                FlipDirectionHandle();
            _stateController.States.FirstFlipDirection = true;
        }

        /// <summary>
        /// Xử lý logic đổi hướng di chuyển của Enemy.
        /// </summary>
        private void FlipDirectionHandle()
        {
            _stateController.States.Direction = -_stateController.transform.localScale.x;
            _stateController.transform.localScale = new Vector2(_stateController.States.Direction, 1f);
        }

        #endregion

        #region --- Fields ---

        private float _idleDuration;

        private Timer _idleTimer;

        #endregion
    }
}