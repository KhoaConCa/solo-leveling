using Platform2D.BossAttackType;
using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using Platform2D.EnemyType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// BossAttackState - Là một GroundAttack State của Boss được kế thừa từ BaseState, được dùng để xử lý Logic và Animation thuộc GroundAttack.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025.
    /// </summary>
    public class BossAttackState : BaseState<BossController, BossStateFactory>
    {
        #region --- Overrides ---

        /// <summary>
        /// Khởi tạo BossGroundAttackState.
        /// </summary>
        /// <param name="stateController">Biến truyền vào mang kiểu dữ liệu BossCore.</param>
        /// <param name="stateFactory">Biến truyền vào mang kiểu dữ liệu BossStateFactory.</param>
        public BossAttackState(BossController stateController, BossStateFactory stateFactory) : base(stateController, stateFactory) { }

        /// <summary>
        /// Cài đặt mặc định cho GroundAttack State.
        /// </summary>
        public override void EnterState() 
        {
            _stateController.Rg2D.velocity = Vector2.zero;

            SetSkill();

            _stateController.StartCoroutine(ResetCoolDown(_stateController.Stats.BaseStats.appearanceDuration));
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
            _stateController.States.CanAttack = false;
        }

        /// <summary>
        /// Kiểm tra chuyển đổi State.
        /// </summary>
        public override void CheckSwitchState() 
        {
            if(_queAttackHandle.Count == 0 && !_resetCoolDown)
            {
                SwitchState(_stateFactory.Injure());
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
        /// Xử lý logic khi Boss đang trong Attack State.
        /// </summary>
        private void AttackHandle()
        {
            if (_queAttackHandle.Count == 0) return;

            if (_resetCoolDown) return;

            if (_stateController.States.IsFinishing)
                _queAttackHandle.Peek().Value.AttackHandle();
            else
            {
                _stateController.StartCoroutine(ResetCoolDown(_stateController.Stats.BaseStats.attackDuration));
                _queAttackHandle.Dequeue();
            }
        }

        private IEnumerator ResetCoolDown(float timer)
        {
            _resetCoolDown = true;
            yield return new WaitForSeconds(timer);
            _resetCoolDown = false;
        }

        private void SetSkill()
        {
            var checkPharse = _stateController.Stats.BaseStats.healthPoint * 30 / 100;
            if (_stateController.Stats.CurrentHealthPoint >= checkPharse)
            {
                _queAttackHandle.Enqueue(new KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>(ENEMY_ATTACK_TYPE.JUMP_ATTACK, new BossJumpAttack(_stateController)));
                _queAttackHandle.Enqueue(new KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>(ENEMY_ATTACK_TYPE.MELEE_ATTACK, new BossMeleeAttack(_stateController)));
                _queAttackHandle.Enqueue(new KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>(ENEMY_ATTACK_TYPE.MELEE_ATTACK, new BossMeleeAttack(_stateController)));
            }
            else
            {
                if(_stateController.States.IsRampage)
                {
                    _stateController.Stats.RampageStats();
                    _stateController.States.IsRampage = false;
                }
                _queAttackHandle.Enqueue(new KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>(ENEMY_ATTACK_TYPE.JUMP_ATTACK, new BossJumpAttack(_stateController)));
                _queAttackHandle.Enqueue(new KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>(ENEMY_ATTACK_TYPE.MELEE_ATTACK, new BossMeleeAttack(_stateController)));
                _queAttackHandle.Enqueue(new KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>(ENEMY_ATTACK_TYPE.JUMP_ATTACK, new BossJumpAttack(_stateController)));
                _queAttackHandle.Enqueue(new KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>(ENEMY_ATTACK_TYPE.MELEE_ATTACK, new BossMeleeAttack(_stateController)));
                _queAttackHandle.Enqueue(new KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>(ENEMY_ATTACK_TYPE.JUMP_ATTACK, new BossJumpAttack(_stateController)));
            }
        }

        #endregion

        #region --- Fields ---

        private readonly Queue<KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>> _queAttackHandle = new Queue<KeyValuePair<ENEMY_ATTACK_TYPE, IAttackHandle>>();

        private bool _resetCoolDown = false;

        #endregion
    }
}