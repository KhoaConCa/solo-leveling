using UnityEngine;

public class CameraLedge : MonoBehaviour
{
    //#region --- Unity Methods ---

    //void Start()
    //{
    //    _collider2D = GetComponent<Collider2D>();
    //}

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("MainPlayer"))
    //    {
    //        if (customInspectorObject.panCameraOnContact)
    //        {
    //            PanCameraOnContact(customInspectorObject.panDistance,
    //                customInspectorObject.panTime,
    //                customInspectorObject.panDirection, false);
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("MainPlayer"))
    //    {
    //        if (customInspectorObject.panCameraOnContact)
    //        {
    //            PanCameraOnContact(customInspectorObject.panDistance,
    //                customInspectorObject.panTime,
    //                customInspectorObject.panDirection, true);
    //        }
    //    }
    //}

    //#endregion

    //#region --- Methods ---

    //#region -- Ledge Detection --
    //public void PanCameraOnContact(float panDistance, float panTime, CAMERA_PAN_DIRECTION panDirection, bool panToStartingPosition)
    //{
    //    if (_panCameraCoroutine != null)
    //    {
    //        StopCoroutine(_panCameraCoroutine);
    //    }

    //    _panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPosition));
    //}

    //private IEnumerator PanCamera(float panDistance, float panTime, CAMERA_PAN_DIRECTION panDirection, bool panToStartingPosition)
    //{
    //    Vector2 endPosition = Vector2.zero;
    //    Vector2 startPosition = Vector2.zero;

    //    if (!panToStartingPosition)
    //    {
    //        switch (panDirection)
    //        {
    //            case CAMERA_PAN_DIRECTION.UP:
    //                endPosition = Vector2.up;
    //                break;
    //            case CAMERA_PAN_DIRECTION.DOWN:
    //                endPosition = Vector2.down;
    //                break;
    //            case CAMERA_PAN_DIRECTION.LEFT:
    //                endPosition = Vector2.left;
    //                break;
    //            case CAMERA_PAN_DIRECTION.RIGHT:
    //                endPosition = Vector2.right;
    //                break;
    //            default:
    //                break;
    //        }

    //        endPosition *= panDistance;
    //        startPosition = _startingTrackedObjectOffset;
    //        endPosition += startPosition;
    //    }
    //    else
    //    {
    //        startPosition = _cameraCore._framingTransposer.m_TrackedObjectOffset;
    //        endPosition = _startingTrackedObjectOffset;
    //    }

    //    float elapsedTime = 0f;

    //    while (elapsedTime < panTime)
    //    {
    //        elapsedTime += Time.deltaTime;

    //        Vector3 panLerp = Vector3.Lerp(startPosition, endPosition, (elapsedTime / panTime));
    //        _cameraCore._framingTransposer.m_TrackedObjectOffset = panLerp;

    //        yield return null;
    //    }
    //}
    //#endregion

    //#endregion

    //#region --- Fields ---

    //[Header("Camera Ledge Detection")]
    //public CustomInspector customInspectorObject;

    //private Collider2D _collider2D;
    //private Coroutine _panCameraCoroutine;
    //private Vector2 _startingTrackedObjectOffset;
    //private CameraCore _cameraCore;

    //#endregion
}

//[System.Serializable]
//public class CustomInspector
//{
//    #region --- Fields ---

//    public bool swapCamera = false;
//    public bool panCameraOnContact = false;

//    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
//    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

//    [HideInInspector] public CAMERA_PAN_DIRECTION panDirection;
//    [HideInInspector] public float panDistance = 3f;
//    [HideInInspector] public float panTime = 0.35f;

//    #endregion
//}

//[CustomEditor(typeof(CameraLedge))]
//public class CameraLegdeEditor : Editor
//{
//    public CameraLedge cameraLedge;

//    #region --- Unity Methods ---

//    private void OnEnable()
//    {
//        cameraLedge = (CameraLedge)target;
//    }

//    #endregion

//    #region --- Overrides ---

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        if (cameraLedge.customInspectorObject.swapCamera)
//        {
//            cameraLedge.customInspectorObject.cameraOnLeft = EditorGUILayout.ObjectField("Camera on Left",
//                cameraLedge.customInspectorObject.cameraOnLeft,
//                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

//            cameraLedge.customInspectorObject.cameraOnRight = EditorGUILayout.ObjectField("Camera on Right",
//                cameraLedge.customInspectorObject.cameraOnRight,
//                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
//        }

//        if (cameraLedge.customInspectorObject.panCameraOnContact)
//        {
//            cameraLedge.customInspectorObject.panDirection = (CAMERA_PAN_DIRECTION)EditorGUILayout.EnumPopup(" Camera Pan Direction", cameraLedge.customInspectorObject.panDirection);
//            cameraLedge.customInspectorObject.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraLedge.customInspectorObject.panDistance);
//            cameraLedge.customInspectorObject.panTime = EditorGUILayout.FloatField("Pan Time", cameraLedge.customInspectorObject.panTime);
//        }

//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(cameraLedge);
//        }
//    }

//    #endregion
//}
