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
                _playerController.States.IsPenetrable = true;

                var oneWayCol = _oneWayGO.GetComponent<CompositeCollider2D>();
                Vector2 newVector = oneWayCol.transform.position - _playerController.Col2D.transform.position;
                if (newVector.normalized.y < 0.2f)
                    _playerController.States.IsPenetrable = false;

                return;
            }

            if (collision.gameObject.CompareTag(TagLayerName.Ground) && _playerController.States.IsPenetrable)
            {
                _groundGO = collision.gameObject;
                _playerController.States.IsPenetrable = false;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_tagOneWay))
            {
                _oneWayGO = null;
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

        [SerializeField] private string _tagOneWay;

        #endregion
    }
}
