using Platform2D.CharacterAnimation;
using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.BossAttackType
{
    /// <summary>
    /// BossMeleeAttack - Thiết lập tấn công cận chiến cho Boss.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 27/05/2025.
    /// </summary>
    public class BossMeleeAttack : IAttackHandle
    {
        public BossMeleeAttack(BossController bossCtrl)
        {
            _enemyCtrl = bossCtrl;
        }

        #region --- Overrides ---

        /// <summary>
        /// Xử lý quy trình tấn công
        /// </summary>
        public void AttackHandle()
        {
            Vector2 pos = _enemyCtrl.ActionChecker.TargetPlayer.transform.position - _enemyCtrl.transform.position;

            Debug.DrawLine(_enemyCtrl.transform.position, _enemyCtrl.ActionChecker.TargetPlayer.transform.position, Color.blue);
            FlipChasing(pos.x);
            if (pos.magnitude > _enemyCtrl.Stats.BaseStats.attackRange)
            {
                AttackChasing(pos.normalized);
            }
            else if (trigger)
            {

                _enemyCtrl.Animator.SetTrigger(AnimationStrings.MeleeAttackTrigger);
                _enemyCtrl.Rg2D.velocity = new Vector2(0, _enemyCtrl.Rg2D.velocity.y);
                trigger = false;
            }
        }

        public bool IsFinished { get; set; }

        #endregion

        #region --- Methods ---

        private void AttackChasing(Vector2 dir)
        {
            var speed = _enemyCtrl.Stats.CurrentMovementSpeed * 1.4f;
            _enemyCtrl.Rg2D.velocity = new Vector2(dir.x * speed, _enemyCtrl.Rg2D.velocity.y);
        }

        private void FlipChasing(float dirX)
        {
            Vector2 flipDir = dirX < 0 ? new Vector2(-1, 1) : new Vector2(1, 1);
            if(_enemyCtrl.transform.localScale.x != flipDir.x)
                _enemyCtrl.transform.localScale = flipDir;
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private BossController _enemyCtrl;

        private bool trigger = true;

        #endregion

    }
}
