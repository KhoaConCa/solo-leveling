using Cinemachine;
using UnityEngine;

namespace Platform2D.CameraSystem
{
    /// <summary>
    /// Camera Trigger - Thực hiện Ledge Detection cho camera.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 15/05/2025.
    /// </summary>
    public class CameraTrigger : MonoBehaviour
    {
        #region --- Unity Methods ---

        void Start()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider == null) return;

            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (panCameraOnContact)
                {
                    CameraController.instance.PanCameraOnContact(panDistance,
                        panTime,
                        panDirection, false);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider == null) return;

            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Vector2 exitDirection = (collider.transform.position - collider.bounds.center).normalized;

                if (swapCamera && cameraOnLeft != null && cameraOnRight != null)
                {
                    CameraController.instance.SwitchCamera(cameraOnLeft, cameraOnRight, exitDirection);
                }

                if (panCameraOnContact)
                {
                    CameraController.instance.PanCameraOnContact(panDistance,
                        panTime,
                        panDirection, true);
                }
            }
        }

        #endregion

        #region --- Methods ---



        #endregion

        #region --- Fields ---

        [Header("Camera Ledge Detection")]
        public bool swapCamera = true;
        public bool panCameraOnContact = true;

        [SerializeField] public CinemachineVirtualCamera cameraOnLeft;
        [SerializeField] public CinemachineVirtualCamera cameraOnRight;

        [SerializeField] public CAMERA_PAN_DIRECTION panDirection;
        [SerializeField] public float panDistance = 3f;
        [SerializeField] public float panTime = 0.35f;

        private Collider2D _collider2D;
        private Coroutine _panCameraCoroutine;

        #endregion
    }
}
