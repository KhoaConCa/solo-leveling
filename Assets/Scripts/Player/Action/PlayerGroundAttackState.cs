using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.Vector;
using System.Collections;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerGroundAttackState - Là một GroundAttack State của Player được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc GroundAttack.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class PlayerGroundAttackState : BaseState<PlayerCore, PlayerStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo PlayerGroundAttackState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu PlayerCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu PlayerStateFactory.</param>
        public PlayerGroundAttackState(PlayerCore stateController, PlayerStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho GroundAttack State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.Stats.CurrentDamage = _stateController.Stats.BaseStats.attackDamage;
        }

        /// <summary>
        /// Cập nhật GroundAttack State.
        /// </summary>
        public override void UpdateState() 
        {
            GroundAttackHandle();

            CheckSwitchState();
        }

        /// <summary>
        /// Thoát GroundAttack State.
        /// </summary>
        public override void ExitState() 
        {
            _stateController.States.IsAttacking = false;
            _stateController.Stats.CurrentDamage = 0;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if (_stateController.States.CanMove)
            {
                if (_stateController.States.OnMove == Vector2.zero || Mathf.Abs(_stateController.States.OnMove.y) > 0.7f)
                    SwitchState(_stateFactory.Idle());
                else
                    SwitchState(_stateFactory.Run());
            }
        }

        /// <summary>
        /// Chuyển đổi State.
        /// </summary>
        /// <param name="newState">Biến mang kiểu dữ liệu là BaseState.</param>
        public override void SwitchState(BaseState<PlayerCore, PlayerStateFactory> newState)
        {
            base.SwitchState(newState);
        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Xử lý logic khi Player đang trong GroundAttack State.
        /// </summary>
        private void GroundAttackHandle()
        {
            if (!_stateController.States.IsAttacking) return;

            AttackPullForce();

            if (_stateController.ActionChecker.Enemy == null) return;

            _stateController.ActionChecker.Enemy.ReceiveDamage(_stateController.Stats.CurrentDamage, _stateController.transform.localScale);

            if (!_stateController.States.CanIncreaseDamage)
                IncreaseDamageCombo();

            _stateController.States.IsAttacking = false;
        }

        private void IncreaseDamageCombo()
        {
            _stateController.Stats.CurrentDamage += _stateController.Stats.CurrentDamage * _stateController.Stats.BaseStats.attackDamageMultiplier;
            Debug.Log(_stateController.Stats.CurrentDamage);
        }

        private void AttackPullForce()
        {
            var speed = _stateController.Stats.BaseStats.attackPullForce * _stateController.transform.localScale.x;
            _stateController.Rg2D.velocity = new Vector2(speed, _stateController.Rg2D.velocity.y);
        }

        #endregion

    }
}