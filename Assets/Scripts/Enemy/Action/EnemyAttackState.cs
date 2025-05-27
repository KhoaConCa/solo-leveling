using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using Platform2D.EnemyAttackType;
using Platform2D.EnemyType;
using Platform2D.Utilities;
using Platform2D.Vector;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyAttackState - Là một GroundAttack State của Enemy được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc GroundAttack.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class EnemyAttackState : BaseState<EnemyController, EnemyStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo EnemyGroundAttackState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu EnemyCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu EnemyStateFactory.</param>
        public EnemyAttackState(EnemyController stateController, EnemyStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho GroundAttack State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.States.IsAttacking = true;
            _stateController.Rg2D.velocity = Vector2.zero;

            switch (_stateController.EnemyType)
            {
                case ENEMY_TYPE.MINIONS:
                    switch(_stateController.AttackType[0])
                    {
                        case ENEMY_ATTACK_TYPE.MELEE_ATTACK:
                            _attackHandle = _stateController.Stats.GetComponentInChildren<EnemyMeleeAttack>();
                            break;
                    }
                            
                    break;
            }
        }

        /// <summary>
        /// Cập nhật GroundAttack State.
        /// </summary>
        public override void UpdateState() 
        {
            AttackHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát GroundAttack State.
        /// </summary>
        public override void ExitState() 
        {
            _stateController.States.IsAttacking = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (_stateController.States.IsDetecting && _stateController.States.RangeToPlayer >= _stateController.Stats.BaseStats.attackRange)
            {
                SwitchState(_stateFactory.Chasing());
                return;
            }

            if (_stateController.States.IsHitting)
            {
                SwitchState(_stateFactory.Hit());
                return;
            }

            if (_stateController.States.CanMove)
            {
                SwitchState(_stateFactory.Idle());
            }
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
        /// Xử lý logic khi Enemy đang trong Attack State.
        /// </summary>
        private void AttackHandle()
        {
            _stateController.States.IsAttacking = false;
            _stateController.States.CanAttack = false;

            _attackHandle.AttackHandle();
        }

        #endregion

        #region --- Fields ---

        private IAttackHandle _attackHandle;

        #endregion
    }
}