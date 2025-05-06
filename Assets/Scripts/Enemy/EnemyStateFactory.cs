using Platform2D.CharacterController;

namespace Platform2D.HierarchicalStateMachine
{
    /// <summary>
    /// EnemyStateFactory - Được dùng để khởi tạo các States thuộc Enemy.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 06/05/2025
    /// </summary>
    public class EnemyStateFactory
    {
        #region --- Methods ---

        /// <summary>
        /// Khởi tạo State Factory.
        /// </summary>
        /// <param name="controller">Controller của Enemy.</param>
        public EnemyStateFactory(EnemyController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Khởi tạo State Idle của Enemy
        /// </summary>
        /// <returns>Trả về State Idle của Enemy.</returns>
        public BaseState<EnemyController, EnemyStateFactory> Idle() => new EnemyIdleState(_controller, this);

        /// <summary>
        /// Khởi tạo State Run của Enemy.
        /// </summary>
        /// <returns>Trả về State Run của Enemy.</returns>
        public BaseState<EnemyController, EnemyStateFactory> Run() => new EnemyRunState(_controller, this);

        #endregion

        #region --- Fields ---

        private readonly EnemyController _controller;

        #endregion
    }
}
