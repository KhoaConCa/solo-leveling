using Platform2D.GlobalInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// EnemyActionChecker - Được tạo ra để thực hiện các tương tác liên quan đến Stats Enemy và Player.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 09/05/2025.
    /// </summary>
    public class EnemyActionChecker : MonoBehaviour, IDamageable
    {
        #region --- Overrides ---

        public void OnHit()
        {

        }

        public void ReceiveDamage(float damage, Vector2 knockBack)
        {
            if (_enemyController == null) return;

            _enemyController.Stats.CurrentHealthPoint -= damage - _enemyController.Stats.CurrentDefencePoint;

            if(_enemyController.Stats.CurrentHealthPoint <= 0)
            {
                _enemyController.States.IsDead = true;
                return;
            }

            _enemyController.States.IsHitting = true;
            _enemyController.States.KnockBackDirection = knockBack;
            Debug.Log($"{this.gameObject.name} get hit: {_enemyController.Stats.CurrentHealthPoint}");
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private EnemyController _enemyController;

        #endregion
    }
}
