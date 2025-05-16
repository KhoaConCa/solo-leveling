using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyChasingState - Là một Chasing State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Chasing.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 12/05/2025.
    /// </summary>
    public class EnemyChasingState : BaseState<EnemyController, EnemyStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo EnemyChasingState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu EnemyController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu EnemyStateFactory.</param>
        public EnemyChasingState(EnemyController stateController, EnemyStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Chasing State.
        /// </summary>
        public override void EnterState() 
        {
            _anchorMaxPos = _stateController.States.AnchorPosX + (_stateController.Stats.BaseStats.maxMovementRange * _stateController.States.Direction);
            _stateController.States.IsMoving = true;
        }

        /// <summary>
        /// Cập nhật Chasing State.
        /// </summary>
        public override void UpdateState() 
        {
            ChasingHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Chasing State.
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
            if (_stateController.States.IsHitting)
            {
                SwitchState(_stateFactory.Hit());
                return;
            }

            if (!_stateController.States.IsDetecting || ReachMaxPos())
                SwitchState(_stateFactory.Return());
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
        /// Xử lý logic khi Enemy đang trong Chasing State.
        /// </summary>
        private void ChasingHandle()
        {
            if(_stateController.States.RangeToPlayer <= 1)
            {
                _stateController.Rg2D.velocity = new Vector2(0f, _stateController.Rg2D.velocity.y);
                return;
            }

            bool reachAnchorPos = ReachMaxPos();

            if (_stateController.States.IsMoving)
                _stateController.Rg2D.velocity = new Vector2(
                                _stateController.States.Direction * _stateController.Stats.BaseStats.movementSpeed,
                                _stateController.Rg2D.velocity.y
                            );

            if (_stateController.States.OnWall || reachAnchorPos || !_stateController.States.OnGround)
                _stateController.States.IsMoving = false;
        }

        private bool ReachMaxPos()
        {
            float currentPosX = _stateController.trans2D.position.x;
            float direction = _stateController.States.Direction;
            return (currentPosX <= _anchorMaxPos && direction < 0) || (currentPosX >= _anchorMaxPos && direction > 0);
        }

        #endregion


        #region --- Fields ---

        private float _anchorMaxPos;

        #endregion
    }
}