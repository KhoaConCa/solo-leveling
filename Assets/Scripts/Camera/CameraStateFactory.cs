using Platform2D.CameraSystem;
using Platform2D.HirarchicalStateMachine;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// CameraStateFactory - Dùng để khởi tạo các State cho Camera.
    /// Tác giả: Dương Nhật Khoa, Ngày tạo: 11/05/2025.
    /// </summary>
    public class CameraStateFactory
    {
        #region --- Methods ---

        /// <summary>
        /// Contrucstor của CameraStateFactory.
        /// </summary>
        /// <param name="controller">Controller của Camera</param>
        public CameraStateFactory(CameraCore controller)
        {
            _controller = controller;
        }

        public BaseState<CameraCore, CameraStateFactory> DefaultFollow() => new DefaultFollowState(_controller, this);

        #endregion

        #region --- Fields ---

        private readonly CameraCore _controller;

        #endregion
    }
}
