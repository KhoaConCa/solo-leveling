using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Platform2D.CameraSystem
{
    /// <summary>
    /// CameraController - Đóng vai trò trung tâm nhằm thực hiện việc di chuyển camera theo người chơi.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 01/05/2025
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        #region --- Unity Methods ---
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            SetupCameras();
        }

        #endregion

        #region --- Methods ---

        #region -- Interpolation Based on Player's Y Velocity --
        /// <summary>
        /// Kiểm tra xem các camera có đang được kích hoạt hay không, nếu có thì lấy camera đó làm camera hiện tại.
        /// </summary>
        private void SetupCameras()
        {
            foreach (var cam in _allVirtualCameras)
            {
                if (cam.enabled)
                {
                    // Set Current Virtual Camera to the first active camera
                    _currentVirtualCamera = cam;

                    // Get the Framing Transposer component from the current virtual camera
                    _framingTransposer = _currentVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                    Debug.Log("CameraController: SetupCameras - Camera found: " + cam.name);
                    break;
                }
            }

            if (_framingTransposer != null)
            {
                _normalYPanAnount = _framingTransposer.m_YDamping;
                _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
            }
            else
            {
                Debug.LogError("CinemachineFramingTransposer not found on any active virtual camera.", this);
                enabled = false;
            }
        }

        /// <summary>
        /// Gọi Coroutine để thực hiện việc lerp Y Damping cho camera.
        /// </summary>
        /// <param name="isPlayerFalling">Player có đang rơi hay không</param>
        public void LerpYDamping(bool isPlayerFalling)
        {
            _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
        }

        /// <summary>
        /// Coroutine thực hiện việc lerp Y Damping cho camera.
        /// </summary>
        /// <param name="isPlayerFalling">Player có đang rơi hay không</param>
        /// <returns>null</returns>
        private IEnumerator LerpYAction(bool isPlayerFalling)
        {
            IsLerpingYDamping = true;

            // Grab the current Y damping value and set end value to 0
            float startDampAmount = _framingTransposer.m_YDamping;
            float endDampAmount = 0f;

            // Determine the end value based on whether the player is falling or not
            if (isPlayerFalling)
            {
                endDampAmount = _fallPanAmount;
                LerpedFromPlayerFalling = true;
            }
            else
            {
                endDampAmount = _normalYPanAnount;
            }

            // Lerp the Y damping value over the specified time
            float elapsedTime = 0f;

            while (elapsedTime < _fallPanYDampingTime)
            {
                elapsedTime += Time.deltaTime;

                float lerpedPanAmount = Mathf.Clamp01(elapsedTime / _fallPanYDampingTime);
                _framingTransposer.m_YDamping = Mathf.Lerp(startDampAmount, endDampAmount, lerpedPanAmount);

                yield return null;
            }

            IsLerpingYDamping = false;
        }
        #endregion

        #region -- Ledge Detection --
        public void PanCameraOnContact(float panDistance, float panTime, CAMERA_PAN_DIRECTION panDirection, bool panToStartingPosition)
        {
            _panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPosition));
        }

        private IEnumerator PanCamera(float panDistance, float panTime, CAMERA_PAN_DIRECTION panDirection, bool panToStartingPosition)
        {
            Vector2 endPosition = Vector2.zero;
            Vector2 startingPosition = Vector2.zero;

            if (!panToStartingPosition)
            {
                switch (panDirection)
                {
                    case CAMERA_PAN_DIRECTION.UP:
                        endPosition = Vector2.up;
                        break;
                    case CAMERA_PAN_DIRECTION.DOWN:
                        endPosition = Vector2.down;
                        break;
                    case CAMERA_PAN_DIRECTION.LEFT:
                        endPosition = Vector2.right;
                        break;
                    case CAMERA_PAN_DIRECTION.RIGHT:
                        endPosition = Vector2.left;
                        break;
                    default:
                        break;
                }

                endPosition *= panDistance;

                startingPosition = _startingTrackedObjectOffset;

                endPosition += startingPosition;
            }
            else
            {
                startingPosition = _framingTransposer.m_TrackedObjectOffset;
                endPosition = _startingTrackedObjectOffset;
            }

            float elapsedTime = 0f;

            while (elapsedTime < panTime)
            {
                elapsedTime += Time.deltaTime;

                Vector3 panLerp = Vector3.Lerp(startingPosition, endPosition, (elapsedTime / panTime));
                _framingTransposer.m_TrackedObjectOffset = panLerp;

                yield return null;
            }
        }
        #endregion

        #region -- Camera Switch --
        public void SwitchCamera(CinemachineVirtualCamera camLeft, CinemachineVirtualCamera camRight, Vector2 triggerExitDirection)
        {
            if (_currentVirtualCamera == camLeft && triggerExitDirection.x > 0f)
            {
                camRight.enabled = true;
                camLeft.enabled = false;
                _currentVirtualCamera = camRight;
                _framingTransposer = _currentVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
            else if (_currentVirtualCamera == camRight && triggerExitDirection.x < 0f)
            {
                camLeft.enabled = true;
                camRight.enabled = false;
                _currentVirtualCamera = camLeft;
                Debug.Log("CameraController: SwitchCamera - Camera switched to: " + camLeft.name);
                _framingTransposer = _currentVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        #endregion

        #endregion

        #region --- Properties ---

        public bool IsLerpingYDamping { get; set; }
        public bool LerpedFromPlayerFalling { get; set; }

        #endregion

        #region --- Fields ---

        [Header("Controls for lerping Y Damping during player jump/fall")]
        [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;
        [SerializeField] private float _fallPanAmount = 0.25f;
        [SerializeField] private float _fallPanYDampingTime = 0.1f;

        public static CameraController instance;
        private Coroutine _lerpYPanCoroutine;
        private CinemachineFramingTransposer _framingTransposer;
        private CinemachineVirtualCamera _currentVirtualCamera;

        private float _normalYPanAnount;

        private Coroutine _panCameraCoroutine;

        private Vector2 _startingTrackedObjectOffset;

        #endregion
    }
}
