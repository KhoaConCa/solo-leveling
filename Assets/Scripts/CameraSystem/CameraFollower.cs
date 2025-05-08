using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.CameraSystem
{
    /// <summary>
    /// CameraFollower - Đóng vai trò trung tâm nhằm thực hiện cân bằng việc camera di chuyển theo người chơi.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 08/05/2025
    /// </summary>
    public class CameraFollower : MonoBehaviour
    {
        #region --- Unity Methods ---

        void Awake()
        {
            _player = _playerPosition.gameObject.GetComponent<PlayerController>();
            _isFacingRight = _player.IsFacingRight;
            Debug.Log($"Is Facing Right: {_isFacingRight}");
        }

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _playerPosition.position, Time.deltaTime * _followSpeed);
        }

        #endregion

        #region --- Methods ---

        public void TurnCalling()
        {
            //if (_turnCoroutine != null)
            //{
            //    StopCoroutine(_turnCoroutine);
            //}

            _isFacingRight = _player.IsFacingRight;

            //_turnCoroutine = StartCoroutine(FlipYLerp());
            LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipYRotationTime).setEaseInOutSine();
        }

        private IEnumerator FlipYLerp()
        {
            float startRoatation = transform.localEulerAngles.y;
            float endRotation = DetermineEndRotation();
            float yRotation = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < _flipYRotationTime)
            {
                elapsedTime += Time.deltaTime;
                yRotation = Mathf.Lerp(startRoatation, endRotation, (elapsedTime / _flipYRotationTime));
                transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

                yield return null;
            }
        }

        private float DetermineEndRotation()
        {
            Debug.Log($"Is Facing Right: {_isFacingRight}");
            return _isFacingRight ? 0f : 180f;
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Transform _playerPosition;
        [SerializeField] private float _flipYRotationTime = 0.5f;

        private Coroutine _turnCoroutine;
        private PlayerController _player;

        private bool _isFacingRight;
        private float _followSpeed = 5f;

        #endregion
    }
}
