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
    /// BossJumpAttack - Thiết lập tấn công cận chiến cho Boss.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 27/05/2025.
    /// </summary>
    public class BossJumpAttack : IAttackHandle
    {
        public BossJumpAttack(BossController bossCtrl)
        {
            _enemyCtrl = bossCtrl;
        }

        #region --- Overrides ---

        /// <summary>
        /// Xử lý quy trình tấn công
        /// </summary>
        public void AttackHandle()
        {
            if (_jumpCount <= 0)
            {
                _enemyCtrl.Animator.SetBool(AnimationStrings.EndJump, true);
                _enemyCtrl.Animator.SetBool(AnimationStrings.IsFinish, false);
                return;
            }

            if (_triggerAnimation)
            {
                _enemyCtrl.Animator.SetBool(AnimationStrings.EndJump, false);
                _enemyCtrl.Animator.SetTrigger(AnimationStrings.JumpAttackTrigger);
                _triggerAnimation = false;
            }

           
            if (_trigger)
            {
                _trigger = false;
                _jumpPos = _enemyCtrl.AreaManager.AnchorSpawnerPos;
                Vector2 posDir = _jumpPos - (Vector2)_enemyCtrl.Col2D.bounds.center;
                FlipChasing(posDir.x);
            }

            Debug.DrawLine((Vector2)_enemyCtrl.Col2D.bounds.center, _jumpPos, Color.blue);
            Vector2 pos = _jumpPos - (Vector2)_enemyCtrl.Col2D.bounds.center;
            if (pos.magnitude >= 1f && _isDone)
            {
                AttackChasing(pos.normalized);
            }
            else if (pos.magnitude < 1f && _isDone)
            {
                _enemyCtrl.StartCoroutine(ResetCoolDownJump());
            }
        }

        public bool IsFinished { get; set; }

        #endregion

        #region --- Methods ---

        private  IEnumerator ResetCoolDownJump()
        {
            _isDone = false;
            _enemyCtrl.Rg2D.velocity = Vector2.zero;
            _enemyCtrl.States.IsDrop = true;
            yield return new WaitForSeconds(_enemyCtrl.Stats.BaseStats.attackDuration);
            _jumpCount--;
            _isDone = true;
            _trigger = true;
        }

        private void AttackChasing(Vector2 dir)
        {
            var speedX = dir.x * 20f;
            var speedY = dir.y * 20f;
            _enemyCtrl.Rg2D.velocity = new Vector2(speedX, speedY);
        }

        private void FlipChasing(float dirX)
        {
            Vector2 flipDir = dirX < 0 ? new Vector2(-1, 1) : new Vector2(1, 1);
            if(_enemyCtrl.transform.localScale.x != flipDir.x)
                _enemyCtrl.transform.localScale = flipDir;
        }

        #endregion

        #region --- Fields ---

        private BossController _enemyCtrl;

        private bool _trigger = true;
        private bool _triggerAnimation = true;
        private bool _isDone = true;

        private int _jumpCount = 3;

        private Vector2 _jumpPos;

        #endregion

    }
}
