using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.CameraSystem
{
    /// <summary>
    /// CameraFollower - Đóng vai trò trung tâm nhằm thực hiện cân bằng việc camera di chuyển theo người chơi.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 08/05/2025.
    /// </summary>
    public class CameraFollower : MonoBehaviour
    {
        #region --- Unity Methods ---

        void Awake()
        {
            _playerController = _playerPosition.gameObject.GetComponent<PlayerCore>();
        }

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _playerPosition.position, Time.deltaTime * _followSpeed);
        }

        #endregion

        #region --- Methods ---

        #region -- LeanTween --
        /// <summary>
        /// Thực hiện việc quay camera theo hướng của người chơi bằng LeanTween.
        /// </summary>
        public void TurnCalling()
        {
            _isFacingRight = _playerController.States.Direction.x > 0;

            LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipYRotationTime).setEaseInOutSine();
        }
        #endregion

        #region -- Coroutine --
        /// <summary>
        /// Thực hiện việc quay camera theo hướng của người chơi bằng Coroutine.
        /// </summary>
        public void BasicTurnCalling()
        {
            if (_turnCoroutine != null)
            {
                StopCoroutine(_turnCoroutine);
            }

            _isFacingRight = _playerController.States.Direction.x > 0;

            _turnCoroutine = StartCoroutine(FlipYLerp());
        }

        /// <summary>
        /// Hàm Coroutine thực hiện việc quay camera theo hướng của người chơi.
        /// </summary>
        /// <returns>null</returns>
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
        #endregion

        /// <summary>
        /// Xác định góc quay cuối cùng của camera dựa trên hướng di chuyển của người chơi.
        /// </summary>
        /// <returns>0 đối với xoay qua phải - 180 đối với xoay qua trái</returns>
        private float DetermineEndRotation()
        {
            return _isFacingRight ? 0f : 180f;
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Transform _playerPosition;
        [SerializeField] private float _flipYRotationTime = 0.5f;

        private Coroutine _turnCoroutine;
        private PlayerCore _playerController;

        private bool _isFacingRight;
        private readonly float _followSpeed = 5f;
        #endregion
    }
}
