using Platform2D.CharacterController;
using Platform2D.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform2D.CharacterController
{
    public class PlayerMovementChecker : MonoBehaviour
    {
        #region --- Unity Methods ---

        private void Awake()
        {
            _playerController = gameObject.GetComponent<PlayerCore>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision == null) return;

            if(collision.gameObject.CompareTag(TagLayerName.OneWay))
            {
                _oneWayGO = collision.gameObject;            
            }

            if (collision.gameObject.CompareTag(TagLayerName.Ground))
            {
                _groundGO = collision.gameObject;
                _playerController.States.IsPenetrable = false;
            }

            RaycastHit2D hit = Physics2D.Raycast(_playerController.Col2D.bounds.center, Vector2.down, 1.5f, LayerMask.GetMask(TagLayerName.Penatrable, TagLayerName.StaticLevel));
            Debug.DrawRay(_playerController.Col2D.bounds.max, Vector2.up, Color.green, 10f);
            if (hit.collider != null && (hit.collider.gameObject.CompareTag(TagLayerName.OneWay) || hit.collider.gameObject.CompareTag(TagLayerName.Ground)))
                _playerController.States.IsTouchOneWay = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagLayerName.OneWay))
            {
                RaycastHit2D hit = Physics2D.Raycast(_playerController.Col2D.bounds.center, Vector2.up, 1.5f, LayerMask.GetMask(TagLayerName.Penatrable));
                Debug.DrawRay(_playerController.Col2D.bounds.max, Vector2.up, Color.red, 10f);
                if(hit.collider != null && hit.collider.gameObject.CompareTag(TagLayerName.OneWay))
                    _playerController.States.IsTouchOneWay = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagLayerName.OneWay))
            {
                _oneWayGO = null;
                return;
            }

            if (collision.gameObject.CompareTag(TagLayerName.Ground))
            {
                _groundGO = null;
            }
        }

        #endregion

        #region --- Methods ---

        public bool TryStartDisable()
        {
            if (_oneWayGO == null || _playerController == null) return false;

            _playerController.StartCoroutine(DisableCollider());
            return true;
        }


        public IEnumerator DisableCollider()
        {
            var oneWayCol = _oneWayGO.GetComponent<CompositeCollider2D>();
            Physics2D.IgnoreCollision(_playerController.Col2D, oneWayCol);

            yield return new WaitForSeconds(0.25f);

            Physics2D.IgnoreCollision(_playerController.Col2D, oneWayCol, false);
        }

        #endregion

        #region --- Properties ---

        public bool IsOneWay => _oneWayGO != null && _oneWayGO.CompareTag(TagLayerName.OneWay);
        public bool IsGround => _groundGO != null && _groundGO.CompareTag(TagLayerName.Ground);

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerCore _playerController;

        [SerializeField] private GameObject _oneWayGO;
        [SerializeField] private GameObject _groundGO;

        #endregion
    }
}
