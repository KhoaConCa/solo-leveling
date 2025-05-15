using Cinemachine;
using UnityEditor;
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
                if (customInspectorObject.panCameraOnContact)
                {
                    CameraController.instance.PanCameraOnContact(customInspectorObject.panDistance,
                        customInspectorObject.panTime,
                        customInspectorObject.panDirection, false);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider == null) return;

            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Vector2 exitDirection = (collider.transform.position - collider.bounds.center).normalized;

                if (customInspectorObject.swapCamera && customInspectorObject.cameraOnLeft != null && customInspectorObject.cameraOnRight != null)
                {
                    CameraController.instance.SwitchCamera(customInspectorObject.cameraOnLeft, customInspectorObject.cameraOnRight, exitDirection);
                }

                if (customInspectorObject.panCameraOnContact)
                {
                    CameraController.instance.PanCameraOnContact(customInspectorObject.panDistance,
                        customInspectorObject.panTime,
                        customInspectorObject.panDirection, true);
                }
            }
        }

        #endregion

        #region --- Methods ---



        #endregion

        #region --- Fields ---

        [Header("Camera Ledge Detection")]
        public CustomInspector customInspectorObject;

        private Collider2D _collider2D;
        private Coroutine _panCameraCoroutine;

        #endregion
    }

    [System.Serializable]
    public class CustomInspector
    {
        #region --- Fields ---

        public bool swapCamera = false;
        public bool panCameraOnContact = false;

        [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
        [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

        [HideInInspector] public CAMERA_PAN_DIRECTION panDirection;
        [HideInInspector] public float panDistance = 3f;
        [HideInInspector] public float panTime = 0.35f;

        #endregion
    }

    [CustomEditor(typeof(CameraTrigger))]
    public class CameraLegdeEditor : Editor
    {
        public CameraTrigger cameraTrigger;

        #region --- Unity Methods ---

        private void OnEnable()
        {
            cameraTrigger = (CameraTrigger)target;
        }

        #endregion

        #region --- Overrides ---

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (cameraTrigger.customInspectorObject.swapCamera)
            {
                cameraTrigger.customInspectorObject.cameraOnLeft = EditorGUILayout.ObjectField("Camera on Left",
                    cameraTrigger.customInspectorObject.cameraOnLeft,
                    typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

                cameraTrigger.customInspectorObject.cameraOnRight = EditorGUILayout.ObjectField("Camera on Right",
                    cameraTrigger.customInspectorObject.cameraOnRight,
                    typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
            }

            if (cameraTrigger.customInspectorObject.panCameraOnContact)
            {
                cameraTrigger.customInspectorObject.panDirection = (CAMERA_PAN_DIRECTION)EditorGUILayout.EnumPopup(" Camera Pan Direction", cameraTrigger.customInspectorObject.panDirection);
                cameraTrigger.customInspectorObject.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraTrigger.customInspectorObject.panDistance);
                cameraTrigger.customInspectorObject.panTime = EditorGUILayout.FloatField("Pan Time", cameraTrigger.customInspectorObject.panTime);
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(cameraTrigger);
            }
        }

        #endregion
    }
}
