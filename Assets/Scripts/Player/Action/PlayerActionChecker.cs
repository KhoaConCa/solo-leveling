using Platform2D.GlobalInterface;
using Platform2D.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platform2D.CharacterController
{
    public class PlayerActionChecker : MonoBehaviour, IDamageable
    {
        #region --- Overrides ---

        public void ReceiveDamage(float damage, Vector2 knockBack)
        {
            // Thoát hàm khi _playerController là false.
            if (_playerController == null) return;

            // Thoát hàm khi Enemy đang trong trạng thái kháng sát thương.
            if (_playerController.States.Invulnerable) return;

            // Thoaát hàm khi Enemy đã chết.
            if (_playerController.States.IsDead) return;

            _playerController.Stats.CurrentHealthPoint -= damage - _playerController.Stats.CurrentDefencePoint;
            _playerController.HealthBar.ChangeHealth(_playerController.Stats.CurrentHealthPoint, true);

            _playerController.States.KnockBackDirection = knockBack;
            _playerController.States.Invulnerable = true;

            if (_playerController.Stats.CurrentHealthPoint <= 0)
            {
                _playerController.States.IsDead = true;
                return;
            }

            _playerController.States.IsHitting = true;
            Debug.Log($"{this.gameObject.name} get hit: {_playerController.Stats.CurrentHealthPoint}");
        }

        #endregion

        #region --- Unity Methods ---

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;

            if(!collision.gameObject.CompareTag(TagLayerName.Enemy)) return;

            Enemy = collision.gameObject.GetComponent<IDamageable>();
            if(Enemy == null) 
                Enemy = collision.gameObject.GetComponentInParent<IDamageable>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == null) return;

            if (!collision.gameObject.CompareTag(TagLayerName.Enemy)) return;

            Enemy = null;
        }

        #endregion

        #region --- Properties ---

        public IDamageable Enemy { get; private set; } = null;

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerCore _playerController;

        #endregion
    }
}
