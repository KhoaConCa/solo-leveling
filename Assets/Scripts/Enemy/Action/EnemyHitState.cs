using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyHitState - Là một Hit State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Hit.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 12/05/2025.
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

            if (_stateController.States.IsDetecting)
            {
                
                SwitchState(_stateFactory.Chasing());
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
            if (!_stateController.States.IsHitting) return;

            if((int)_stateController.States.KnockBackDirection.x == (int)_stateController.transform.localScale.x)
            {
                FlipDirectionHandle();
                _stateController.States.IsDetecting = true;
                Debug.Log("chasing flip");
                return;
            }

            var knockBackSpeed = _stateController.States.KnockBackDirection.x * _stateController.Stats.BaseStats.KnockBackForce;
            _stateController.Rg2D.velocity = new Vector2(knockBackSpeed, _stateController.Rg2D.velocity.y);
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

        private float _currentTime;

        #endregion
    }
}