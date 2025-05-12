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

        public void OnHit()
        {
            if (Enemy == null) return;

            Enemy.ReceiveDamage(_playerController.Stats.CurrentDamage, _playerController.transform.localScale);
        }

        public void ReceiveDamage(float damage, Vector2 knockBack)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region --- Unity Methods ---

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;

            if(!collision.gameObject.CompareTag(TagLayerName.Enemy)) return;

            Enemy = collision.gameObject.GetComponent<EnemyActionChecker>();
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
