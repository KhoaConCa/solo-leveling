using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyDetectState - Là một Detect State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Detect.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 12/05/2025.
    /// </summary>
    public class EnemyDetectState : BaseState<EnemyController, EnemyStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo EnemyDetectState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu EnemyController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu EnemyStateFactory.</param>
        public EnemyDetectState(EnemyController stateController, EnemyStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Detect State.
        /// </summary>
        public override void EnterState() 
        {
            _detectTimer = new Timer();
            _detectTimer.StartCountdown();

            _detectDuration = _stateController.Stats.BaseStats.detectDuration;
            _stateController.States.Invulnerable = true;
            _stateController.States.IsMoving = false;
            _stateController.Rg2D.velocity = Vector2.zero;
        }

        /// <summary>
        /// Cập nhật Detect State.
        /// </summary>
        public override void UpdateState() 
        {
            DetectHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Detect State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.Invulnerable = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (_stateController.States.IsChasing)
            {
                SwitchState(_stateFactory.Chasing());
                return;
            }

            if (!_stateController.States.IsDetecting)
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
        /// Xử lý logic khi Enemy đang trong Detect State.
        /// </summary>
        private void DetectHandle()
        {
            if(_stateController.States.IsDetecting)
            {
                _stateController.States.IsChasing = _detectTimer.FixedTimeCountdown(_detectDuration);
            }
        }

        #endregion

        #region --- Fields ---

        private float _detectDuration;

        private Timer _detectTimer;

        #endregion
    }
}