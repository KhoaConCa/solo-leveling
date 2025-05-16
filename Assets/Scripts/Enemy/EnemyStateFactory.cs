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

        #region -- Target States --
        /// <summary>
        /// Khởi tạo State Detect của Enemy.
        /// </summary>
        /// <returns>Trả về State Detect của Enemy.</returns>
        public BaseState<EnemyController, EnemyStateFactory> Detect() => new EnemyDetectState(_controller, this);

        /// <summary>
        /// Khởi tạo State Chasing của Enemy.
        /// </summary>
        /// <returns>Trả về State Chasing của Enemy.</returns>
        public BaseState<EnemyController, EnemyStateFactory> Chasing() => new EnemyChasingState(_controller, this);

        /// <summary>
        /// Khởi tạo State Return của Enemy.
        /// </summary>
        /// <returns>Trả về State Return của Enemy.</returns>
        public BaseState<EnemyController, EnemyStateFactory> Return() => new EnemyReturnState(_controller, this);
        #endregion

        #region -- Receive States --
        /// <summary>
        /// Khởi tạo State Hit của Enemy.
        /// </summary>
        /// <returns>Trả về State Hit của Enemy.</returns>
        public BaseState<EnemyController, EnemyStateFactory> Hit() => new EnemyHitState(_controller, this);

        /// <summary>
        /// Khởi tạo State Dead của Enemy.
        /// </summary>
        /// <returns>Trả về State Dead của Enemy.</returns>
        public BaseState<EnemyController, EnemyStateFactory> Dead() => new EnemyDeadState(_controller, this);
        #endregion

        #endregion

        #region --- Fields ---

        private readonly EnemyController _controller;

        #endregion
    }
}
