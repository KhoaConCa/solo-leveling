namespace Platform2D.HirarchicalStateMachine
{
    /// <summary>
    /// DefaultFollowState - Là một State của Camera được kế thừa từ BaseState, được dùng để xử lý Logic.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 11/05/2025.
    /// </summary>
    //public class DefaultFollowState : BaseState<CameraCore, CameraStateFactory>
    //{
    //    #region --- Overrides ---

    //    public DefaultFollowState(CameraCore stateController, CameraStateFactory stateFactory) : base(stateController, stateFactory) { }

    //    public override void EnterState()
    //    {
    //        _playerTransform = _stateController.playerPosition;
    //        _playerController = _playerTransform.gameObject.GetComponent<PlayerCore>();
    //    }
    //    public override void UpdateState()
    //    {
    //        float playerScaleX = _playerTransform.localScale.x;

    //        FlipBaseOnScale(playerScaleX);

    //        _stateController.cameraFollower.transform.position = Vector3.Lerp(_stateController.cameraFollower.transform.position, _stateController.playerPosition.position, Time.deltaTime * _followSpeed);
    //    }
    //    public override void ExitState() { }

    //    public override void CheckSwitchState() { }

    //    public override void SwitchState(BaseState<CameraCore, CameraStateFactory> newState)
    //    {
    //        base.SwitchState(newState);
    //    }

    //    #endregion

    //    #region --- Methods ---

    //    /// <summary>
    //    /// FlipBaseOnScale - Đảo ngược giá trị offset của camera dựa trên Scale của nhân vật.
    //    /// </summary>
    //    /// <param name="scaleX">Giá trị của Scale X của nhân vật</param>
    //    private void FlipBaseOnScale(float scaleX)
    //    {
    //        Vector3 newOffset;

    //        if (scaleX < 0)
    //            newOffset = new Vector3(-_baseOffset.x, _baseOffset.y, _baseOffset.z);
    //        else
    //            newOffset = _baseOffset;

    //        CinemachineFramingTransposer transposer = _stateController.virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    //        if (transposer != null)
    //        {
    //            transposer.m_TrackedObjectOffset = newOffset;
    //        }
    //    }

    //    #endregion

    //    #region --- Fields ---

    //    private Transform _playerTransform;
    //    private PlayerCore _playerController;

    //    private Vector3 _baseOffset = new Vector3(0.7f, 0, 0);

    //    private readonly float _followSpeed = 5f;

    //    #endregion
    //}
}
