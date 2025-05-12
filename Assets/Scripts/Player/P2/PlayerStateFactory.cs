using Platform2D.CharacterController;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// PlayerStateFactory - Được dùng để khởi tạo các States thuộc Player.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 07/05/2025
    /// </summary>
    public class PlayerStateFactory
    {
        #region --- Methods ---

        /// <summary>
        /// Khởi tạo State Factory.
        /// </summary>
        /// <param name="controller">Controller của Player.</param>
        public PlayerStateFactory(PlayerCore controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Khởi tạo State Idle của Player
        /// </summary>
        /// <returns>Trả về State Idle của Player.</returns>
        public BaseState<PlayerCore, PlayerStateFactory> Idle() => new PlayerIdleState(_controller, this);

        /// <summary>
        /// Khởi tạo State Run của Player.
        /// </summary>
        /// <returns>Trả về State Run của Player.</returns>
        public BaseState<PlayerCore, PlayerStateFactory> Run() => new PlayerRunState(_controller, this);

        /// <summary>
        /// Khởi tạo State Jump của Player.
        /// </summary>
        /// <returns>Trả về State Jump của Player.</returns>
        public BaseState<PlayerCore, PlayerStateFactory> Jump() => new PlayerJumpState(_controller, this);

        /// <summary>
        /// Khởi tạo State Fall của Player.
        /// </summary>
        /// <returns>Trả về State Fall của Player.</returns>
        public BaseState<PlayerCore, PlayerStateFactory> Fall() => new PlayerFallState(_controller, this);

        /// <summary>
        /// Khởi tạo State Crouch của Player.
        /// </summary>
        /// <returns>Trả về State Crouch của Player.</returns>
        public BaseState<PlayerCore, PlayerStateFactory> Crouch() => new PlayerCrouchState(_controller, this);

        /// <summary>
        /// Khởi tạo State Dash của Player.
        /// </summary>
        /// <returns>Trả về State Dash của Player.</returns>
        public BaseState<PlayerCore, PlayerStateFactory> Dash() => new PlayerDashState(_controller, this);

        #endregion

        #region --- Fields ---

        private readonly PlayerCore _controller;

        #endregion
    }
}
