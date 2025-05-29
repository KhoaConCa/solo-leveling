using Platform2D.GlobalInterface;
using Platform2D.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterController
{
    /// <summary>
    /// BossActionChecker - Được tạo ra để thực hiện các tương tác liên quan đến Stats Boss và Player.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 09/05/2025.
    /// </summary>
    public class BossActionChecker : MonoBehaviour, IDamageable
    {
        #region --- Overrides ---

        public void ReceiveDamage(float damage, Vector2 knockBack)
        {
            // Thoát hàm khi _bossController là false.
            if (_bossController == null) return;

            // Thoát hàm khi Boss đang trong trạng thái kháng sát thương.
            if (_bossController.States.Invulnerable) return;

            // Thoaát hàm khi Boss đã chết.
            if (_bossController.States.IsDead) return;

            var moreDmg = _bossController.States.IsWeakness ? _bossController.Stats.CurrentWeaknessMultiplier : 1f;
            var dmgDeal = (damage - _bossController.Stats.CurrentDefencePoint) * moreDmg;
            Debug.Log($"Dmg deal: {dmgDeal}");
            _bossController.Stats.CurrentHealthPoint -= dmgDeal;

            if (!_bossController.States.IsHitting)
            {
                _bossController.HealthBar.ChangeHealth(_bossController.Stats.CurrentHealthPoint);
                if (_bossController.Stats.CurrentHealthPoint > 0)
                    _bossController.StartCoroutine(GetHit());
                Debug.Log($"Boss {_bossController.Stats.BaseStats.name} get hit: Curhp ({_bossController.Stats.CurrentHealthPoint})");
            }
            
            if (_bossController.Stats.CurrentHealthPoint <= 0)
            {
                _bossController.States.IsDead = true;
                _bossController.Col2D.enabled = false;
            } 
        }

        public void OnHit()
        {
            if (Player == null) return;
            Vector2 lengthDetect = TargetPlayer.transform.position - _bossController.transform.position;
            Vector2 dirDetect = lengthDetect.normalized.x < 0 ? new Vector2(-1, 0) : new Vector2(1, 0);
            Player.ReceiveDamage(_bossController.Stats.CurrentAttackDamage, dirDetect);
        }

        #endregion

        #region --- Unity Methods ---

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision);
            if (collision == null) return;
            if (!collision.gameObject.CompareTag(TagLayerName.Player)) return;

            OnHit();
        }

        public void OnDrawGizmos()
        {
            if (_bossController != null)
            {
                // Màu mặc định hoặc khi đã phát hiện player
                Gizmos.color = Color.red;

                // Vẽ tâm enemy
                Gizmos.DrawCube(_bossController.Col2D.bounds.center, Vector2.one * 0.1f);

                // Vẽ phạm vi phát hiện (OverlapCircle)
                Vector2 boxSize = new Vector2(_bossController.Stats.BaseStats.detectedRange * 2.5f, _bossController.Stats.BaseStats.detectedRange);
                Gizmos.DrawWireCube(_bossController.Col2D.bounds.center, boxSize);
            }
        }

        #endregion

        #region --- Methods ---

        private IEnumerator GetHit()
        {
            _bossController.States.IsHitting = true;

            _bossController.SpriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.2f);

            _bossController.States.IsHitting = false;

            _bossController.SpriteRenderer.color = Color.white;
        }

        #endregion

        #region -- Properties --

        public IDamageable Player { get; set; } = null;

        public GameObject TargetPlayer
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
            }
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private GameObject _player = null;

        [SerializeField] private BossController _bossController;


        [SerializeField] private bool _showGizmos = true;

        #endregion
    }
}
