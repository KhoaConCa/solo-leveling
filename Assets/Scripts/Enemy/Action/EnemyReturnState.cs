using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyReturnState - Là một Return State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc Return.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 12/05/2025.
    /// </summary>
    public class EnemyReturnState : BaseState<EnemyController, EnemyStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo EnemyReturnState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu EnemyController.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu EnemyStateFactory.</param>
        public EnemyReturnState(EnemyController stateController, EnemyStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho Return State.
        /// </summary>
        public override void EnterState() 
        {
            _healingTimer = new Timer();
            _healingTimer.StartCountdown();

            _stateController.States.IsReturn = true;
            _stateController.States.IsMoving = true;

            _direction = (int)_stateController.States.Direction;
        }

        /// <summary>
        /// Cập nhật Return State.
        /// </summary>
        public override void UpdateState() 
        {
            ReturnHandle();

            HealingHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát Return State.
        /// </summary>
        public override void ExitState()
        {
            _stateController.States.IsReturn = false;
            _stateController.States.Invulnerable = false;
            _stateController.States.FirstFlipDirection = false;
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

            if (!_stateController.States.IsMoving && (int)_stateController.Stats.CurrentHealthPoint == (int)_stateController.Stats.BaseStats.healthPoint)
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
        /// Xử lý logic khi Enemy đang trong Run State.
        /// </summary>
        private void ReturnHandle()
        {
            if (!_stateController.States.IsMoving) return;

            if (_direction == (int)_stateController.States.Direction)
                FlipDirectionHandle();

            if (_stateController.States.IsMoving)
                _stateController.Rg2D.velocity = new Vector2(
                                _stateController.States.Direction * _stateController.Stats.BaseStats.movementSpeed,
                                _stateController.Rg2D.velocity.y
                            );

            if (_stateController.States.OnWall || (int)_stateController.transform.position.x == (int)_stateController.States.AnchorPosX || !_stateController.States.OnGround)
                _stateController.States.IsMoving = false;
        }

        /// <summary>
        /// Xử lý logic đổi hướng di chuyển của Enemy.
        /// </summary>
        private void FlipDirectionHandle()
        {
            _stateController.States.Direction = -_stateController.transform.localScale.x;
            _stateController.transform.localScale = new Vector2(_stateController.States.Direction, 1f);
        }

        /// <summary>
        /// Xử lý logic hồi máu khi Enemy đang mất máu.
        /// </summary>
        private void HealingHandle()
        {
            if ((int)_stateController.Stats.CurrentHealthPoint == (int)_stateController.Stats.BaseStats.healthPoint) return;
            if (_healingTimer.FixedTimeCountdown(_healingCoolDown))
            {
                float healingPoint = _stateController.Stats.BaseStats.healthPoint * 5 / 100;
                _stateController.Stats.CurrentHealthPoint += healingPoint;
                Debug.Log(_stateController.Stats.CurrentHealthPoint);

                _healingTimer.StartCountdown();
            }
            
            if (_stateController.Stats.CurrentHealthPoint > _stateController.Stats.BaseStats.healthPoint)
                _stateController.Stats.CurrentHealthPoint = _stateController.Stats.BaseStats.healthPoint;
        }

        #endregion

        #region --- Fields ---

        private Timer _healingTimer;
        private readonly float _healingCoolDown = 1f;

        private int _direction;

        #endregion
    }
}