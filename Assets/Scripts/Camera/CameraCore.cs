using Cinemachine;
using Platform2D.CharacterController;
using System.Collections;
using UnityEngine;

namespace Platform2D.CameraSystem
{
    /// <summary>
    /// CameraCore - Đóng vai trò trung tâm bộ điều khiển Camera.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 09/05/2025.
    /// </summary>
    public class CameraCore : MonoBehaviour
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

            GetPlayerCoreComponent();

            SetupCameras();
        }

        void Update()
        {

        }

        #endregion

        #region --- Methods ---

        /// <summary>
        /// Lấy component PlayerCore.
        /// </summary>
        private void GetPlayerCoreComponent()
        {
            _playerController = playerPosition.gameObject.GetComponent<PlayerCore>();

            if (_playerController == null)
            {
                Debug.LogError("PlayerCore component not found on playerPosition object.", this);
                enabled = false;
            }
        }

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

        #endregion

        #region --- Properties ---

        public bool IsLerpingYDamping { get; set; }
        public bool LerpedFromPlayerFalling { get; set; }

        #endregion

        #region --- Fields ---

        [Header("Controls for lerping Y Damping during player jump/fall")]
        [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;
        [SerializeField] public CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float _fallPanAmount;
        [SerializeField] private float _fallPanYDampingTime;

        public static CameraCore instance;
        private Coroutine _lerpYPanCoroutine;
        private CinemachineFramingTransposer _framingTransposer;
        private CinemachineVirtualCamera _currentVirtualCamera;

        private float _normalYPanAnount;

        [Header("Camera Follower with Direction Bias")]
        [SerializeField] public Transform playerPosition;
        [SerializeField] public GameObject cameraFollower;
        [SerializeField] private float _flipYRotationTime = 0.2f;

        private PlayerCore _playerController;

        private bool _isFacingRight;
        private readonly float _followSpeed = 5f;
        private Vector2 _startingTrackedObjectOffset;

        #endregion
    }
}