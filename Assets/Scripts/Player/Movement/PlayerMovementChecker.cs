using Platform2D.CharacterController;
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
            if(collision.gameObject.CompareTag(_tagOneWay))
                _oneWayGO = collision.gameObject;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_tagOneWay))
                _oneWayGO = null;
        }

        #endregion

        #region --- Methods ---

        public IEnumerator DisableCollider()
        {
            if (_oneWayGO == null && _playerController == null) yield return null;

            _playerController.CurrentState = _playerController.StateFactory.Fall();
            _playerController.CurrentState.EnterState();

            var oneWayCol = _oneWayGO.GetComponent<CompositeCollider2D>();
            Physics2D.IgnoreCollision(_playerController.Col2D, oneWayCol);

            _playerController.States.CanDownward = false;
            _playerController.States.IsDisable = true;

            yield return new WaitForSeconds(0.25f);

            _playerController.States.IsDisable = false;
            Physics2D.IgnoreCollision(_playerController.Col2D, oneWayCol, false);
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private PlayerCore _playerController;

        [SerializeField] private GameObject _oneWayGO;

        [SerializeField] private string _tagOneWay;

        #endregion
    }
}
