using Platform2D.CharacterController;
using Platform2D.CharacterInterface;
using Platform2D.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.EnemyAttackType
{
    /// <summary>
    /// EnemyDashAttack - Thiết lập tấn công lướt cho Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 27/05/2025.
    /// </summary>
    public class EnemyDashAttack : MonoBehaviour, IAttackHandle
    {
        #region --- Overrides ---

        /// <summary>
        /// Xử lý quy trình tấn công
        /// </summary>
        public void AttackHandle()
        {
            AttackDashForce();

            if (_enemyCtrl.States.CanMove)
                IsFinished = true;
        }

        public bool IsFinished { get; set; }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Lực đẩy nhẹ khi tấn công.
        /// </summary>
        private void AttackDashForce()
        {
            var speed = _meleeStatsSO.attackPullForce * _enemyCtrl.transform.localScale.x;
            _enemyCtrl.Rg2D.velocity = new Vector2(speed, _enemyCtrl.Rg2D.velocity.y);
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private EnemyController _enemyCtrl;

        [SerializeField] private EnemyMeleeStatsSO _meleeStatsSO;

        #endregion

    }
}
