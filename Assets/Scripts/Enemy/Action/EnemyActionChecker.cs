using Platform2D.GlobalInterface;
using Platform2D.Utilities;
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

        public void ReceiveDamage(float damage, Vector2 knockBack)
        {
            // Thoát hàm khi _enemyController là false.
            if (_enemyController == null) return;

            // Thoát hàm khi Enemy đang trong trạng thái kháng sát thương.
            if (_enemyController.States.Invulnerable) return;

            // Thoaát hàm khi Enemy đã chết.
            if (_enemyController.States.IsDead) return;

            _enemyController.Stats.CurrentHealthPoint -= damage - _enemyController.Stats.CurrentDefencePoint;
            _enemyController.HealthBar.ChangeHealth(_enemyController.Stats.CurrentHealthPoint);

            _enemyController.States.KnockBackDirection = knockBack;
            _enemyController.States.Invulnerable = true;

            if (_enemyController.Stats.CurrentHealthPoint <= 0)
            {
                _enemyController.States.IsDead = true;
                return;
            }

            _enemyController.States.IsHitting = true;
        }

        public void OnHit()
        {
            if (Player == null) return;
            Vector2 lengthDetect = _target.transform.position - _enemyController.transform.position;
            Vector2 dirDetect = lengthDetect.normalized.x < 0 ? new Vector2(-1, 0) : new Vector2(1, 0);
            Player.ReceiveDamage(_enemyController.Stats.BaseStats.attackDamage, dirDetect);
        }

        #endregion

        #region --- Unity Methods ---

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;

            if (!collision.gameObject.CompareTag(TagLayerName.Player)) return;

            Player = collision.gameObject.GetComponentInParent<PlayerActionChecker>();
            OnHit();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == null) return;

            if (!collision.gameObject.CompareTag(TagLayerName.Player)) return;

            Player = null;
        }

        #endregion

        #region --- Methods ---

        public void DetectedPlayer()
        {
            var col = Physics2D.OverlapCircle(_enemyController.Col2D.bounds.center, _enemyController.Stats.BaseStats.detectedRange, _playerLayer);
            if(col != null)
            {
                var targetCtrl = col.gameObject.GetComponent<PlayerCore>();
                if(targetCtrl.States.IsDead)
                {
                    _target = null;
                    return;
                }

                _target = col.gameObject;
                
                Vector2 lengthDetect = _target.transform.position - _enemyController.transform.position;
                RaycastHit2D rayHit = Physics2D.Raycast(_enemyController.transform.position, lengthDetect.normalized, lengthDetect.magnitude, LayerMask.GetMask(TagLayerName.Penatrable, TagLayerName.StaticLevel));
                if (rayHit.collider != null)
                {
                    _target = null;
                    _enemyController.States.IsDetecting = false;
                    return;
                }
                Vector2 dirDetect = lengthDetect.normalized.x < 0 ? new Vector2(-1, lengthDetect.normalized.y) : new Vector2(1, lengthDetect.normalized.y);
                if (dirDetect.x == _enemyController.States.Direction)
                {
                    _enemyController.States.IsDetecting = true;
                    _enemyController.States.RangeToPlayer = lengthDetect.magnitude;
                    Debug.DrawLine(_enemyController.transform.position, _target.transform.position, _detectedColor);
                }
                else
                {
                    _enemyController.States.IsDetecting = false;
                    _enemyController.States.RangeToPlayer = -1;
                }
            }
            else
            {
                _target = null;
                _enemyController.States.RangeToPlayer = -1;
                _enemyController.States.IsDetecting = false;
            }
        }

        public void OnDrawGizmos()
        {
            if (_showGizmos && _enemyController != null)
            {
                // Màu mặc định hoặc khi đã phát hiện player
                Gizmos.color = _enemyController.States.IsDetecting ? _detectedColor : _defaultDetectColor;

                // Vẽ tâm enemy
                Gizmos.DrawCube(_enemyController.Col2D.bounds.center, Vector2.one * 0.1f);

                // Vẽ phạm vi phát hiện (OverlapCircle)
                Gizmos.DrawWireSphere(_enemyController.Col2D.bounds.center, _enemyController.Stats.BaseStats.detectedRange);
            }
        }

        #endregion

        #region -- Properties --

        public IDamageable Player { get; private set; } = null;

        public GameObject Target => _target;

        #endregion

        #region --- Fields ---

        [SerializeField] private EnemyController _enemyController;

        [SerializeField] private GameObject _target;

        [SerializeField] private Transform _enemyDetected;

        [SerializeField] private LayerMask _playerLayer;

        [SerializeField] private bool _showGizmos = true;

        [SerializeField] private Color _defaultDetectColor = Color.green;
        [SerializeField] private Color _detectedColor = Color.red;

        #endregion
    }
}
